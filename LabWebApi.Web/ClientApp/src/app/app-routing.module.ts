import { ProductsViewComponent } from './pages/home-components/products-panel/product-view/product-view.component';
import { RouterModule, Routes } from '@angular/router';

import { AdminRoleGuard } from './core/guards/AdminRoleGuard';
import { AuthGuard } from './core/guards/AuthGuard';
import { AutoLoginGuard } from './core/guards/AutoLoginGuard';
import { HomeComponent } from './pages/home-components/home/home.component';
import { LoginComponent } from './pages/auth-components/login/login.component';
import { NgModule } from '@angular/core';
import { ProductsListComponent } from './pages/home-components/products-panel/products-list/products-list.component';
import { ProfileComponent } from './pages/home-components/profile/profile-show/profile.component';
import { RegistrationComponent } from './pages/auth-components/registration/registration.component';
import { UsersListComponent } from './pages/home-components/admin-panel/users-list/users-list.component';

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
  {
    path: 'users-list',
    component: UsersListComponent,
    canActivate: [AuthGuard, AdminRoleGuard],
  },
  {
    path: 'products-list',
    component: ProductsListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'products/:id',
    component: ProductsViewComponent,
    canActivate: [AuthGuard],
  },
  { path: "profile", component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
