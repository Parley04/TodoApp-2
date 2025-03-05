import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { DecodeService } from '../service/decode.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    typeId: number = 0;

    constructor(public layoutService: LayoutService, private decodeService: DecodeService) { }

    ngOnInit() {
        this.model = [{
            label: 'Welcome ' + this.decodeService.getName(),
            items: [
                { label: 'Home', icon: 'pi pi-fw pi-home', routerLink: ['/home'] },
                { label: 'Todos', icon: 'pi pi-fw pi-check-square', routerLink: ['/todo'] },
            ]
        }];
      
    }
}
