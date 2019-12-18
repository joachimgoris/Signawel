import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlacklistEmailsComponent } from './blacklist-emails.component';

describe('BlacklistEmailsComponent', () => {
  let component: BlacklistEmailsComponent;
  let fixture: ComponentFixture<BlacklistEmailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlacklistEmailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlacklistEmailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
