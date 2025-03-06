import { Inject, Injectable } from '@angular/core';
import { Token } from '../login/model/token';
import { HttpClient } from '@angular/common/http';
import { Login } from '../login/model/login';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DecodeService } from './decode.service';
import { Register } from '../register/model/register';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  adminToken: Token = new Token();

  constructor(
    @Inject("apiUrl") private apiUrl: string,
    private httpClient: HttpClient,
    private router: Router,
    private messageService: MessageService,
    private jwtHelper: JwtHelperService,
    private decoder: DecodeService,
  ) { }

  isAuthenticate() {
    if (localStorage.getItem("todoToken")) {
      return true;
    }
    return false;
  }

  login(adminLogin: Login): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      let api = this.apiUrl + "Auth/Login";
      this.httpClient.post(api, adminLogin).subscribe((res: any) => {
        this.adminToken = res.data;
        console.log(this.adminToken);
        localStorage.setItem("todoToken", this.adminToken.token);
        this.messageService.add({ severity: 'info', summary: 'Bilgi', detail: "Loing is Succeed" });
        setTimeout(() => {
          this.router.navigate(["/home"]);
        }, 1500);
        resolve(true);
      }, (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error })
        resolve(false);
      });
    });
  }

  register(data: Register): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      let api = this.apiUrl + "Auth/Register";
      this.httpClient.post(api, data).subscribe((res: any) => {
        this.adminToken = res.data;
        console.log(this.adminToken);
        localStorage.setItem("todoToken", this.adminToken.token);
        this.messageService.add({ severity: 'info', summary: 'Bilgi', detail: "Register is Succeed" });
        setTimeout(() => {
          this.router.navigate(["/home"]);
        }, 1500);
        resolve(true);
      }
        , (err) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error })
          resolve(false);
        }
      );
    })
  }

  logout() {
    localStorage.removeItem("todoToken");
    setTimeout(() => {
      this.router.navigate(["/login"]);
    }, 750);
    this.messageService.add({ severity: 'info', summary: 'Bilgi', detail: "Logout is Succeed" });
  }

  // hasRequiredRoles(requiredRoles: Array<string> | string): boolean {
  //   if (!this.isAuthenticated) {
  //     return false;
  //   }
  //   let match = requiredRoles.includes(this.decoder.getUserRole());
  //   return match;
  // }

  identityCheck() {
    const token = localStorage.getItem("todoToken");
    let expired: boolean;

    try {
      expired = this.jwtHelper.isTokenExpired(token);
    }
    catch (error) {
      expired = true;
    }
    _isAuthenticated = token != null && !expired;
  }


  get isAuthenticated(): boolean {
    return _isAuthenticated;
  }
}

export let _isAuthenticated: boolean;
