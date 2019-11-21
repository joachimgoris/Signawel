import { TestBed } from "@angular/core/testing";

import { DeterminationGraphService } from "./determination-graph.service";

describe("DeterminationGraphServiceService", () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it("should be created", () => {
    const service: DeterminationGraphService = TestBed.get(
      DeterminationGraphService
    );
    expect(service).toBeTruthy();
  });
});
