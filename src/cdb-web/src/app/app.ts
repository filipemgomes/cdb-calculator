import { Component } from '@angular/core';

import { CdbSimulatorComponent } from './cdb-simulator/cdb-simulator.component';

@Component({
  imports: [CdbSimulatorComponent],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App {}
