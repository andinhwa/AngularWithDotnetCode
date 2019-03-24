import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppLayoutComponent } from './_layout/app-layout/app-layout.component';
import { ButtonComponent } from './_components/button/button.component';
import { AppComponentComponent } from './_components/app-component/app-component.component';
import { CardsComponent } from './_components/cards/cards.component';
import { AppAddonComponent } from './_addons/app-addon/app-addon.component';
import { TableComponent } from './_addons/table/table.component';
import {AuthGuard} from './_Guard';
const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: DashboardComponent, pathMatch: 'full' },
      {
        path: '',
        component: AppComponentComponent,
        children: [
          { path: 'button', component: ButtonComponent },
          { path: 'cards', component: CardsComponent }
        ]
      },
      {
        path: '',
        component: AppAddonComponent,
        children: [{ path: 'table', component: TableComponent }]
      }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
