import { EventEmitter } from '@angular/core';

export interface AdComponent {
  data: any;
  eSubmit: EventEmitter<any>;
}
