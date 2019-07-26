import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TabStepComponent } from './tab-step/tab-step.component';

const routes: Routes = [
  { path: '', component: TabStepComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DemoModuleRoutingModule {}
