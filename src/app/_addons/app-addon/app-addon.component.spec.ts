import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppAddonComponent } from './app-addon.component';

describe('AppAddonComponent', () => {
  let component: AppAddonComponent;
  let fixture: ComponentFixture<AppAddonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppAddonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppAddonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
