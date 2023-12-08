import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component } from '@angular/core';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-sow',
  templateUrl: './create-sow.component.html',
  styleUrls: ['./create-sow.component.scss']
})
export class CreateSowComponent {
  constructor(
    private shared  : Shared,
    private _location: Location
    ){
    this.shared.setTitle(AppConstants.createSOW);
   }
   //Variables
   workstreamData : any = ['Item 1','Item 2','Item 3'];
  
   // Accordion array - Drag n Drop
  dragNDrop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.workstreamData, event.previousIndex, event.currentIndex);
  }

  get_SOW(){
    // this.api.getMethod(environment.workstream_url).subscribe(res=>{
    //   this.workstreamData = res;
    // })
  }
  //Add Statement of Work 
  add_SOW()
  {
  }

  backClicked() {
    this._location.back();
  }
}
