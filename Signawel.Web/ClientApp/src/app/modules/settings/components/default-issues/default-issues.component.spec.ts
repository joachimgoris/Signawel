import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultIssuesComponent } from './default-issues.component';

describe('DefaultIssuesComponent', () => {
  let component: DefaultIssuesComponent;
  let fixture: ComponentFixture<DefaultIssuesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultIssuesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
