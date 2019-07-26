/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TabStepComponent } from './tab-step.component';

describe('TabStepComponent', () => {
  let component: TabStepComponent;
  let fixture: ComponentFixture<TabStepComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabStepComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
