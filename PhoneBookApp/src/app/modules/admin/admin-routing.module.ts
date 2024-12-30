import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserAddComponent } from './components/user-add/user-add.component';
import { ContactsListComponent } from './components/contacts-list/contacts-list.component';
import { AdminAuthenticationGuard } from './admin-authentication.guard';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
    {path: '', component: HomeComponent, children: [
   {path: 'user-list', component: UserListComponent, canActivate: [AdminAuthenticationGuard], data: { permission: "Admin" }},
    {path: 'user-add', component: UserAddComponent, canActivate: [AdminAuthenticationGuard], data: { permission: "Admin" }},
    {path: 'user-add/:id', component: UserAddComponent, canActivate: [AdminAuthenticationGuard], data: { permission: "Admin" }},
    {path: 'contacts', component: ContactsListComponent, canActivate: [AdminAuthenticationGuard], data: { }},
    {path: '', redirectTo: '/admin/contacts', pathMatch: 'full'}
  ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
