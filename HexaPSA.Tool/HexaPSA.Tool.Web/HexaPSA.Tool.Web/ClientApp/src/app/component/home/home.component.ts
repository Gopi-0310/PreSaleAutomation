import { ChangeDetectorRef, Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { TooltipPosition } from '@angular/material/tooltip'
import { DatePipe } from '@angular/common';
import { Shared } from 'src/app/shared/shared';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { AppConstants } from 'src/environments/AppConstants';
import { environment } from 'src/environments/environment';
import { MatTableDataSource } from '@angular/material/table';
import { ProjectPopupComponent } from './project-popup/project-popup.component';
import { EstimationDashboardService } from '../../services/estimation-dashboard.service';
import { EstimationDashboard } from '../../entity/estimation-dashboard.model';
import { ProjectService } from '../../services/project.service';
import { ToastrService } from 'ngx-toastr';
import { ViewExportService } from 'src/app/services/view-export.service';
import { async } from 'rxjs';
import { RecentActivityLog } from '../../entity/recent-activity.model';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, DoCheck {

  //Project Estimation Table
  projectEstimations_data     !: MatTableDataSource<any>;
  projectEstimations_length    : number = 1; //Initialized for now
  projectEstimations_columns   : string[] = ['name', 'projectTypes', 'actions'];
 
  // Recent Activities
  recentActivities_data       !: MatTableDataSource<any>;
  recentActivities_length      : number = 1;
  recentActivities_columns     : string[] = ['projectName', 'activity', 'CreatedDate'];
  recentExported_data         !: MatTableDataSource<any>;
  recentExported_length        : number = 1;
  recentExported_columns       : string[] = ['projectName', 'Activity', 'CreatedDate']
  projectDetails      : any;
  hoveredRow          : any;
  isRemoved           : boolean = false;
  isEdit              : boolean = false;
  panelOpenState       = false;
  expandedAccordion   : boolean = true;
  hidenDot            : string = "......";
  p                  !: number;
  model              !: EstimationDashboard;
  // this variable to assign the model data so it takes the without id it is usefull for reducing duplicate code
  poppupData    : any;
  dashBoardData : any;
  positionOptions: TooltipPosition = "left";
  //update
  project         : any;
  dashBoardModel !: EstimationDashboard;


  constructor(protected shared: Shared,
    private api       : ApiIntractionsService,
    private matdialog : MatDialog,
    protected router  : Router,
    private datePipe  : DatePipe,
    private cdr       : ChangeDetectorRef,
    private estimationDashboardAPI : EstimationDashboardService,
    private projectService         : ProjectService,
    private toastrService          : ToastrService,
    private viewExportService      : ViewExportService,

  ) {
    this.shared.setTitle(AppConstants.homePage);
    this.shared.setFormName(AppConstants.homePage);
  }
  ngDoCheck(): void {

  }
  ngOnChanges(changes: SimpleChanges): void {

  }


  ngOnInit(): void {
    this.assignTableData();
    this.getRecentActivities();
    this.getRecentExportActivities();
  }



  getColorByStatus(data: string) {
    switch (data) {
      case "Completed": return "color-completed-bg";
      case "In-Progress": return "color-in-progress-bg";
      case "On-Hold": return "color-on-hold-bg";
      case "To-do": return "color-to-do-bg";
      default: return "";
    }
  }
  columnSearch() {
    return false;
  }
  assignTableData() {
    // For now
    this.recentActivities_data = new MatTableDataSource([{ "projectName": "Presales Automation", "activity": "Capacity Utilization", "id": 1 }, { "projectName": "EHPN", "activity": "Effort Estimation Module", "id": 2 }, { "projectName": "EHPN", "activity": "Create SOW", "id": 3 }, { "projectName": "EHPN", "activity": "Create SOW", "id": 4 }, { "projectName": "EHPN", "activity": "Create SOW", "id": 5 }])
    this.recentExported_data = new MatTableDataSource([
      {
        "projectName": "Presales Automation",
        "activity": "Capacity Utilization",
        "CreatedDate": "11-11-2023",
        "id": 1
      },
      {
        "projectName": "EHPN",
        "activity": "Effort Estimation Module",
        "CreatedDate": "11-11-2023",
        "id": 2
      },
      {
        "projectName": "EHPN",
        "activity": "Create SOW",
        "CreatedDate": "11-11-2023",
        "id": 3
      },
      {
        "projectName": "EHPN",
        "activity": "Create SOW",
        "CreatedDate": "11-11-2023",
        "id": 4
      },
      {
        "projectName": "EHPN",
        "activity": "Create SOW",
        "CreatedDate": "11-11-2023",
        "id": 5
      }
    ])
    this.projectService.getAll().subscribe({
      next: (res: any) => {
        this.projectEstimations_length = res.length;
        this.projectEstimations_data = new MatTableDataSource(Object.values(res));
        this.cdr.detectChanges();
      },
      error: () => {
        //Handle errors
      }
    })
  }

  getRecentExportActivities() {
    this.viewExportService.getRecentExportActivities().subscribe({
      next: (res: any) => {
        this.recentExported_length = res.length;
        this.recentExported_data = new MatTableDataSource(Object.values(res));
        this.cdr.detectChanges();
      },
      error: () => {
        //Handle errors
      }
    })
  }

  getRecentActivities() {
    this.viewExportService.getRecentActivities().subscribe({
      next: (res: RecentActivityLog) => {
        //this.recentActivities_length = res.length;
        this.recentActivities_data = new MatTableDataSource(Object.values(res));
        this.cdr.detectChanges();
      },
      error: () => {
        //Handle errors
      }
    })
  }

  isHovered(item: Object) {
    return this.hoveredRow === item;
  }

  setHovered(item: Object, isHovered: boolean) {
    if (isHovered) {
      this.hoveredRow = item;
    } else {
      this.hoveredRow = null;
    }
  }

  
 
  // set the progress bar with ...........
  data(status: string) {
    return "width:" + status;
  }


  // filter the value..........................
  applyFilter(event: Event) {
    const fillter_Value = (event.target as HTMLInputElement).value
    this.dashBoardData.filter = fillter_Value.trim().toLocaleLowerCase();
  }

  async openDialogTemplateRef(updateData?: EstimationDashboard) {
    this.isEdit = !!updateData;
    var id = updateData?.id;
    this.estimationDashboardAPI.getAll(id).subscribe(async res => {
      this.projectDetails = await res;
      console.log("res", res)
    });
    if (updateData) {

      console.log("get return data", this.projectDetails);
      if (updateData.id === this.projectDetails.id) {
        this.poppupData = this.projectDetails;
      }
    } else {
      this.poppupData = null;
    }

    const dialogRef = this.matdialog.open(ProjectPopupComponent, {
      height: '638px',
      width: '450px',
      data: this.poppupData
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
          this.estimationDashboardAPI.save(result).subscribe(result => {
            this.ngOnInit();
            this.toastrService.success(AppConstants.ProjectSucessMessage, result.name + '' + "Project");
          });
        }
      }
    });
  }

  async recentActivites(row?: RecentActivityLog) {
    const Url = row?.activity;
    let projectId = row?.projectId;
    let objToSend: NavigationExtras = {};
    objToSend.state = {
      projectId,
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
      case AppConstants.rateCard:
        this.router.navigateByUrl(`resource/`);
        break;
      default:
        this.router.navigateByUrl('');
        break;
    }
  }

  //routing landing page respective project name
  // Used for re-design
  landingPage(projectDetails: EstimationDashboard) {
    let projectName = projectDetails.name;
    let projectId = projectDetails.id;
    this.shared.setProjectId(projectDetails.id);
    /*this.router.navigateByUrl('/landing');*/
    let objToSend: NavigationExtras = {};
    objToSend.state = {
      projectId,
      projectName
    };
    if (!this.isRemoved && !this.isEdit) {
      this.router.navigateByUrl('/landing', objToSend);
    }
    else {
      this.isRemoved = false;
      this.isEdit = false;
    }
  }

  //remove method.............
  remove(deleteData: EstimationDashboard) {
    this.isRemoved = true;
    var queryParams = "/" + deleteData.id;
    this.estimationDashboardAPI.remove(queryParams).subscribe(res => {
      this.assignTableData();
      this.toastrService.info(AppConstants.DeleteMessage);
    })
  }

  // Function to return the content for the tooltip
  getTooltipContent(data: EstimationDashboard) {
    return `
      Project Type: ${data.projectTypeId}${"......."}
      Technology  : ${data.technologies}${"......."}
      userName    : ${data.estimatedUsers[0].userId}
    `;
  }
}
