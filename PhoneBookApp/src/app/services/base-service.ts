import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, catchError, of } from "rxjs";
import { environment } from "src/environments/environment";
import { APIResponseModel } from "../models/api-response-model";
import { ToastrService } from "ngx-toastr";

export class BaseService {
    
    protected baseUrl = ``;
    protected http: HttpClient;
    private toastrService: ToastrService;

    constructor(httpClient: HttpClient, serviceUrl: string, toastrService: ToastrService) {
        this.baseUrl = `${environment.apiBaseUrl}/${serviceUrl}`;
        this.http = httpClient;
        this.toastrService = toastrService;
    }

    entityExists(data: any): Observable<APIResponseModel<any>> {
        return this.http.get<APIResponseModel<any>>(`${this.baseUrl}/entityExists/${data}`)
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, {});
            })
        );
    }

    add(userData: any): Observable<APIResponseModel<any>> {
        return this.http.post<APIResponseModel<any>>(`${this.baseUrl}`, userData)
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, {});
            })
        );
    }

    get(filters: any | null = null): Observable<APIResponseModel<any[]>> {
        return this.http.get<APIResponseModel<any[]>>(`${this.baseUrl}`, filters ? { params: filters} : { params: {}})
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, []);
            })
        );
    }

    getById(id: string): Observable<APIResponseModel<any>> {
        return this.http.get<APIResponseModel<any>>(`${this.baseUrl}/${id}`)
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, {});
            })
        );
    }

    update(id: string, data: any): Observable<APIResponseModel<any>> {
        return this.http.put<APIResponseModel<any>>(`${this.baseUrl}/${id}`, data)
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, {});
            })
        );
    }

    delete(id: string): Observable<APIResponseModel<any>> {
        return this.http.delete<APIResponseModel<any>>(`${this.baseUrl}/${id}`)
        .pipe(
            catchError((err: HttpErrorResponse) => {
               return this.httpErrorHandler(err, {});
            })
        );
    }

    httpErrorHandler(err: HttpErrorResponse, data: any) : Observable<APIResponseModel<any>> {
        if (err.status !== 401) {
            this.toastrService.error('Something went wrong with server', 'Error');
        }
        var response = new APIResponseModel<any>();
        response.isError = true;
        response.message = "Something went wrong with server";
        response.data = data;
        return of(response);
    }
}