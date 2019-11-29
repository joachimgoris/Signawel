import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PriorityEmailsComponent } from './priority-emails.component';

describe('PriorityEmailsComponent', () => {
  let component: PriorityEmailsComponent;
  let fixture: ComponentFixture<PriorityEmailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PriorityEmailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PriorityEmailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
