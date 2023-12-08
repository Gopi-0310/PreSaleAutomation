import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CapacityPopupComponent } from './capacity-popup.component';

describe('CapacityPopupComponent', () => {
  let component: CapacityPopupComponent;
  let fixture: ComponentFixture<CapacityPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CapacityPopupComponent]
    });
    fixture = TestBed.createComponent(CapacityPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
