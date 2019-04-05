import { Component, OnInit, PipeTransform } from '@angular/core';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import { UserInfor } from '../../_models/userInfor';
import { DataTableService } from '../../_services';
import { Observable, Subject } from 'rxjs';
import { ActivatedRoute, Router  } from '@angular/router';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  table: any;
  datas: UserInfor[];
  datas$: UserInfor[];
  page = 1;
  pageSize = 70;

  private searchTerms = new Subject<string>();

  constructor(
    private dataTableService: DataTableService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.page = +this.route.snapshot.paramMap.get('tab');
    this.getDataTables();
    this.searchTerms.subscribe(_ => {
      this.datas$ = this.searchData(_);
    });
  }

  pageChange(): void {
    this.router.navigateByUrl(`/table/${this.page}`);
    this.getDataTables();
  }

  getDataTables(): void {
    this.dataTableService.getAll().subscribe(datas => {
      this.datas = datas;
      this.searchTerms.next(``);
    });
    // const table = $('#dataTable');
    // this.table = table.DataTable();
  }

  searchData(_: string): UserInfor[] {
    return this.datas.filter(user => {
      const term = _.toLowerCase();
      return (
        user.first_name.toLowerCase().includes(term) ||
        user.last_name.toLowerCase().includes(term) ||
        user.gender.toLowerCase().includes(term) ||
        user.ip_address.toLowerCase().includes(term) ||
        user.email.toLowerCase().includes(term)
      );
    });
  }

  search(_: string) {
    this.searchTerms.next(_);
  }
}
