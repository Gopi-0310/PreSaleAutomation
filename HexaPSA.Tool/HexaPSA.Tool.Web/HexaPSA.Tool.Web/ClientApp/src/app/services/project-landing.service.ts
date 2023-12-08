import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ProjectLanding } from '../entity/project-landing.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectLandingService {

  constructor(private http : HttpClient, private router : Router) { }

  getAll(apiEndPoint : string,  queryParams? : HttpParams){
    return this.http.get<ProjectLanding>(apiEndPoint, {params : queryParams}).pipe(
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
  save(apiEndPoint : string, projectLandingModel:ProjectLanding){
    return this.http.post<ProjectLanding>(apiEndPoint, projectLandingModel).pipe(
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
  update(apiEndPoint : string, projectLandingModel:ProjectLanding){
    return this.http.put<ProjectLanding>(apiEndPoint, projectLandingModel).pipe(
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
    return this.http.delete<ProjectLanding>(apiEndPoint, { params: queryParams}).pipe(
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
