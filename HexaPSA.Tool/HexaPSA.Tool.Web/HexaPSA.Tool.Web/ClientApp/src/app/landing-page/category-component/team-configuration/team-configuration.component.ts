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
import { TeamPopupComponent } from './team-popup/team-popup.component';
import { TeamConfiguration } from '../../../entity/team-configuration.model';
import { Location } from '@angular/common';
import { NavigationExtras, Router } from '@angular/router';
import { TeamConfigurationService } from '../../../services/team-configuration.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-team-configuration',
  templateUrl: './team-configuration.component.html',
  styleUrls: ['./team-configuration.component.scss'],
})
export class TeamConfigurationComponent {
  //common...........         
  displayedColumns: string[] = ['name', 'role', 'actions'];
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
  project: any;

  constructor(
    private router: Router,
    private shared: Shared,
    private matdialog: MatDialog,
    private api: ApiIntractionsService,
    private _location: Location,
    private teamConfigurationService: TeamConfigurationService,
    private toastrService: ToastrService
  ) {
    this.shared.setTitle(AppConstants.teamConfiguration);
    this.shared.setFormName(AppConstants.teamConfiguration);
    this.navigation = this.api.getItem('projectDetails');
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    var data: any = currentStateExtras;
    this.project = data;
  }


  ngOnInit(): void {
    this.getData();
  }


  //getMethod for accessing initial table value...

  getData() {
    this.teamConfigurationService.getbyID(this.project.projectId).subscribe((res: TeamConfiguration) => {
        this.dataSource = new MatTableDataSource(Object.values(res));
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }


  //Getting for popup component data create and edit  ...........
  openDialogTemplateRef(updateData?: TeamConfiguration) {
    if (updateData != null) {
      this.poppupData = updateData
    } else { this.poppupData = this.project.projectId }

    const dialogRef = this.matdialog.open(TeamPopupComponent,
      {
        height: '300px',
        width: '380px',
        data: this.poppupData
      }
    );
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.teamConfigurationService.getbyID(this.project.projectId)
          .subscribe((res) => {
            if (
              updateData != null &&
              Object.values(res).find((x: any) => x.id == updateData.id)
            ) {
              var queryParams = '/' + updateData.id;
              this.teamConfigurationService.update(
                  queryParams,
                  result
                )
                .subscribe((res) => {
                  this.getData();
                  this.toastrService.success(AppConstants.ProjectSucessMessage);
                });
            } else {
              this.teamConfigurationService.save(result)
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
  remove(deleteData: TeamConfiguration) {
    var queryParams = deleteData.id?deleteData.id : '';
    this.teamConfigurationService.remove(queryParams)
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





