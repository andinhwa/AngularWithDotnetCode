import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AdComponent } from '../AdComponent';

@Component({
  selector: 'app-step-one',
  templateUrl: './step-one.component.html',
  styleUrls: ['./step-one.component.css']
})
export class StepOneComponent implements OnInit, AdComponent {
  @Input() data: any;
  @Output() eSubmit: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit() {

  }

  submit() {
    this.eSubmit.emit(this.data.name);
  }
}
