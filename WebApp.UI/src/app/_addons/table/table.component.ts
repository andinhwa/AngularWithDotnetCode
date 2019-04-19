import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import { Customer } from '../../_models';
import { CustomerService } from '../../_services';
import { Observable, Subject, from } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddNewCustomerComponent } from '../add-new-customer/add-new-customer.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  table: any;
  customers$: Observable<Customer[]>;
  customers: Customer[];
  page = 1;
  pageSize = 70;

  private searchTerms = new Subject<string>();
  constructor(
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: NgbModal

  ) { }

  ngOnInit() {
    this.page = +this.route.snapshot.paramMap.get('tab');
    this.getDataTables();
    this.customerService.search(this.searchTerms)
      .subscribe(_ => this.customers = _);
  }

  pageChange(): void {
    this.router.navigateByUrl(`/table/${this.page}`);
  }

  getDataTables(): void {
    this.customerService.getAll()
      .subscribe(_ => this.customers = _);
  }

  search(term: string): void {
    this.searchTerms.next(term);
  }

  editCustomer(customer: Customer): void {
    const modalRef = this.modalService
      .open(AddNewCustomerComponent, { centered: true, backdrop: 'static', size: 'lg' });
    modalRef.result.then((result) => {
      // var item = this.customers.some(x => x.id === result.id);
      const filtered = this.customers.filter(_ => _.id === result.id);
      if (filtered.length > 0) {
        this.customers.splice(this.customers.indexOf(filtered[0]), 1);
      }
      this.customers.push(result);

    }, (reason) => {

    });
    modalRef.componentInstance.customer = customer;
  }

}
