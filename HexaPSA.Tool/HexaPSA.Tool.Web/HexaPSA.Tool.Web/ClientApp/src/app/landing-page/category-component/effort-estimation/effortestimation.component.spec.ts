import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EffortestimationComponent } from './effortestimation.component';

describe('EffortestimationComponent', () => {
  let component: EffortestimationComponent;
  let fixture: ComponentFixture<EffortestimationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EffortestimationComponent]
    });
    fixture = TestBed.createComponent(EffortestimationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
