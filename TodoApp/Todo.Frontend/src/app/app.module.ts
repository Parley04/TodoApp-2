import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule, JwtHelperService } from '@auth0/angular-jwt';
import { MessageService, ConfirmationService } from 'primeng/api';
import { AppRoutingModule } from './app-rotuting.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './todo/interceptor/auth.interceptor';
import { ErrorInterceptor } from './todo/interceptor/error.interceptor';
import { AppLayoutModule } from './todo/layout/app.layout.module';
import { AuthGuard } from './todo/login/guard/auth.guard';
import { RoleGuard } from './todo/login/guard/role.guard';
import { LoginModule } from './todo/login/login.module';
import { DecodeService } from './todo/service/decode.service';
import { TodoModule } from './todo/todo.module';



@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    AppLayoutModule,
    LoginModule,
    TodoModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem("todoToken"),
      },
    }),
  ],
  providers: [
    { provide: 'apiUrl', useValue: 'https://localhost:7054/api/' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    MessageService,
    DecodeService,
    JwtHelperService,
    MessageService,
    ConfirmationService,
    AuthGuard,
    RoleGuard,
  ],
  exports: [AppComponent],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
