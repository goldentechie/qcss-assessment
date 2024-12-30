import { Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ContactsService extends BaseService {

  private apiUrl = `contacts`;
  private httpClient: HttpClient;

  constructor(http: HttpClient, private toastService: ToastrService) {
    super(http, `contacts`, toastService);
  }

}
