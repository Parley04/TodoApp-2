import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Role } from '../login/model/role';

@Injectable({
    providedIn: 'root'
})
export class DecodeService {

    jwtHelperService: JwtHelperService = new JwtHelperService();
    roles: Role[] = [];
    role: Role = new Role();

    constructor() { }

    getUserId(): string | null {
        return this.jwtHelperService.decodeToken(localStorage.getItem("todoToken") || '')?.Id || null;
    }
    
    getName(): string | null {
        return this.jwtHelperService.decodeToken(localStorage.getItem("todoToken") || '')?.Name || null;
    }
    
    getUserName(): string | null {
        return this.jwtHelperService.decodeToken(localStorage.getItem("todoToken") || '')?.UserName || null;
    }
    
    getUserEmail(): string | null {
        return this.jwtHelperService.decodeToken(localStorage.getItem("todoToken") || '')?.Email || null;
    }
    
    
}
