import { Injectable } from '@angular/core';
import { UserInfor } from '../_models';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class DataTableService {
  constructor(private http: HttpClient) {}
  private heroesUrl = `https://my.api.mockaroo.com/andinhdemo`; // URL to web api
  private httpParams = new HttpParams()
  .set('key', 'd4868c40');

  getAll(): Observable<UserInfor[]> {
    return this.http.get<UserInfor[]>(this.heroesUrl, {headers: httpOptions.headers, params: this.httpParams}).pipe(
      tap(_ => console.log('get OK')),
      catchError(this.handleError(`Get ALL userInfor`, []))
    );
  }

  private handleError<T>(operation = `operation`, result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.log(error); // log to console instead
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
