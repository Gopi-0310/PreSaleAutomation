import { TestBed } from '@angular/core/testing';

import { CostByResourceService } from './cost-by-resource.service';

describe('CostByResourceService', () => {
  let service: CostByResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CostByResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
