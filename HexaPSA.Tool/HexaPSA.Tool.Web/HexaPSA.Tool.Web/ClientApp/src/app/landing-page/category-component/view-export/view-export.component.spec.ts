import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewExportComponent } from './view-export.component';

describe('ViewExportComponent', () => {
  let component: ViewExportComponent;
  let fixture: ComponentFixture<ViewExportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewExportComponent]
    });
    fixture = TestBed.createComponent(ViewExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
