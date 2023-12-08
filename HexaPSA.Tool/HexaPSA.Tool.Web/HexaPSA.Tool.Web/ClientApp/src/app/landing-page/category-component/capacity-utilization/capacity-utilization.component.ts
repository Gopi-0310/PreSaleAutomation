import { Component, TemplateRef, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AppConstants } from 'src/environments/AppConstants';
import { MatSort } from '@angular/material/sort';
import { environment } from '../../../../environments/environment';
import { TooltipPosition } from '@angular/material/tooltip';
import { Shared } from 'src/app/shared/shared';
import { CapacityPopupComponent } from './capacity-popup/capacity-popup.component';
import { CapacityUtilizationService } from '../../../services/capacity-utilization.service';
import { CapacityUtilizationMapping } from '../../../entity/capacity-utilization-mapping.model';
import { CapacityUtilization } from 'src/app/entity/capacity-utilization.model';
import { ResourceRatecardService } from 'src/app/services/resource-ratecard.service';
import { ResourceRatecard } from 'src/app/entity/resource-ratecard.model';
import { NavigationExtras, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-capacity-utilization',
  templateUrl: './capacity-utilization.component.html',
  styleUrls: ['./capacity-utilization.component.scss'],
})
export class CapacityUtilizationComponent {
  resultsLength = 0;
  project: any;
  @ViewChild('matPaginator') paginator  !: MatPaginator;
  @ViewChild('MatSort') sort            !: MatSort;
  public templateRef                    !: TemplateRef<any>;
  hoveredRow: any;
  poppupData: any;
  listOfData: any;
  dataSource = new MatTableDataSource<any>;
  positionOptions: TooltipPosition = "left";
  rateCard_length: number = 1;
  displayedColumns: string[] = ['code','role', 'hours', 'rate', 'location','actions'];
  capacityModel   !: CapacityUtilization;
  resourceModel   !: ResourceRatecard;
  mappingModel    !: CapacityUtilizationMapping;
  constructor(
    private router: Router,
    private shared: Shared,
    private matdialog: MatDialog,
    private capacityUtilizationService: CapacityUtilizationService,
    private resourceRateService: ResourceRatecardService,
    private _location: Location,
    private toastrService: ToastrService
  ) {
    this.shared.setTitle(AppConstants.capacityUtilization);
    this.shared.setFormName(AppConstants.capacityUtilization);
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    var data :any = currentStateExtras;
    this.project = data;
  }

  ngOnInit(): void {
    this.getListOfData();
  }

  getListOfData() {
    this.capacityUtilizationService.getAll(this.project.projectId)
      .subscribe((res) => {
        console.log("res",res);
        //this.listOfData = Object.values(res).filter((x: { projectId: string; }) => x.projectId == this.projectId);
        this.dataSource = new MatTableDataSource(Object.values(res));
      });
  }

  openDialogTemplateRef(updateData?: CapacityUtilizationMapping) {
    if (updateData != null) {
      this.poppupData = updateData
    } else { this.poppupData = this.project.projectId }

    const dialogRef = this.matdialog.open(CapacityPopupComponent,
      {
        height: '350px',
        width: '380px',
        data: this.poppupData
      }
    );
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.capacityUtilizationService.getAll(this.project.projectId).subscribe((res) => {
          if ( updateData != null && Object.values(res).find((x: any) => x.id == updateData.id)) {
            var queryParams = '/' + updateData.id;
            this.capacityModel ={
              id         : updateData.id,
              projectId  : this.project.projectId,
              roleId     : result.roleId.id,
              hours      : result.hours,
              location   : result.location
            }

            this.capacityUtilizationService.update(queryParams,this.capacityModel).subscribe((res) => {
                 var queryParams = "/"+ this.poppupData.resourceRate.id;
                 this.mappingModel ={
                  id        : this.poppupData.resourceRate.id,
                  projectId : this.project.projectId,
                  roleId    : result.roleId.id,
                  rate      : result.rate,
                  location  : result.location
                 }
                 this.resourceRateService.update(queryParams,this.mappingModel).subscribe(res=>{
                 this.getListOfData();
                 this.toastrService.success(AppConstants.UpdateSuccsessMessage)
                 });
              });
          } else {
            result.projectId = this.project.projectId;
            this.capacityUtilizationService.save(result).subscribe((res) => {
                this.getListOfData();
                this.toastrService.success(AppConstants.ProjectSucessMessage)
              });
          }
        });
      }
    });
  }

  applyFilter(event: Event) {
    const filter_Data = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filter_Data.trim().toLocaleLowerCase();
    if (this.dataSource.filter) {
      this.dataSource.paginator?.firstPage();
    }
  }

  remove(deleteData: CapacityUtilizationMapping) {
    var queryParams = '/' + deleteData.id;
    this.capacityUtilizationService.remove(queryParams)
      .subscribe((res) => {
        this.getListOfData();
        this.toastrService.info(AppConstants.DeleteMessage)
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
