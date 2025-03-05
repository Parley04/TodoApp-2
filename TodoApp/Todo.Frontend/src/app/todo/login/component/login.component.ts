import { Component, OnInit } from '@angular/core';
import { Login } from '../model/login';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { LayoutService } from '../../layout/service/app.layout.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: [

    ]
})
export class LoginComponent implements OnInit {


    data: Login = new Login();
    loading: boolean = false;

    constructor(private loginService: AuthService, public layoutService: LayoutService, private router: Router) { }

    ngOnInit(): void {
        if (this.loginService.isAuthenticate()) {
            this.router.navigate(["/home"]);
        }
    }

    async login() {
        await this.loginService.login(this.data).then((res) => {
            this.loading = res;
        });


    }

}
