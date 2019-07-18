import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-step-one',
  templateUrl: './step-one.component.html',
  styleUrls: ['./step-one.component.css']
})
export class StepOneComponent implements OnInit {

  a: String;
  constructor() {
    this.a = 'teo.com.vn';

  }

  ngOnInit() {
  }

}
