import { TestBed } from '@angular/core/testing';

import { SuccessesHubService } from './successes-hub.service';

describe('SuccessesHubService', () => {
  let service: SuccessesHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SuccessesHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
