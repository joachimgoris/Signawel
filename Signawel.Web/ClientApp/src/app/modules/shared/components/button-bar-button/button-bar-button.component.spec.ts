import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonBarButtonComponent } from './button-bar-button.component';

describe('ButtonBarButtonComponent', () => {
  let component: ButtonBarButtonComponent;
  let fixture: ComponentFixture<ButtonBarButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ButtonBarButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ButtonBarButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
