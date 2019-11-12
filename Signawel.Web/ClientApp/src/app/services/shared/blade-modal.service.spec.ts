import { TestBed } from '@angular/core/testing';

import { BladeModalService } from './blade-modal.service';

describe('BladeModalService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BladeModalService = TestBed.get(BladeModalService);
    expect(service).toBeTruthy();
  });
});
