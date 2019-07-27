import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AdComponent } from '../AdComponent';

@Component({
  selector: 'app-step-two',
  templateUrl: './step-two.component.html',
  styleUrls: ['./step-two.component.css']
})
export class StepTwoComponent implements OnInit, AdComponent {
  @Input() data: any;
  @Output() eSubmit: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit() {

  }
  submit() {
    this.eSubmit.emit(this.data.name);
  }
}
