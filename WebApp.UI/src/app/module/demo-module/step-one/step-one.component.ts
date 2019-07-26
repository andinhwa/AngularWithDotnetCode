import { Component, OnInit, Input } from '@angular/core';
import { AdComponent } from '../AdComponent';

@Component({
  selector: 'app-step-one',
  templateUrl: './step-one.component.html',
  styleUrls: ['./step-one.component.css']
})
export class StepOneComponent implements OnInit, AdComponent {
  @Input() data: any;
  ngOnInit() {
  }

  submit() {
    this.data.parent.callback(this.data.name);
  }
}
