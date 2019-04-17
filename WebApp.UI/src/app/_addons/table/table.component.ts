import { Component, OnInit, PipeTransform } from '@angular/core';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import { Customer } from '../../_models';
import { CustomerService } from '../../_services';
import { Observable, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  table: any;
  customers$: Observable<Customer[]>;
  page = 1;
  pageSize = 70;

  private searchTerms = new Subject<string>();
  constructor(
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.page = +this.route.snapshot.paramMap.get('tab');
    this.getDataTables();
    this.customers$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this.customerService.search(term))
    );
  }

  pageChange(): void {
    this.router.navigateByUrl(`/table/${this.page}`);
    this.getDataTables();
  }

  getDataTables(): void {
    this.customers$ = this.customerService.getAll();
  }

  search(term: string): void {
    this.searchTerms.next(term);
  }
}
