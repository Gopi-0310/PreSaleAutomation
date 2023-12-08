import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ViewExport } from '../entity/view-export.model';
import { environment } from 'src/environments/environment';
import { ActivityLog } from '../entity/activitylog.model';
import { RecentActivityLog } from '../entity/recent-activity.model';

@Injectable({
  providedIn: 'root'
})
export class ViewExportService {

  constructor(private http: HttpClient, private router: Router) { }

  getAll(apiEndPoint: string, queryParams?: HttpParams) {
    return this.http.get<ViewExport>(apiEndPoint, { params: queryParams }).pipe(
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
  save(apiEndPoint: string, viewExportModel: ViewExport) {
    return this.http.post<ViewExport>(apiEndPoint, viewExportModel).pipe(
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
  update(apiEndPoint: string, viewExportModel: ViewExport) {
    return this.http.put<ViewExport>(apiEndPoint, viewExportModel).pipe(
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
  remove(apiEndPoint: string, queryParams?: HttpParams) {
    return this.http.delete<ViewExport>(apiEndPoint, { params: queryParams }).pipe(
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

  getRecentExportActivities() {
    return this.http.get<ActivityLog>(`${environment.endpointUrl}/ActivityLog`).pipe(
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

  getRecentActivities() {
    return this.http.get<RecentActivityLog>(`${environment.endpointUrl}/ActivityLog/GetRecentAcitivity`).pipe(
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

