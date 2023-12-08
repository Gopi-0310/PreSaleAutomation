import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CapacityUtilizationComponent } from './capacity-utilization.component';

describe('CapacityUtilizationComponent', () => {
  let component: CapacityUtilizationComponent;
  let fixture: ComponentFixture<CapacityUtilizationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CapacityUtilizationComponent]
    });
    fixture = TestBed.createComponent(CapacityUtilizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
