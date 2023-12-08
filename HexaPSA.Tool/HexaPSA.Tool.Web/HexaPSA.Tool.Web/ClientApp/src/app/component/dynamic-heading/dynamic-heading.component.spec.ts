import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicHeadingComponent } from './dynamic-heading.component';

describe('DynamicHeadingComponent', () => {
  let component: DynamicHeadingComponent;
  let fixture: ComponentFixture<DynamicHeadingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DynamicHeadingComponent]
    });
    fixture = TestBed.createComponent(DynamicHeadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
