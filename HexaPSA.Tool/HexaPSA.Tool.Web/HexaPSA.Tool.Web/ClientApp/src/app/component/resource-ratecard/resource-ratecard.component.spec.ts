import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResourceRatecardComponent } from './resource-ratecard.component';

describe('ResourceRatecardComponent', () => {
  let component: ResourceRatecardComponent;
  let fixture: ComponentFixture<ResourceRatecardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ResourceRatecardComponent]
    });
    fixture = TestBed.createComponent(ResourceRatecardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
