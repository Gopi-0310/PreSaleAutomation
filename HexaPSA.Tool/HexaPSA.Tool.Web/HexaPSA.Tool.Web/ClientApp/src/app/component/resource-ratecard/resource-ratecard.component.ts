import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { PopupComponent } from '../popup/popup.component';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { environment } from 'src/environments/environment';
import { ResourceRateCard } from 'src/app/model/resource-rate-card';
import {TooltipPosition} from '@angular/material/tooltip'
import { ResourceRatecard } from 'src/app/entity/resource-ratecard.model';
import { ResourceRatecardService } from 'src/app/services/resource-ratecard.service';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-resource-ratecard',
  templateUrl: './resource-ratecard.component.html',
  styleUrls: ['./resource-ratecard.component.scss']
})
export class ResourceRatecardComponent{
  //common...........         
  displayedColumns      :  string[] = ['code', 'role','rate','actions'];
  resultsLength         =  0;
  dataSource            =  new MatTableDataSource<any>;
  model                !: ResourceRatecard;
  hoveredRow            : any;
  rateCard_length       : number = 1; //Initialized for now
  // this variable to assign the model data so it takes the without id it is usefull for reducing duplicate code
  poppupData            : any;
  listOfData            : any;
  @ViewChild('matPaginator')  paginator     !:  MatPaginator;
  @ViewChild('MatSort')       sort          !: MatSort;
  public                      templateRef   !: TemplateRef<any>;

  constructor(
    private shared         : Shared, 
    private matdialog      : MatDialog,
    private resourceRateService: ResourceRatecardService,
    private _location      : Location,
    private toastrService  : ToastrService
    ){
      this.shared.setTitle(AppConstants.rateCard);
      this.shared.setFormName(AppConstants.rateCard);
    }

  
  ngOnInit(): void {this.resorceTableData();}
   

  //getMethod for accessing initial table value...
  resorceTableData(){
    this.resourceRateService.getAll().subscribe(res=>{
      this.listOfData = res;
      this.listOfData = Object.values(res).filter((x: { projectId: string; }) => x.projectId == null);
      this.dataSource =  new MatTableDataSource(Object.values(this.listOfData)); 
      this.dataSource.sort = this.sort; 
      this.dataSource.paginator = this.paginator;  
     })
  }

  //Getting for popup component data create and edit  ...........
  openDialogTemplateRef(updateData? : ResourceRatecard){
     if(updateData != null)
          { this.poppupData = updateData }
     else { this.poppupData =  null}

    const dialogRef = this.matdialog.open(PopupComponent, 
      {
        height  : '300px',
        width   : '380px',
        data    : this.poppupData
      });
       dialogRef.afterClosed().subscribe( result =>  {
        if(result) 
        {
          let rate = result.rate;
          let newRate = parseInt(rate);

            this.resourceRateService.getAll().subscribe((res : ResourceRatecard) =>{
              if(updateData != null && Object.values(res).find(x=> x.id == updateData.id))
                { 
                  this.model = {
                    id     : updateData.id,
                    rate: newRate,
                    roleId : result.code
                  }
                  var queryParams = "/"+ updateData.id;
                  this.resourceRateService.update(queryParams,this.model).subscribe(res=>{
                  this.resorceTableData(); 
                  this.toastrService.success(AppConstants.UpdateSuccsessMessage);
                });
                }
                else if(Object.values(res).find(x=> x.role.name == result.role))
                 {
                  alert("already exits")
                 } 
                 else{
                   this.model = {
                    roleId  : result.code,
                    rate: newRate }
                    this.resourceRateService.save(this.model).subscribe(res =>{
                    this.resorceTableData(); 
                    this.toastrService.success(AppConstants.ProjectSucessMessage);
                    })
                 }
                 
            })     
          }
      });
  }

  //Filter the table content.............
  applyFilter(event:Event){
     const filter_Data = (event.target as HTMLInputElement).value;
         this.dataSource.filter = filter_Data.trim().toLocaleLowerCase();
            if(this.dataSource.filter)
              {
                this.dataSource.paginator?.firstPage();
              }
  }

  //remove method..................
  remove(deleteData : ResourceRatecard){
      var queryParams = "/"+ deleteData.id;
        this.resourceRateService.remove(queryParams).subscribe(res=>{
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

  columnSearch() 
  {
    return false;
  }

  backClicked() {
    this._location.back();
  }

}





