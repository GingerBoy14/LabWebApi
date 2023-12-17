import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthInterceptorProvider } from './core/interceptors/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HeaderComponent } from './pages/app-components/header/header.component';
import { HomeComponent } from './pages/home-components/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './pages/auth-components/login/login.component';
import { MatModule } from './core/modules/mat.module';
import { NgModule } from '@angular/core';
import { RegistrationComponent } from './pages/auth-components/registration/registration.component';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators'; // <-- #2 import module

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
  ],
  providers: [AuthInterceptorProvider, ErrorInterceptor],
  bootstrap: [AppComponent],
})
export class AppModule {}
