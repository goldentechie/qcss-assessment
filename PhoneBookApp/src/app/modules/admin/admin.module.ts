import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { AdminRoutingModule } from './admin-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ToastrModule } from 'ngx-toastr';
// Components
import { UserListComponent } from './components/user-list/user-list.component';
import { UserAddComponent } from './components/user-add/user-add.component';
import { ContactsListComponent } from './components/contacts-list/contacts-list.component';
import { ContactPopupComponent } from './components/popups/contact-popup/contact-popup.component';
import { DeleteConfirmationComponent } from './components/popups/delete-confirmation/delete-confirmation.component';
import { HomeComponent } from './components/home/home.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    UserListComponent,
    UserAddComponent,
    ContactsListComponent,
    ContactPopupComponent,
    DeleteConfirmationComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    AgGridModule,
    NgxDropzoneModule,
    NgSelectModule, FormsModule, ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    ToastrModule,
    SharedModule,
  ],
  exports: [
  ]
})
export class AdminModule { }
