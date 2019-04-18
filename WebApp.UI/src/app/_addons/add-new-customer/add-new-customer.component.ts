import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Customer } from 'src/app/_models';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
@Component({
  selector: 'app-add-new-customer',
  templateUrl: './add-new-customer.component.html',
  styleUrls: ['./add-new-customer.component.css']
})
export class AddNewCustomerComponent implements OnInit {
  customerForm: FormGroup;

  @Input() customer: Customer;
  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder) { }
  ngOnInit() {
    if (this.customer == null) {
      this.customer = new Customer();
    }
    this.customerForm = this.formBuilder.group({
      id: this.customer.id,
      firstName: new FormControl(this.customer.firstName, [Validators.required]),
      lastName: new FormControl(this.customer.lastName, [Validators.required]),
      email: new FormControl(this.customer.email, [Validators.required, Validators.email]),
      address: new FormControl(this.customer.address, [Validators.required])
    });
  }

  get f() {
    return this.customerForm.controls;
  }

  save(): void {
    if (this.customerForm.invalid) {
      return;
    }
    console.log(`this.customerForm.value ${this.f.firstName.invalid}`);
    this.activeModal.close(this.customer);
    ;
  }
}
