import { Component, OnInit, Input } from '@angular/core';
import { AdComponent } from 'src/app/AdComponent';

@Component({
  selector: 'app-step-two',
  templateUrl: './step-two.component.html',
  styleUrls: ['./step-two.component.css']
})
export class StepTwoComponent implements OnInit, AdComponent {
  @Input() data: any;
  constructor() { }

  ngOnInit() {

  }
  submit() {
    this.data.parent.callback(this.data.name);
  }
}
