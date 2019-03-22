import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
    TableComponent
  ],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
