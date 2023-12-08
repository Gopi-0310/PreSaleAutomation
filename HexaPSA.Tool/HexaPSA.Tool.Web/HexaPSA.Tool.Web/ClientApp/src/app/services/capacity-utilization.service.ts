import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { CapacityUtilizationMapping } from '../entity/capacity-utilization-mapping.model';
import { CapacityUtilization } from '../entity/capacity-utilization.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CapacityUtilizationService {

  constructor(private http : HttpClient, private router : Router) { }

  getAll(id : string){
    return this.http.get<CapacityUtilizationMapping>(`${environment.endpointUrl}/CapacityUtilization/`+id).pipe(
      catchError((err) => {
        if(err.status == 401){
         //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if(err.status == 400){
         //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })    
    )     
  }

// TODO if we need to passs the token we should pass the interceptor..
  save(capacityModel:CapacityUtilization){
    return this.http.post<CapacityUtilization>(`${environment.endpointUrl}/CapacityUtilization`, capacityModel).pipe(
      catchError((err) => {
        if(err.status == 401){
         //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if(err.status == 400){
         //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })    
    )     
  }

  // TODO if we need to passs the token we should pass the interceptor..
  update(id:string,capacityModel:CapacityUtilization){
    return this.http.put<CapacityUtilization>(`${environment.endpointUrl}/CapacityUtilization`+id, capacityModel).pipe(
      catchError((err) => {
        if(err.status == 401){
         //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if(err.status == 400){
         //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })    
    )     
  }

  // TODO if we need to passs the token we should pass the interceptor..
  remove(id : string ){
    return this.http.delete<CapacityUtilization>(`${environment.endpointUrl}/CapacityUtilization`+id).pipe(
      catchError((err) => {
        if(err.status == 401){
         //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if(err.status == 400){
         //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })    
    )     
  }

  
}
