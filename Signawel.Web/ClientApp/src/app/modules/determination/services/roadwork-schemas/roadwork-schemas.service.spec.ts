import { TestBed } from '@angular/core/testing';

import { RoadworkSchemasService } from './roadwork-schemas.service';

describe('RoadworkSchemasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoadworkSchemasService = TestBed.get(RoadworkSchemasService);
    expect(service).toBeTruthy();
  });
});
