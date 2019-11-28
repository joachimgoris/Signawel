import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddReportGroupComponent } from './add-report-group.component';

describe('AddReportGroupComponent', () => {
  let component: AddReportGroupComponent;
  let fixture: ComponentFixture<AddReportGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddReportGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddReportGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
