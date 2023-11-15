import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './core/guards/AuthGuard';
import { AutoLoginGuard } from './core/guards/AutoLoginGuard';
import { HomeComponent } from './pages/home-components/home/home.component';
import { LoginComponent } from './pages/auth-components/login/login.component';
import { NgModule } from '@angular/core';
import { RegistrationComponent } from './pages/auth-components/registration/registration.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent, canActivate: [AutoLoginGuard] },
  { path: 'registration', component: RegistrationComponent },
  {
    path: 'user-home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
  },
  { path: '**', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
