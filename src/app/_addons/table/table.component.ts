import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  constructor() {}
  datatable: any;

  ngOnInit() {
    const table = $('#dataTable');
    this.datatable = table.DataTable();
  }
}
