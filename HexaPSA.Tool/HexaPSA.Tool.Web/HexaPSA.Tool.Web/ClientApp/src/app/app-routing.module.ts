import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { ResourceRatecardComponent } from './component/resource-ratecard/resource-ratecard.component';
import { AppConstants } from 'src/environments/AppConstants';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { CreateSowComponent } from './landing-page/category-component/create-sow/create-sow.component';
import { EffortestimationComponent } from './landing-page/category-component/effort-estimation/effortestimation.component';
import { TeamConfigurationComponent } from './landing-page/category-component/team-configuration/team-configuration.component';
import { TimeTrackerComponent } from './landing-page/category-component/time-tracker/time-tracker.component';
import { ViewExportComponent } from './landing-page/category-component/view-export/view-export.component';
import { RoleComponent } from './component/role/role.component';
import { UserComponent } from './component/user/user.component';
import { ConfirmationComponent } from './component/confirmation/confirmation.component';
import { ProjectTypeComponent } from './component/project-type/project-type.component';
import { TechnologyComponent } from './component/technology/technology.component';



const routes: Routes = [
  {path :'',component:HomeComponent, pathMatch:'full'},
  { path: 'resource', component: ResourceRatecardComponent },
  { path: 'role', component: RoleComponent },
  { path: 'user', component: UserComponent },
  { path: 'project-type', component: ProjectTypeComponent },
  { path: 'technology', component: TechnologyComponent },
  { 
    path: 'landing', loadChildren: () => import('./landing-page/landing-page.module').then(m => m.LandingPageModule) 
  },
  { path: 'VerifyLogin/Confirmation/:id', component: ConfirmationComponent, pathMatch: 'full' },
  // {
  //   path: '', component:LandingPageComponent,
  //   children:[
  //     {path: 'createsow',component:CreateSowComponent},
  //     {path: 'effortestimation',component:EffortestimationComponent},
  //     {path: 'teamconfiguration',component:TeamConfigurationComponent},
  //     {path: 'timetracker',component:TimeTrackerComponent},
  //     {path: 'viewexport',component:ViewExportComponent}
  //   ]
  // }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

