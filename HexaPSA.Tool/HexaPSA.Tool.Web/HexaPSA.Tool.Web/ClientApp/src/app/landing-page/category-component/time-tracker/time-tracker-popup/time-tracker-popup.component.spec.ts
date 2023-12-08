import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeTrackerPopupComponent } from './time-tracker-popup.component';

describe('TimeTrackerPopupComponent', () => {
  let component: TimeTrackerPopupComponent;
  let fixture: ComponentFixture<TimeTrackerPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TimeTrackerPopupComponent]
    });
    fixture = TestBed.createComponent(TimeTrackerPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
