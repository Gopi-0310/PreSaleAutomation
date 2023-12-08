import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
  export class Shared {
    heading     = new BehaviorSubject('');
    buttonName  = new BehaviorSubject('');
    formModel   = new BehaviorSubject('');
    projectId   = new BehaviorSubject('');
    interview = new BehaviorSubject('');
    
    setTitle(heading:string){
      this.heading.next(heading);
    }
    setName(buttonName:string){
      this.buttonName.next(buttonName);
    }
    setFormName(formModel:string){
      this.formModel.next(formModel);
    }
    setProjectId(projectId:any) {
      this.projectId.next(projectId);
    }
  }
