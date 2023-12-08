import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamPopupComponent } from './team-popup.component';

describe('TeamPopupComponent', () => {
  let component: TeamPopupComponent;
  let fixture: ComponentFixture<TeamPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeamPopupComponent]
    });
    fixture = TestBed.createComponent(TeamPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
