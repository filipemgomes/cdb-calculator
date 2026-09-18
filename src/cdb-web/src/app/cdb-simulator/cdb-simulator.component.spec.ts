import { provideHttpClient } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';

import { CdbSimulatorComponent } from './cdb-simulator.component';
import { CdbService } from './cdb.service';

describe('CdbSimulatorComponent', () => {
  let fixture: ComponentFixture<CdbSimulatorComponent>;
  let component: CdbSimulatorComponent;
  let cdbService: CdbService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CdbSimulatorComponent],
      providers: [provideHttpClient()],
    }).compileComponents();

    fixture = TestBed.createComponent(CdbSimulatorComponent);
    component = fixture.componentInstance;
    cdbService = TestBed.inject(CdbService);
  });

  it('should not call the API when the form is invalid', () => {
    const simulateSpy = vi.spyOn(cdbService, 'simulate');

    component.form.controls.valorInicial.setValue(0);
    component.form.controls.prazoMeses.setValue(1);
    component.simulate();

    expect(simulateSpy).not.toHaveBeenCalled();
    expect(component.form.invalid).toBe(true);
  });

  it('should call the API and expose the returned result with a valid form', () => {
    vi.spyOn(cdbService, 'simulate').mockReturnValue(
      of({ resultadoBruto: 1019.53, resultadoLiquido: 1015.14 }),
    );

    component.form.controls.valorInicial.setValue(1000);
    component.form.controls.prazoMeses.setValue(2);
    component.simulate();

    expect(cdbService.simulate).toHaveBeenCalledWith({ valorInicial: 1000, prazoMeses: 2 });
    expect(component.result()).toEqual({ resultadoBruto: 1019.53, resultadoLiquido: 1015.14 });
    expect(component.errors()).toEqual([]);
  });

  it('should expose the messages without a result when the API returns a validation error', () => {
    vi.spyOn(cdbService, 'simulate').mockReturnValue(
      throwError(() => ({ error: { erros: ['O prazo deve ser um número inteiro de meses maior que 1.'] } })),
    );

    component.form.controls.valorInicial.setValue(1000);
    component.form.controls.prazoMeses.setValue(2);
    component.simulate();

    expect(component.result()).toBeNull();
    expect(component.errors()).toEqual(['O prazo deve ser um número inteiro de meses maior que 1.']);
  });

  it('should clear the previous result when a field changes after a successful simulation', () => {
    const simulateSpy = vi
      .spyOn(cdbService, 'simulate')
      .mockReturnValue(of({ resultadoBruto: 1019.53, resultadoLiquido: 1015.14 }));

    component.form.controls.valorInicial.setValue(1000);
    component.form.controls.prazoMeses.setValue(2);
    component.simulate();
    expect(component.result()).not.toBeNull();

    component.form.controls.valorInicial.setValue(2000);

    expect(component.result()).toBeNull();
    expect(simulateSpy).toHaveBeenCalledTimes(1);
  });
});
