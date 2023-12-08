import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectType } from '../entity/project-type';
import { environment } from 'src/environments/environment';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectTypeService {
  
  constructor(private http:HttpClient,private router:Router) { }
  getAll() {
    return this.http.get<ProjectType>(`${environment.endpointUrl}/ProjectTypeContrller/types/`).pipe(
      catchError((err) => {
        if (err.status == 401) {
          //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if (err.status == 400) {
          //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })
    )
  }

  save(projectRequest: any) {
    return this.http.post<any>(`${environment.endpointUrl}/ProjectTypeContrller`, projectRequest).pipe(
      catchError((err) => {
        if (err.status == 401) {
          //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if (err.status == 400) {
          //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })
    )
  }

  // TODO if we need to passs the token we should pass the interceptor..
  update(id: string, rateCardModel: any) {
    return this.http.put<any>(`${environment.endpointUrl}/ProjectTypeContrller` + id, rateCardModel).pipe(
      catchError((err) => {
        if (err.status == 401) {
          //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if (err.status == 400) {
          //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })
    )
  }

  // TODO if we need to passs the token we should pass the interceptor..
  remove(id: string) {
    return this.http.delete<any>(`${environment.endpointUrl}/ProjectTypeContrller` + id).pipe(
      catchError((err) => {
        if (err.status == 401) {
          //TODO clear the local storage data and hide the spinner
          this.router.navigateByUrl('');
        }
        else if (err.status == 400) {
          //TODO hide the spinner
        }
        //TODO add the loggerfile 
        console.error(err);
        return throwError(err);
      })
    )
  }
}
