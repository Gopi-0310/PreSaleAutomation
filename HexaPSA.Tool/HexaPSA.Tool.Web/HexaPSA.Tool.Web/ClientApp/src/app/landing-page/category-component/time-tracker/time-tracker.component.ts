import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { environment } from 'src/environments/environment';
import { TooltipPosition } from '@angular/material/tooltip'
import { TimeTrackerPopupComponent } from './time-tracker-popup/time-tracker-popup.component';
import { TimeTracker } from '../../../entity/time-tracker.model';
import { PresaleTimeTrackerService } from '../../../services/presale-time-tracker.service';
import { ProjectService } from '../../../services/project.service';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-time-tracker',
  templateUrl: './time-tracker.component.html',
  styleUrls: ['./time-tracker.component.scss'],
})


export class TimeTrackerComponent {
  //common...........         
  displayedColumns: string[] = ['name', 'project','activity', 'description','hours','date','actions' ];
  resultsLength = 0;
  navigation: string;
  dataSource = new MatTableDataSource<any>;
  positionOptions: TooltipPosition = "left";
  hoveredRow: any;
  rateCard_length: number = 1; //Initialized for now
  // this variable to assign the model data so it takes the without id it is usefull for reducing duplicate code
  poppupData: any;
  @ViewChild('matPaginator') paginator     !: MatPaginator;
  @ViewChild('MatSort') sort          !: MatSort;
  public templateRef   !: TemplateRef<any>;
  projectList: any;
  selectedProjectId?: string;
 

  constructor(
    private shared: Shared,
    private matdialog: MatDialog,
    private api: ApiIntractionsService,
    private presaleTimeTrackerService: PresaleTimeTrackerService,
    private projectService: ProjectService,
    private _location: Location,
    private toastrService: ToastrService
  ) {
    this.shared.setTitle(AppConstants.timeTracker);
    this.shared.setFormName(AppConstants.timeTracker);
    this.navigation = this.api.getItem('projectDetails');
  
  }


  ngOnInit(): void {
    this.getData();
    this.getAllProject();
  }


  //getMethod for accessing initial table value...

  getData() {
    this.presaleTimeTrackerService.getAll().subscribe(res => {
     this.dataSource = new MatTableDataSource(Object.values(res));
     this.dataSource.sort = this.sort;
     this.dataSource.paginator = this.paginator;
    });
  }

  getAllProject() {
    this.projectService.getAll().subscribe({
      next: (res: any) => {
        console.log(res)
        this.projectList = res;
      },
      error: () => {
       this.toastrService.error(AppConstants.ErrorMessage);
      }
    })
  }

  onChanges(projectDetails:any) {
    console.log(projectDetails.id,"projectDetails",projectDetails);
    this.selectedProjectId = projectDetails.id;
    this.presaleTimeTrackerService.getAll().subscribe(res => {
      var arrayList = [];
      for (var i = 0; i < res.length; i++) {
        if (projectDetails.id == res[i].projectId) {
          arrayList.push(res[i]);
        }
        this.dataSource = new MatTableDataSource(Object.values(arrayList));
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        
        console.log("id",this.selectedProjectId);
      }
    });
  }

  //Getting for popup component data create and edit  ...........
  openDialogTemplateRef(updateData?: any) {
    console.log('updateData', updateData)
    console.log("id",this.selectedProjectId);
    if (updateData) {
      this.poppupData = {
        "id": updateData.id,
        "userId": updateData.user.id,
        "activity": updateData.activity,
        "description": updateData.description,
        "hours": updateData.hours,
        "projectId": updateData.projectId,
        "activityDate": updateData.activityDate
      }
    } else {
      this.poppupData =  {
        "projectId": this.selectedProjectId ? this.selectedProjectId : null,
      }
}

    const dialogRef = this.matdialog.open(TimeTrackerPopupComponent,
      {
        height: '300px',
        width: '380px',
        data: this.poppupData
      }
    );
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.presaleTimeTrackerService.getAll()
          .subscribe((res) => {
            if (
              updateData != null &&
              Object.values(res).find((x: any) => x.id == updateData.id)
            ) {
              var queryParams = '/' + updateData.id;
              this.presaleTimeTrackerService.update(queryParams, result)
                .subscribe((res) => {
                  this.getData();
                  this.toastrService.success(AppConstants.ProjectSucessMessage);
                });
            } else {
              this.presaleTimeTrackerService.save(result)
                .subscribe((res) => {
                  this.getData();
                  this.toastrService.success(AppConstants.ProjectSucessMessage);
                });
            }
          });
      }
    });
  }

  //Filter the table content.............
  applyFilter(event: Event) {
    const filter_Data = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filter_Data.trim().toLocaleLowerCase();
    if (this.dataSource.filter) {
      this.dataSource.paginator?.firstPage();
    }
  }
  //remove method..................
  remove(deleteData: TimeTracker) {
    var queryParams = '/' + deleteData.id;
    this.presaleTimeTrackerService.remove(queryParams)
      .subscribe((res) => {
        this.getData();
        this.toastrService.info(AppConstants.DeleteMessage);
      });
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

  columnSearch() {
    return false;
  }

  backClicked() {
    this._location.back();
  }
}





