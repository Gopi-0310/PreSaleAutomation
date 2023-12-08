import { Injectable } from '@angular/core';
import { Technology } from '../entity/technology.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class TechnologyService {

  constructor(private http:HttpClient,private router:Router) { }
  getAll() {
    return this.http.get<Technology>(`${environment.endpointUrl}/Technology`).pipe(
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
    return this.http.post<any>(`${environment.endpointUrl}/Technology`, projectRequest).pipe(
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
    return this.http.put<any>(`${environment.endpointUrl}/Technology` + id, rateCardModel).pipe(
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
    return this.http.delete<any>(`${environment.endpointUrl}/Technology` + id).pipe(
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
