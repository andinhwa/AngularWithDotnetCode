import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tab-step',
  templateUrl: './tab-step.component.html',
  styleUrls: ['./tab-step.component.css']
})
export class TabStepComponent implements OnInit {

  a: String;
  constructor() {
    this.a = 'teo.com.vn';

  }

  ngOnInit() {
  }

}
