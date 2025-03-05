import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { DecodeService } from '../erp/service/decode.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    typeId: number = 0;

    constructor(public layoutService: LayoutService, private decodeService: DecodeService) { }

    ngOnInit() {
        //this.typeId=parseInt(this.decodeService.getUserTpeId());
        // if(this.typeId==1){

        this.model = [{
            label: 'Hoşgeldiniz ' + this.decodeService.getName(),
            items: [
                { label: 'Ana Sayfa', icon: 'pi pi-fw pi-home', routerLink: ['/home'] },
                { label: 'Kullanıcılar', icon: 'pi pi-fw pi-building', routerLink: ['/faculty'] },
            ]
        }];
        
        // }
        // else if(this.typeId==2){
        //     this.model = [
        //         {
        //             label: 'Hoşgeldin ' +this.decodeService.getUserName(),
        //             items: [
        //                 { label: 'Ana Sayfa', icon: 'pi pi-fw pi-home', routerLink: ['/home'] },
        //                 { label: 'Toplantılarım', icon: 'pi pi-fw pi-calendar',routerLink:['/teacher-meeting'] },
        //                 { label: 'Makalelerim', icon: 'pi pi-fw pi-file-edit',routerLink:['/teacher-article'] },
        //                 { label: 'Projelerim', icon: 'pi pi-fw pi-sliders-h' ,routerLink:['/teacher-project']},
        //             ]
        //         }
        //     ];
        // }
    }
}
