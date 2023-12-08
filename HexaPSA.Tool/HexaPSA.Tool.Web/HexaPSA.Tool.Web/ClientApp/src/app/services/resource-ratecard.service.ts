import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ResourceRatecard } from '../entity/resource-ratecard.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ResourceRatecardService {

  constructor(private http : HttpClient, private router : Router) { }

  getAll(){
    return this.http.get<ResourceRatecard>(`${environment.endpointUrl}/ResourceRateCard`).pipe(
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
  save(rateCardModel:ResourceRatecard){
    return this.http.post<ResourceRatecard>(`${environment.endpointUrl}/ResourceRateCard`, rateCardModel).pipe(
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
  update(id : string, rateCardModel:ResourceRatecard){
    return this.http.put<ResourceRatecard>(`${environment.endpointUrl}/ResourceRateCard`+ id, rateCardModel).pipe(
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
  remove(id : string){
    return this.http.delete<ResourceRatecard>(`${environment.endpointUrl}/ResourceRateCard`+id).pipe(
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
