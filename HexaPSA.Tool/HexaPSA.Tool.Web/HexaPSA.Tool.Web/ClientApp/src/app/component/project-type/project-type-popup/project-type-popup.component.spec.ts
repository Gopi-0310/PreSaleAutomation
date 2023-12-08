import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTypePopupComponent } from './project-type-popup.component';

describe('ProjectTypePopupComponent', () => {
  let component: ProjectTypePopupComponent;
  let fixture: ComponentFixture<ProjectTypePopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectTypePopupComponent]
    });
    fixture = TestBed.createComponent(ProjectTypePopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
