import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageRoutingModule } from './landing-page-routing.module';
import { LandingPageComponent } from './landing-page.component';
import { EffortestimationComponent } from './category-component/effort-estimation/effortestimation.component';
import { CreateSowComponent } from './category-component/create-sow/create-sow.component';
import { ViewExportComponent } from './category-component/view-export/view-export.component';
import { TimeTrackerComponent } from './category-component/time-tracker/time-tracker.component';
import { TeamConfigurationComponent } from './category-component/team-configuration/team-configuration.component';
import { AppRoutingModule } from '../app-routing.module';
import { MaterialModule } from '../Material/material/material.module';
import { CapacityUtilizationComponent } from './category-component/capacity-utilization/capacity-utilization.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTreeNestedDataSource, MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { TimeDifferencePipe } from '../shared/pipes/time-difference.pipe';
import { TeamPopupComponent } from './category-component/team-configuration/team-popup/team-popup.component';
import { CapacityPopupComponent } from './category-component/capacity-utilization/capacity-popup/capacity-popup.component';
import { NgChartsModule } from 'ng2-charts';
import { TimeTrackerPopupComponent } from './category-component/time-tracker/time-tracker-popup/time-tracker-popup.component';
import { CostByWorkStreamPipe } from '../shared/pipes/cost-by-work-stream.pipe';
import { CreateChildComponent } from './category-component/effort-estimation/create-child/create-child.component';
import { ProjectInfoPopPupComponent } from './category-component/popup/project-info-pop-pup/project-info-pop-pup.component';


@NgModule({
  declarations: [
    LandingPageComponent,
    EffortestimationComponent,
    CreateSowComponent,
    ViewExportComponent,
    TimeTrackerComponent,
    TeamConfigurationComponent,
    CapacityUtilizationComponent,
    TeamPopupComponent,
    CapacityPopupComponent,
    TimeDifferencePipe,
    CostByWorkStreamPipe,
    TimeTrackerPopupComponent,
    CreateChildComponent,
    ProjectInfoPopPupComponent
  ],
  imports: [
    CommonModule,
    LandingPageRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatTreeModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    NgChartsModule,

  ]
})
export class LandingPageModule { }
