import { TestBed } from '@angular/core/testing';

import { PriorityEmailsService } from './priority-emails.service';

describe('PriorityEmailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PriorityEmailsService = TestBed.get(PriorityEmailsService);
    expect(service).toBeTruthy();
  });
});
