import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ApiIntractionsService {
  storedItem : any;
  constructor(private http : HttpClient) { }

  getMethod(apiEndPoint?:any){
       return this.http.get<any>(apiEndPoint);
  }
  postMethod(apiEndPoint:any, data:any){
       return this.http.post<any>(apiEndPoint, data);
  }
  putMethod(apiEndPoint:any,data:any){
       return this.http.put<any>(apiEndPoint,data);
  }
  deleteMethod(apiEndPoint:any){
       return this.http.delete<any>(apiEndPoint);
  }

  //Local Storage Methods..............
  setItem(key: string, value: any) {
     localStorage.setItem(key, JSON.stringify(value));
   }
 
   getItem(key: string) {
     this.storedItem = localStorage.getItem(key);
     return JSON.parse(this.storedItem);
   }
}
