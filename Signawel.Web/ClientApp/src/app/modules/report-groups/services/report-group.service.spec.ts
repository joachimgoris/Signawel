import { TestBed } from '@angular/core/testing';

import { ReportGroupService } from './report-group.service';

describe('ReportGroupServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ReportGroupService = TestBed.get(ReportGroupService);
    expect(service).toBeTruthy();
  });
});
