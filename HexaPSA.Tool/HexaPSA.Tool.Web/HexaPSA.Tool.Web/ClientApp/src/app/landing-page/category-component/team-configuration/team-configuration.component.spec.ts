import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamConfigurationComponent } from './team-configuration.component';

describe('TeamConfigurationComponent', () => {
  let component: TeamConfigurationComponent;
  let fixture: ComponentFixture<TeamConfigurationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeamConfigurationComponent]
    });
    fixture = TestBed.createComponent(TeamConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
