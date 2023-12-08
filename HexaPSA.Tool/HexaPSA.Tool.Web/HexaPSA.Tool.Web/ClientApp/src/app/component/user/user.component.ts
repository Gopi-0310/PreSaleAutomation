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
import { TeamConfiguration } from '../../entity/team-configuration.model';
import { TeamPopupComponent } from '../../landing-page/category-component/team-configuration/team-popup/team-popup.component';
import { UserListService } from '../../services/user-list.service';
import { UserList } from '../../entity/user-list.model';
import { UserPopupComponent } from './user-popup/user-popup.component';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent {
  //common...........         
  displayedColumns : string[] = ['name', 'role', 'email'];
  resultsLength    = 0;
  dataSource       = new MatTableDataSource<any>;
  positionOptions  : TooltipPosition = "left";
  hoveredRow       : any;
  rateCard_length  : number = 1; //Initialized for now
  // this variable to assign the model data so it takes the without id it is usefull for reducing duplicate code
  poppupData       : any;
  @ViewChild('matPaginator') paginator   !: MatPaginator;
  @ViewChild('MatSort') sort             !: MatSort;
  public templateRef                     !: TemplateRef<any>;

  constructor(
    private shared         : Shared,
    private matdialog      : MatDialog,
    private userListService: UserListService,
    private _location      : Location,
    private toastrService  : ToastrService
  ) {
    this.shared.setTitle(AppConstants.user);
    this.shared.setFormName(AppConstants.user);
  }


  ngOnInit(): void {
    this.getData();
  }


  //getMethod for accessing initial table value...

  getData() {
    this.userListService.getAll().subscribe((res: UserList) => {
      this.dataSource = new MatTableDataSource(Object.values(res));
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }


  //Getting for popup component data create and edit  ...........
  openDialogTemplateRef(updateData?: UserList) {
    if (updateData != null) {
      this.poppupData = updateData
    } else { this.poppupData = null }

    const dialogRef = this.matdialog.open(UserPopupComponent,
      {
        height: '300px',
        width: '380px',
        data: this.poppupData
      }
    );
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.userListService.save(result.value).subscribe((res) => {
          this.ngOnInit();
          this.toastrService.success(AppConstants.ProjectSucessMessage);
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
    console.log('deleteData', deleteData);
    this.toastrService.info(AppConstants.DeleteMessage);
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
