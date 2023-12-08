import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CostByResource } from '../entity/cost-by-resource.model';

@Injectable({
  providedIn: 'root'
})
export class CostByResourceService {

  constructor(private http: HttpClient, private router: Router) { }

  getById(id: string) {
    return this.http.get<CostByResource>(`${environment.endpointUrl}/WorkStreamActivity/` + id).pipe(
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

  //exportLOE(id: string) {

  //  return this.http.get(`${environment.endpointUrl}/Export/LOE/` + id, {});


   
  //}

  exportLOE(id: string) {
    return this.http.get(`${environment.endpointUrl}/Export/LOE/` + id, {
      reportProgress: true,
      observe: 'events',
      responseType: 'blob'
    });
  }

  costByResource(id: string){
    return this.http.get(`${environment.endpointUrl}/Export/CostByResource/` + id); 
  }
}
