import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, tap, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Customer } from '../_models';
import { environment } from '../../environments/environment';
const apiUrl = `${environment.apiEndpoint}/api/customer`;

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Customer[]>(`${apiUrl}`).pipe(
      tap(_ => console.log(`fetched Customer`)),
      catchError(this.handleError(`getCustomer`, []))
    );
  }

  getById(id: string) {
    return this.http.get(`${apiUrl}/${id}`).pipe(
      tap(_ => console.log(`fetched Customer id=${id}`)),
      catchError(this.handleError<Customer>(`Get Customer id=${id}`))
    );
  }

  add(customer: Customer) {
    return this.http.post<Customer>(`${apiUrl}`, customer).pipe(
      tap((newCustomer: Customer) => console.log(`add Customer w/ id =${newCustomer.id}`)),
      catchError(this.handleError<Customer>(`Add Customer`))
    );
  }

  update(customer: Customer) {
    return this.http.put(`${apiUrl}/${customer.id}`, customer).pipe(
      tap(_ => console.log(`updated Customer id = ${customer.id}`)),
      catchError(this.handleError<any>('updateCustomer'))
    );
  }

  delete(id: string) {
    return this.http.delete(`${apiUrl}/${id}`);
  }

  search(terms: Observable<string>) {
   return terms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),
      // ignore new term if same as previous term
      distinctUntilChanged(),
      // switch to new search observable each time the term changes
      switchMap((term: string) => this.search1(term))
    );
  }

  search1(key: string) {
    return this.http.get<Customer[]>(`${apiUrl}/search?key=${key}`).pipe(
      tap(_ => console.log(`found Customer matching "${key}"`)),
      catchError(this.handleError<Customer[]>('Search Customer', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.log(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
