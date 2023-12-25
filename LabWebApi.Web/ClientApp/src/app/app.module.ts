import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthInterceptorProvider } from './core/interceptors/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CreateProductDialogComponent } from './pages/home-components/products-panel/create-product-dialog/create-product-dialog.component';
import { EditProductDialogComponent } from './pages/home-components/products-panel/edit-product-dialog/edit-product-dialog.component';
import { EditUserDialogComponent } from './pages/home-components/admin-panel/edit-user-dialog/edit-user-dialog.component';
import { EnumNamePipe } from './core/pipes/EnumNamePipe';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HeaderComponent } from './pages/app-components/header/header.component';
import { HomeComponent } from './pages/home-components/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './pages/auth-components/login/login.component';
import { MatModule } from './core/modules/mat.module';
import { NgModule } from '@angular/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ProductsListComponent } from './pages/home-components/products-panel/products-list/products-list.component';
import { ProfileComponent } from './pages/home-components/profile/profile-show/profile.component';
import { ProfileEditComponent } from './pages/home-components/profile/profile-edit/profile-edit.component';
import { ProfileChangePasswordComponent } from './pages/home-components/profile/profile-change-password/profile-change-password.component';
import { RegistrationComponent } from './pages/auth-components/registration/registration.component';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators'; // <-- #2 import module
import { UsersListComponent } from './pages/home-components/admin-panel/users-list/users-list.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    HeaderComponent,
    UsersListComponent,
    EnumNamePipe,
    EditUserDialogComponent,
    ProductsListComponent,
    CreateProductDialogComponent,
    EditProductDialogComponent,
    ProfileComponent,
    ProfileEditComponent,
    ProfileChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    RxReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FlexLayoutModule,
    MatModule,
    JwtModule.forRoot({ config: { tokenGetter } }),
    NgxDatatableModule,
  ],
  providers: [AuthInterceptorProvider, ErrorInterceptor],
  bootstrap: [AppComponent],
})
export class AppModule {}
