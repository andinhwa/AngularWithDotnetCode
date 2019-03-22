import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './app-sidebar.component.html',
  styleUrls: ['./app-sidebar.component.css']
})
export class AppSidebarComponent implements OnInit {
  constructor() {}

  ngOnInit() {
  }

  sidebarToggled(): void {
      $('[main-body]').toggleClass('sidebar-toggled');
      $('.sidebar').toggleClass('toggled');
      if ($('.sidebar').hasClass('toggled')) {
        debugger;
        $('.sidebar .collapse').collapse('hide');
      }
  }
}
