import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page.component';
import { CreateSowComponent } from './category-component/create-sow/create-sow.component';
import { EffortestimationComponent } from './category-component/effort-estimation/effortestimation.component';
import { TeamConfigurationComponent } from './category-component/team-configuration/team-configuration.component';
import { TimeTrackerComponent } from './category-component/time-tracker/time-tracker.component';
import { ViewExportComponent } from './category-component/view-export/view-export.component';
import { CapacityUtilizationComponent} from './category-component/capacity-utilization/capacity-utilization.component';

const routes: Routes = [
  { path :'',component:LandingPageComponent, pathMatch:'full'},
  { path :'landing/capacityutilization', component: CapacityUtilizationComponent },
  { path :'landing/createsow', component : CreateSowComponent},
  { path :'landing/effortestimation', component : EffortestimationComponent},
  { path :'landing/teamconfiguration', component : TeamConfigurationComponent},
  { path :'landing/timetracker', component : TimeTrackerComponent},
  { path :'landing/viewexport', component : ViewExportComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LandingPageRoutingModule { }
