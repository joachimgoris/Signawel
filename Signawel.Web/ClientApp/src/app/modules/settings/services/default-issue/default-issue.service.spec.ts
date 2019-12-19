import { TestBed } from '@angular/core/testing';

import { DefaultIssueService } from './default-issue.service';

describe('DefaultIssueService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DefaultIssueService = TestBed.get(DefaultIssueService);
    expect(service).toBeTruthy();
  });
});
