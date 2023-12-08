import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { EstimationDashboard } from '../entity/estimation-dashboard.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EstimationDashboardService {
 
  constructor(private http : HttpClient, private router : Router) { }

  // getAll(apiEndPoint : string,  queryParams? : HttpParams){
  //   return this.http.get<EstimationDashboard>(apiEndPoint, { params: queryParams })
  //   //  .pipe(
  //   //    catchError((err) => {
  //   //    if(err.status == 401){
         
  //   //      this.router.navigateByUrl('');
  //   //    }
  //   //    else if(err.status == 400){
         
  //   //    }
        
  //   //    console.error(err);
  //   //    return throwError(err);
  //   //  })    
  //   //)     
  // }
  getAll(id:any){
    return this.http.get<EstimationDashboard>(`${environment.endpointUrl}/Projects/GetProject/`+id).pipe(
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
  save(dashBoardModel:EstimationDashboard){
    return this.http.post<EstimationDashboard>(`${environment.endpointUrl}/Projects/CreateProject`, dashBoardModel).pipe(
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
  update(id : string, dashBoardModel:string){
    console.log(id);
    console.log(`${environment.endpointUrl}/Projects/EditProject/${id}`);
    var headers={
      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      })
  }
    return this.http.put<EstimationDashboard>(`${environment.endpointUrl}/Projects/EditProject/${id}`,dashBoardModel,headers).pipe(
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
  remove(id:string){
    return this.http.delete<EstimationDashboard>(`${environment.endpointUrl}/Projects/DeleteProject`+id).pipe(
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
