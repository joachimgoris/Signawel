import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditReportGroupDialogComponent } from './edit-report-group-dialog.component';

describe('EditReportGroupDialogComponent', () => {
  let component: EditReportGroupDialogComponent;
  let fixture: ComponentFixture<EditReportGroupDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditReportGroupDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditReportGroupDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
