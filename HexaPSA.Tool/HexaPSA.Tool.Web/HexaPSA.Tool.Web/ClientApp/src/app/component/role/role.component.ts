import { Component, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { TooltipPosition } from '@angular/material/tooltip'
import { RolepopupComponent } from './rolepopup/rolepopup.component';
import { Role } from '../../entity/role.model';
import { RoleService } from '../../services/role-services';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent {
  //common...........         
  displayedColumns   : string[] =  ['code', 'role', 'actions'];
  resultsLength      : number =  0;
  dataSource         = new MatTableDataSource<any>;
  model             !: Role;
  positionOptions    : TooltipPosition = "left";
  hoveredRow         : any;
  poppupData         : any;
  rateCard_length    : number = 1; 
  @ViewChild('matPaginator') paginator  !: MatPaginator;
  @ViewChild('MatSort') sort            !: MatSort;
  public templateRef                    !: TemplateRef<any>;

  constructor(
    private shared      : Shared,
    private matdialog   : MatDialog,
    private roleService : RoleService,
    private _location   : Location,
    private toastrService : ToastrService
  ) {
    this.shared.setTitle(AppConstants.role);
    this.shared.setFormName(AppConstants.role);
  }

  ngOnInit(): void {
    this.resorceTableData();
  }

  resorceTableData() {
    this.roleService.getAll().subscribe(res => {
      this.dataSource = new MatTableDataSource(Object.values(res));
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      //this.resultsLength = res.length;
    })
  }

  //Getting for popup component data create and edit  ...........
  openDialogTemplateRef(updateData?: Role) {
    if (updateData != null) {
      this.poppupData = updateData;
    } else { this.poppupData = null }

    const dialogRef = this.matdialog.open(RolepopupComponent,
      {
        height: '380px',
        width: '450px',
        data: this.poppupData
      }
    );
    dialogRef.afterClosed().subscribe(result => {
      this.model = result.value;
      if (this.model) {
        this.roleService.getAll().subscribe((res: Role) => {
          if (updateData != null && Object.values(res).find(x => x.id == updateData.id)) {
            var queryParams = "/" + updateData.id;
            this.roleService.update(queryParams, this.model).subscribe(res => {
              this.resorceTableData();
              this.toastrService.success(AppConstants.UpdateSuccsessMessage);
            });
          }
          else {
            if (Object.values(res).find(x => x.code == result.code)) { alert("user Exits"); }
            else {
              this.roleService.save(this.model).subscribe(res => {
                this.resorceTableData();
                this.toastrService.success(AppConstants.ProjectSucessMessage);
              })
            }
          }
        })
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
  remove(deleteData: Role) {
    console.log(deleteData)
    var queryParams = "/" + deleteData.id;
    this.roleService.remove(queryParams).subscribe(res => {
      this.resorceTableData();
      this.toastrService.info(AppConstants.DeleteMessage);
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

  columnSearch() {
    return false;
  }
  backClicked() {
    this._location.back();
  }
}
