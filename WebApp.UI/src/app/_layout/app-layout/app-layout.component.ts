import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_services';
import { User } from 'src/app/_models';

@Component({
  selector: 'app-layout',
  templateUrl: './app-layout.component.html',
  styleUrls: ['./app-layout.component.css']
})
export class AppLayoutComponent implements OnInit {
  curentUser: User;
  constructor() {}
  ngOnInit() {
  }
}
