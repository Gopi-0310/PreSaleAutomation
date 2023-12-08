import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './component/home/home.component';
import { ResourceRatecardComponent } from './component/resource-ratecard/resource-ratecard.component';
import { NavbarComponent } from './component/top-nav/navbar/navbar.component';
import { DynamicHeadingComponent } from './component/dynamic-heading/dynamic-heading.component';
import { LandingPageModule } from './landing-page/landing-page.module';
import { PagenotfoundComponent } from './component/page-not-found/pagenotfound.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PopupComponent } from './component/popup/popup.component';
import { MaterialModule } from './Material/material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule,FormsModule} from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { DatePipe } from '@angular/common';
import { NgChartsModule } from 'ng2-charts';
import { MatMenuModule } from '@angular/material/menu';
import { RoleComponent } from './component/role/role.component';
import { RolepopupComponent } from './component/role/rolepopup/rolepopup.component';
import { ProjectPopupComponent } from './component/home/project-popup/project-popup.component';
import { UserComponent } from './component/user/user.component';
import { UserPopupComponent } from './component/user/user-popup/user-popup.component';
import { BreadCrumbsComponent } from './component/bread-crumbs/bread-crumbs.component';
import { ToastrComponentlessModule, ToastrModule } from 'ngx-toastr';
import { FilterPipePipe } from './shared/pipes/filter-pipe.pipe';
import { ProjectTypeComponent } from './component/project-type/project-type.component';
import { TechnologyComponent } from './component/technology/technology.component';
import { ConfirmationComponent } from './component/confirmation/confirmation.component';
import { ProjectTypePopupComponent } from './component/project-type/project-type-popup/project-type-popup.component';
import { TechnologyPopupComponent } from './component/technology/technology-popup/technology-popup.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ResourceRatecardComponent,
    NavbarComponent,
    DynamicHeadingComponent,
    PagenotfoundComponent,
    PopupComponent,
    RoleComponent,
    RolepopupComponent,
    ProjectPopupComponent,
    UserComponent,
    UserPopupComponent,
    BreadCrumbsComponent,
    FilterPipePipe,
    ProjectTypeComponent,
    TechnologyComponent,
    ConfirmationComponent,
    ProjectTypePopupComponent,
    TechnologyPopupComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LandingPageModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    NgChartsModule,
    MatMenuModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
