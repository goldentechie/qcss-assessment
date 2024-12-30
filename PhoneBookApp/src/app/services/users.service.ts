import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { ToastrService } from 'ngx-toastr';
import { APIResponseModel } from '../models/api-response-model';
import { Observable, catchError } from 'rxjs';
import { UserModel } from '../models/user/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends BaseService {

  constructor(http: HttpClient, private toastService: ToastrService) {
    super(http, `users`, toastService);
  }

  authenticate(username: string, password: string): Observable<APIResponseModel<UserModel>> {
    return this.http.post<APIResponseModel<UserModel>>(`${this.baseUrl}/authenticate`, { username: username, password: password})
    .pipe(
        catchError((err: HttpErrorResponse) => {
           return this.httpErrorHandler(err, {});
        })
    );
  }

  createNewPassword(currentPassword: string, password: string): Observable<APIResponseModel<UserModel>> {
    return this.http.post<APIResponseModel<UserModel>>(`${this.baseUrl}/resetpassword`, { currentPassword: currentPassword, password: password})
    .pipe(
        catchError((err: HttpErrorResponse) => {
           return this.httpErrorHandler(err, {});
        })
    );
  }

}
