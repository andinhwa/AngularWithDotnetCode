import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DemoModuleComponent } from './demo-module.component';
import { DemoModuleRoutingModule } from './demo-module-routing.module';
import { StepOneComponent } from './step-one/step-one.component';
import { StepTwoComponent } from './step-two/step-two.component';
import { TabStepComponent } from './tab-step/tab-step.component';
import {AdDirective} from './_directive/ad.directive';
@NgModule({
  imports: [
    CommonModule,
    DemoModuleRoutingModule
  ],
  declarations: [
    DemoModuleComponent,
    TabStepComponent,
    StepOneComponent,
    StepTwoComponent,
    AdDirective
  ],
  entryComponents: [
    StepOneComponent,
    StepTwoComponent
  ]
})
export class DemoModuleModule { }
