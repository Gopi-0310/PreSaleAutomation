import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnologyPopupComponent } from './technology-popup.component';

describe('TechnologyPopupComponent', () => {
  let component: TechnologyPopupComponent;
  let fixture: ComponentFixture<TechnologyPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TechnologyPopupComponent]
    });
    fixture = TestBed.createComponent(TechnologyPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
