import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';

import { CdbService, ValidationErrorResponse, SimulateResponse } from './cdb.service';

function integerMinValidator(min: number) {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (value === null || value === '') {
      return null;
    }
    return Number.isInteger(value) && value >= min ? null : { integerMin: { min } };
  };
}

@Component({
  selector: 'app-cdb-simulator',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cdb-simulator.component.html',
  styleUrl: './cdb-simulator.component.scss',
})
export class CdbSimulatorComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly cdbService = inject(CdbService);

  readonly form = this.formBuilder.group({
    valorInicial: [null as number | null, [Validators.required, Validators.min(0.01)]],
    prazoMeses: [null as number | null, [Validators.required, integerMinValidator(2)]],
  });

  readonly result = signal<SimulateResponse | null>(null);
  readonly errors = signal<string[]>([]);
  readonly loading = signal(false);

  constructor() {
    this.form.valueChanges.pipe(takeUntilDestroyed()).subscribe(() => this.result.set(null));
  }

  simulate(): void {
    this.result.set(null);
    this.errors.set([]);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { valorInicial, prazoMeses } = this.form.getRawValue();

    this.loading.set(true);
    this.cdbService
      .simulate({ valorInicial: valorInicial as number, prazoMeses: prazoMeses as number })
      .subscribe({
        next: (result) => {
          this.result.set(result);
          this.loading.set(false);
        },
        error: (error: HttpErrorResponse) => {
          this.errors.set(this.extractErrors(error));
          this.loading.set(false);
        },
      });
  }

  private extractErrors(error: HttpErrorResponse): string[] {
    const body = error.error as ValidationErrorResponse | undefined;
    if (body?.erros?.length) {
      return body.erros;
    }
    return ['Não foi possível calcular a simulação. Tente novamente.'];
  }
}
