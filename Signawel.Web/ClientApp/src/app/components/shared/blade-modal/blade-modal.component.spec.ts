import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BladeModalComponent } from './blade-modal.component';

describe('BladeModalComponent', () => {
  let component: BladeModalComponent;
  let fixture: ComponentFixture<BladeModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BladeModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BladeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
