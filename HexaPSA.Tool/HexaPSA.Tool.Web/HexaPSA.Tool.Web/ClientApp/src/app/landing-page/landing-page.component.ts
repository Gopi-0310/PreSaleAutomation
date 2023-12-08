import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { Shared } from '../shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { NavigationExtras, Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { ApiIntractionsService } from '../services/api-intractions.service';
import { MatTableDataSource } from '@angular/material/table';
import { EstimationDashboardService } from '../services/estimation-dashboard.service';
import { switchMap } from 'rxjs';
import { ProjectService } from '../services/project.service';
import { CostByResourceService } from '../services/cost-by-resource.service';
import { WorkStreamService } from '../services/workstream.service';
import { MatDialog } from '@angular/material/dialog';
import { EstimationDashboard } from '../entity/estimation-dashboard.model';
import { ToastrService } from 'ngx-toastr';
import { ProjectPopupComponent } from '../component/home/project-popup/project-popup.component';
import { ProjectInfoPopPupComponent } from './category-component/popup/project-info-pop-pup/project-info-pop-pup.component';


@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent {

  hours:number = 528;
  weeks:number = this.hours/40;
  workstream:number = 8;
  public navigationData !: any;
  loading: boolean = false;
  projectId : any;
  // Recent Activities
  estimatedBy_data         !:MatTableDataSource<any>;
  estimatedBy_length       : number = 1;
  estimatedBy_columns      : string[] = ['userName','roles']
  hoveredRow               !: Object | null;
  dataSource: any = [];
  project: any;
  totalHoursAndCost : any;
  aggregatedData: { workStreamId: string, rate: string, hours: string, name:string }[] = [];
  totalWeeksAndHours : any;
  projectDetails      : any;
  isRemoved           : boolean = false;
  isEdit              : boolean = false;
  poppupData          : any;
  projectInfo         : any;
constructor(private shared : Shared,
            private router : Router,
            private matdialog : MatDialog,
            private api    : ApiIntractionsService,
            private cdr: ChangeDetectorRef,
            private estimationDashboardAPI: EstimationDashboardService,
            private projectService: ProjectService,
            private service : CostByResourceService,
            private workStreamService: WorkStreamService, 
            private toastrService          : ToastrService,
            ){
            let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
             this.project = currentStateExtras;
           }

  
ngOnInit(): void {
  //this.api.getMethod(environment.landing_data_url).subscribe((res: any) => {
  //  this.dataSource = res;    
  //  })
    this.assignTableData();
   // this.getAllList();
    this.getWorkStreamCalculated();
  }
  
 ngAfterViewInit() : void { }

  assignTableData() {
    this.projectService.getById(this.project.projectId).subscribe((res) => {
      this.navigationData = res;
      this.shared.setTitle(this.navigationData.name);
      this.estimatedBy_data = res.estimatedUsers;
      this.loading = true;
    });
  }
 
  // To-do Need to Obtain color by Status
  getColorByStatus() {
    return "color-completed";
  }
  //

  pageNavigate(pageLink: any) {
    const Url = pageLink;
    let projectId = this.project.projectId;
    let projectName = this.project.projectName;
    let objToSend: NavigationExtras = {};
    objToSend.state = {
      projectId,
      projectName
    };
    switch (Url) {
      case AppConstants.capacityUtilization:
        this.router.navigateByUrl(`landing/capacityutilization`, objToSend);
        break;
      case AppConstants.effortEstimation:
        this.router.navigateByUrl(`landing/effortestimation`, objToSend);
        break;
      case AppConstants.createSOW:
        this.router.navigateByUrl(`landing/createsow`, objToSend);
        break;
      case AppConstants.exportArtifacts:
        this.router.navigateByUrl(`landing/viewexport`, objToSend);
        break;
      case AppConstants.timeTracker:
        this.router.navigateByUrl(`landing/timetracker`, objToSend);
        break;
      case AppConstants.teamConfiguration:
        this.router.navigateByUrl(`landing/teamconfiguration`, objToSend);
        break;
      default:
        this.router.navigateByUrl(`landing`, objToSend);
        break;
    }
}

getWorkStreamCalculated(){
  this.workStreamService.getByIdTotalWeeks(this.project.projectId).subscribe(res =>{
   console.log("total calculations",res);
   this.totalWeeksAndHours = res;
  });
 }

//getting total cost and weeks
aggregateDataByWorkStreamId(data: any) {
  const groupedData: { [key: string]: { rate: string, hours: string, name: string } } = {};
  const totalData: { rate: string, hours: string, week: string} = {
    rate: '0', // Initialize total rate to 0
    hours: '0', // Initialize total hours to 0
    week: '0'
  };
  data.forEach((item: any) => {
    const workStreamId = item.workStream.id;
    totalData.rate = (parseFloat(totalData.rate) + parseFloat(item.rate.rate)).toString();
    totalData.hours = (parseFloat(totalData.hours) + item.hours).toString();
    totalData.week = (parseFloat(totalData.week) + item.week).toString();
    this.totalHoursAndCost = totalData;
    if (groupedData[workStreamId]) {
      groupedData[workStreamId].rate += item.rate.rate;
      groupedData[workStreamId].hours += item.hours;
      groupedData[workStreamId].name = item.workStream.name;
    } else {
      groupedData[workStreamId] = {
        rate: item.rate.rate,
        hours: item.hours,
        name: item.workStream.name
      };
    }
  });

  this.aggregatedData = Object.keys(groupedData).map(workStreamId => ({
    workStreamId: workStreamId,
    rate: groupedData[workStreamId].rate,
    hours: groupedData[workStreamId].hours,
    name: groupedData[workStreamId].name
  }));
}

// getAllList(){
//   this.service.getById(this.project.projectId).subscribe((res:any)=>{
//   this.navigationData = res;
//   })
// }

//project information dialog methods...
 openDialogTemplateRef() {
  // this.isEdit = !!updateData;
  // var id = updateData?.id;
  // this.estimationDashboardAPI.getAll(id).subscribe(async res => {
  //   this.projectDetails = await res;
  //   console.log("res", res)
  // });
  // if (this.navigationData) {

  //   console.log("get return data", this.projectDetails);
  //   if (updateData.id === this.projectDetails.id) {
  //     this.poppupData = this.projectDetails;
  //   }
  // } else {
  //   this.poppupData = null;
  // }

      this.shared.projectId.subscribe(res=>{
        console.log("projectID",res);
        this.projectService.getById(res).subscribe((resId) => {
          
           console.log("projectID",resId);
       });
      //  console.log("projectID",resId);
      })
     
 
  
  const dialogRef = this.matdialog.open(ProjectInfoPopPupComponent, {
    height: '638px',
    width: '450px',
    data: this.navigationData 
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      console.log(result)
      if (result.id != null) {
        var res = JSON.stringify(result);
        this.estimationDashboardAPI.update(result.id, res).subscribe(updatedData => {
          this.ngOnInit();
          this.toastrService.success(AppConstants.ProjectSucessMessage, result.name + '' + "Project");
        });
      } else {
        // this.estimationDashboardAPI.save(result).subscribe(result => {
        //   this.ngOnInit();
        //   this.toastrService.success(AppConstants.ProjectSucessMessage, result.name + '' + "Project");
        // });
      }
    }
  });
}

}
