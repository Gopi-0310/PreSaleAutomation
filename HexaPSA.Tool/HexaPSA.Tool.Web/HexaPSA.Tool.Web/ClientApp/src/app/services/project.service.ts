import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { CreateProjectRequest } from '../model/project';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, private router: Router) { }

  getAll() {
    return this.http.get<any>(`${environment.endpointUrl}/Projects/GetAllProject`).pipe(
      
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

  getAllPieChart() {
    return this.http.get<any>(`${environment.endpointUrl}/Projects/GetAllChart`).pipe(

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

  getganttChart(id: string) {
    return this.http.get<any>(`${environment.endpointUrl}/WorkStreamActivity/GetChartDetailsById/` + id).pipe(

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

  getById(id: string) {
    return this.http.get<any>(`${environment.endpointUrl}/Projects/GetProject/`+ id).pipe(
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

  save(projectRequest: CreateProjectRequest) {
    return this.http.post<CreateProjectRequest>(`${environment.endpointUrl}/CreateProject`, projectRequest).pipe(
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
