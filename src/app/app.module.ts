import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';

import { fakeBackendProvider } from './_helpers';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AppLayoutComponent } from './_layout/app-layout/app-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppSidebarComponent } from './_layout/app-sidebar/app-sidebar.component';
import { AppTopbarComponent } from './_layout/app-topbar/app-topbar.component';
import { AppFooterComponent } from './_layout/app-footer/app-footer.component';
import { AppComponentComponent } from './_components';
import { ButtonComponent } from './_components';
import { CardsComponent } from './_components';
import { AppAddonComponent } from './_addons/app-addon/app-addon.component';
import { TableComponent } from './_addons/table/table.component';
import {AlertComponent} from './_components';

import * as $ from 'jquery';
import * as bootstrap from 'bootstrap';
import { from } from 'rxjs';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    DashboardComponent,
    LoginComponent,
    AppSidebarComponent,
    AppTopbarComponent,
    AppFooterComponent,
    ButtonComponent,
    AppComponentComponent,
    CardsComponent,
    AppAddonComponent,
    TableComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    LoadingBarHttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    // provider used to create fake backend
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
