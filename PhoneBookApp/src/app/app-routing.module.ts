import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { VerifyEmailComponent } from './components/verify-email/verify-email.component';
import { CreateNewPasswordComponent } from './components/create-new-password/create-new-password.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { AuthenticationGuard } from './authentication.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'verify-email', component: VerifyEmailComponent },
  { path: 'create-new-password', component: CreateNewPasswordComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'admin', loadChildren: () => import('./modules/admin/admin.module').then((m) => m.AdminModule), canActivate: [AuthenticationGuard], data: { role: [ "Admin", "SystemUser"] }},
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
