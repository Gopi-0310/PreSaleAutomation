import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { workStream } from '../entity/workstream-activity';
import { workStreamItems } from '../entity/workstream-items.model';

@Injectable({
  providedIn: 'root'
})
export class WorkStreamService {

  constructor(private http: HttpClient, private router: Router) { }

  getById(id: string) {
    return this.http.get<any>(`${environment.endpointUrl}/WorkStream/` + id).pipe(
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



  getByIdTotalWeeks(id: string) {
    return this.http.get<any>(`${environment.endpointUrl}/WorkStream/GetDetailsById/` + id).pipe(
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
    return this.http.delete<workStream>(`${environment.endpointUrl}/WorkStream/` + id).pipe(
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
