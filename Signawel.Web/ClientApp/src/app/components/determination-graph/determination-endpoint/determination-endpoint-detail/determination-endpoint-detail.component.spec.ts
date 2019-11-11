import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeterminationEndpointDetailComponent } from './determination-endpoint-detail.component';

describe('DeterminationEndpointDetailComponent', () => {
  let component: DeterminationEndpointDetailComponent;
  let fixture: ComponentFixture<DeterminationEndpointDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeterminationEndpointDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeterminationEndpointDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
