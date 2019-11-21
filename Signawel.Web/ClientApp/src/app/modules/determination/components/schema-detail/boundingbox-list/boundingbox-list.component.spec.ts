import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoundingboxListComponent } from './boundingbox-list.component';

describe('BoundingboxListComponent', () => {
  let component: BoundingboxListComponent;
  let fixture: ComponentFixture<BoundingboxListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoundingboxListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoundingboxListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
