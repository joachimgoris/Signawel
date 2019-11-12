import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeterminationEndpointListComponent } from './determination-endpoint-list.component';

describe('DeterminationEndpointListComponent', () => {
  let component: DeterminationEndpointListComponent;
  let fixture: ComponentFixture<DeterminationEndpointListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeterminationEndpointListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeterminationEndpointListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
