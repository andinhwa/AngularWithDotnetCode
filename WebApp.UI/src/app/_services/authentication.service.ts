import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable} from 'rxjs';
import { map, first} from 'rxjs/operators';
import { AppTokenInfor } from '../_models';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<AppTokenInfor>;
  public currentUser: Observable<AppTokenInfor>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<AppTokenInfor>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): AppTokenInfor {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    const url = `${environment.apiEndpoint}/connect/token`;
    const body = `username=${username}&password=${password}&grant_type=password&scope=offline_access+profile+email+openid`;

    return this.http.post<AppTokenInfor>(url, body, httpOptions).pipe(
      map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.access_token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user as AppTokenInfor);
          this.getUserInfor();
        }
        return user;
      })
    );
  }

  getUserInfor(): void {
    const url = `${environment.apiEndpoint}/connect/userinfo`;
    this.http.get<AppTokenInfor>(url).pipe(first()).subscribe(data => {
      const user = this.currentUserSubject.value;
      user.username = data.username;
      user.fullName = data.fullName;
      user.roles = data.roles;
      localStorage.removeItem('currentUser');
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user as AppTokenInfor);
    });
  }

  logout(): void {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
