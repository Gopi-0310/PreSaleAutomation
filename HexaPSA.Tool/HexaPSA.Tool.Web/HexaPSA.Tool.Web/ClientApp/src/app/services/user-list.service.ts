import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserList } from '../entity/user-list.model';

@Injectable({
  providedIn: 'root'
})
export class UserListService {

  constructor(private http: HttpClient, private router: Router) { }

  getAll() {
    return this.http.get<UserList>(`${environment.endpointUrl}/User`).pipe(
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
  save(userListModel: UserList) {
    return this.http.post<UserList>(`${environment.endpointUrl}/User`, userListModel).pipe(
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
