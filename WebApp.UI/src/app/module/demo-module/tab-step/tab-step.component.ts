import { Component, OnInit, ComponentFactoryResolver, ViewChild, ViewContainerRef, OnDestroy } from '@angular/core';
import { AdItem } from '../AdItem';
import { StepOneComponent } from '../step-one/step-one.component';
import {AdDirective} from '../_directive/ad.directive';
import { AdComponent } from '../AdComponent';
import { StepTwoComponent } from '../step-two/step-two.component';

@Component({
  selector: 'app-tab-step',
  templateUrl: './tab-step.component.html',
  styleUrls: ['./tab-step.component.css']
})

export class TabStepComponent implements OnInit, OnDestroy {
  ads: AdItem[];
  data: { name: 'parent tab' };
  tabIndex: 0;

  @ViewChild(AdDirective) adHost: AdDirective;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) {
  }

  ngOnDestroy() {
  }

  onSubmit(data: any) {
    if (this.tabIndex < (this.ads.length - 1)) {
      this.tabIndex++;
    } else {
      this.tabIndex = 0;
    }

    this.getcomponent();
    console.log(data);
  }

  getcomponent() {
    const step = this.ads[this.tabIndex];

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(step.component);

    const viewContainerRef = this.adHost.viewContainerRef;
    viewContainerRef.clear();
    const componentRef = viewContainerRef.createComponent(componentFactory);
    const tabComponent = (componentRef.instance as AdComponent);
    tabComponent.eSubmit.subscribe(data => this.onSubmit(data));
    tabComponent.data = step.data;
  }

  ngOnInit() {
    this.tabIndex = 0;
    this.ads = [
      new AdItem(StepOneComponent, { name: 'step one'}),
      new AdItem(StepTwoComponent, { name: 'step two'})
    ];
    this.getcomponent();
  }

}
