import { 
    Injectable 
 } from '@angular/core';  
 import { Profile } from './profile';
 
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError, retry} from 'rxjs/operators';
 
 @Injectable()
 export class ProfileService { 
    constructor(private http: HttpClient) { }

    getAll(): Observable<Profile[]> { 
       return this.http.get<Profile[]>("https://localhost:44316/");
    }

    addProfile(profile: Profile) {
       console.log("Service layer invoked");
       return this.http.post("https://localhost:44316/", profile).pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
      if (error.status === 0) {
        // A client-side or network error occurred. Handle it accordingly.
        console.error('An error occurred:', error.error);
      } else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong.
        console.error(
          `Backend returned code ${error.status}, body was: `, error.error);
      }
      // Return an observable with a user-facing error message.
      return throwError(
        'Something bad happened; please try again later.');
    }
    
 } 