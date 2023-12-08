import {  Component, TemplateRef } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AppConstants } from 'src/environments/AppConstants';
import { ApiIntractionsService } from '../../../services/api-intractions.service';
import { environment } from '../../../../environments/environment';
import { Shared } from 'src/app/shared/shared';
import * as XLSX from 'xlsx';
import { CostByResourceService } from 'src/app/services/cost-by-resource.service';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { Location } from '@angular/common';


@Component({
  selector: 'app-view-export',
  templateUrl: './view-export.component.html',
  styleUrls: ['./view-export.component.scss']
})
export class ViewExportComponent {
displayedColumns   : string[] = ['Code', 'Role', 'Hour', 'Week','Rate'];
displayedColumns1  : string[] = ['thread', 'hours', 'fees'];
resultsLength      = 0;
hoveredRow         : any;
dataSource         = new MatTableDataSource<any>;
dataSource1        = new MatTableDataSource<any>;
workStreamList     = new MatTableDataSource<any>;

public templateRef !: TemplateRef<any>;
getListCostByResource : any;
projectTitle  : string = "Project A";
hours         : number = 844;
hoursPerWeek  : number = 40;
weeks         : number = this.hours/this.hoursPerWeek;
project       : any;
WorkStreamData : any;
totalCostAndWeeks: any;
totalHoursAndCost : any = 0;
aggregatedData: { workStreamId: string, rate: string, hours: string, name:string }[] = [];

  constructor(
    private shared: Shared,
    private service : CostByResourceService,
    private api: ApiIntractionsService,
    private _location: Location,
    private router: Router){
    this.shared.setTitle(AppConstants.exportArtifacts);
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    var data: any = currentStateExtras;
    this.project = data;
   }
//common...........         
ngOnInit(): void {
  this.getAllList();
  this.totalWorkStreamList();
  this.totalCostByWeeks();
 
}

ngAfterViewInit() { }

getAllList(){
  this.service.getById(this.project.projectId).subscribe(res=>{
    this.WorkStreamData = res;
    //this.dataSource = new MatTableDataSource(Object.values(this.WorkStreamData));
    if (this.WorkStreamData.length > 0) {
      this.aggregateDataByWorkStreamId(this.WorkStreamData);
    }
  })
}

totalCostByWeeks(){
  this.service.costByResource(this.project.projectId).subscribe(res=>{
    this.totalCostAndWeeks =  res;
    this.dataSource = new MatTableDataSource(Object.values(this.totalCostAndWeeks.costSummary.costByResourceList));
    console.log("export loe",this.totalCostAndWeeks.costSummary.costByResourceList);
  })
}
totalWorkStreamList(){
  this.service.costByResource(this.project.projectId).subscribe(res=>{
    this.totalCostAndWeeks =  res;
    this.workStreamList = new MatTableDataSource(Object.values(this.totalCostAndWeeks.costSummary.costByWorkStreamList));
    console.log("export loe and",res);
  })
}
  exportLOE() {
    this.service.exportLOE(this.project.projectId).subscribe(async (event) => {
      let data = event as HttpResponse<Blob>;
      const downloadedFile = new Blob([data.body as BlobPart], {
        type: data.body?.type
      });
      
      if (downloadedFile.type != "") {
        const a = document.createElement('a');
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        a.download = "LOE";
        a.href = URL.createObjectURL(downloadedFile);
        a.target = '_blank';
        a.click();
        document.body.removeChild(a);
      }
    });
  }

exportexcel(getTitle : any, getId : any): void
{

 /* pass here the table id */
 let element = document.getElementById(`${getId}`);
 const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);

 /* generate workbook and add the worksheet */
 const wb: XLSX.WorkBook = XLSX.utils.book_new();
 XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

 /* save to file */  
 XLSX.writeFile(wb, `${getTitle}.xlsx`);
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


getAllListData(){
  this.WorkStreamData
}

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
  this.dataSource1 =  new MatTableDataSource(Object.values(this.aggregatedData));
}

  backClicked() {
    this._location.back();
  }

}


