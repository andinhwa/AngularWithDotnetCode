import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Customer } from 'src/app/_models';
import { FormArray, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { CustomerService } from '../../_services';
import { from } from 'rxjs';

@Component({
  selector: 'app-add-new-customer',
  templateUrl: './add-new-customer.component.html',
  styleUrls: ['./add-new-customer.component.css']
})
export class AddNewCustomerComponent implements OnInit {
  customerForm: FormGroup;
  musicPreferences = [
    { id: 1, genre: 'Pop', },
    { id: 2, genre: 'Rock' },
    { id: 3, genre: 'Techno' },
    { id: 4, genre: 'Hiphop' }
  ];

  @Input() customer: Customer;
  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
  ) { }
  ngOnInit() {
    if (this.customer == null) {
      this.customer = new Customer();
    }

    const formControls = this.musicPreferences.map(control => new FormControl(false));
    this.customerForm = this.formBuilder.group({
      id: new FormControl(this.customer.id),
      firstName: new FormControl(this.customer.firstName, [Validators.required, Validators.minLength(2)]),
      lastName: new FormControl(this.customer.lastName, [Validators.required, Validators.minLength(2)]),
      email: new FormControl(this.customer.email, [Validators.required, Validators.email]),
      address: new FormControl(this.customer.address, [Validators.required]),
      // checkBoxs: this.formBuilder.array([
      //   this.formBuilder.control(''),
      //   this.formBuilder.control(''),
      //   this.formBuilder.control(''),
      // ])
      checkBoxs: new FormArray(formControls)
    });
  }

  get f() {
    return this.customerForm.controls;
  }

  invalid(name: string, validator?: string): boolean {
    if (validator == null) {
      return this.f[name].invalid;
    }
    return this.f[name].invalid && this.f[name].errors[validator];
  }

  save(): void {
    if (this.customerForm.invalid) {
      return;
    }
    console.log(this.customerForm);

    const selectedPreferences = this.customerForm.value.checkBoxs
    .map((checked, index) => checked ? this.musicPreferences[index] : null)
    .filter(value => value !== null);

    console.log(selectedPreferences);
    this.customerService.add(this.customerForm.value as Customer)
      .subscribe(_ => { this.activeModal.close(_); });
  }
}
