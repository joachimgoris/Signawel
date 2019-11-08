import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeterminationGraphComponent } from './determination-graph.component';

describe('DeterminationGraphComponent', () => {
  let component: DeterminationGraphComponent;
  let fixture: ComponentFixture<DeterminationGraphComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeterminationGraphComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeterminationGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
