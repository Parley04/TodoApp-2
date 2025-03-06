import { Component, OnInit } from '@angular/core';
import { Register } from '../model/register';
import { Router } from '@angular/router';
import { RegisterService } from '../service/register.service';
import { LayoutService } from '../../layout/service/app.layout.service';
import { AuthService } from '../../service/auth.service';
import { Login } from '../../login/model/login';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: ``
})
export class RegisterComponent implements OnInit {

  register: Register = new Register();
  data: Login = new Login();
  loading: boolean = false;

  constructor(private authService: AuthService, public layoutService: LayoutService, private router: Router) { }
  //PASSWORD TRIM UYGULA BUNU LOGIN DE DE YAP 
  ngOnInit(): void {
    if (this.authService.isAuthenticate()) {
      this.router.navigate(["/home"]);
    }
  }

  async registerFunc() {
    await this.authService.register(this.register).then((res:any)=>{
      this.loading=true;
    })
  }
  async login() {
    await this.authService.login(this.data).then((res) => {
      this.loading = res;
    });


  }
}