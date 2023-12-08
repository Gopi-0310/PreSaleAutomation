import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectInfoPopPupComponent } from './project-info-pop-pup.component';

describe('ProjectInfoPopPupComponent', () => {
  let component: ProjectInfoPopPupComponent;
  let fixture: ComponentFixture<ProjectInfoPopPupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectInfoPopPupComponent]
    });
    fixture = TestBed.createComponent(ProjectInfoPopPupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
