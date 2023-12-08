import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { CreateSow } from '../entity/create-sow.model';

@Injectable({
  providedIn: 'root'
})
export class CreateSowService {

  constructor(private http : HttpClient, private router : Router) { }

  getAll(apiEndPoint : string,  queryParams? : HttpParams){
    return this.http.get<CreateSow>(apiEndPoint, {params : queryParams}).pipe(
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
  save(apiEndPoint : string, sowModel:CreateSow){
    return this.http.post<CreateSow>(apiEndPoint, sowModel).pipe(
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
  update(apiEndPoint : string, sowModel:CreateSow){
    return this.http.put<CreateSow>(apiEndPoint, sowModel).pipe(
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
  remove(apiEndPoint : string, queryParams? : HttpParams ){
    return this.http.delete<CreateSow>(apiEndPoint, { params: queryParams}).pipe(
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

