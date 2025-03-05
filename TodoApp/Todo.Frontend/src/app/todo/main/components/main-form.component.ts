import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DecodeService } from '../../service/decode.service';
import { Todo } from '../model/todo';
import { MainService } from '../service/main.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-main-form',
  templateUrl: './main-form.component.html',
  styles: ``
})
export class MainFormComponent implements OnInit {

  constructor(
    private mainService: MainService,
    private messageService: MessageService,
    private decodeService: DecodeService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }
  data: Todo = new Todo();
  id: number = 0;
  userId: string = "";
  loading: boolean = false;
  @ViewChild('todoForm') form: any;

  ngOnInit(): void {

    const userIdFromService = this.decodeService.getUserId();
    this.userId = userIdFromService ? userIdFromService.toString() : '';
    this.route.params.subscribe(params => {
      this.id = params["id"];
      this.getTags();
      if (this.id != undefined) {
        this.mainService.getById(this.id.toString()).subscribe((res: any) => {
          if (res.data == null) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Todo is not founded' });
            setTimeout(() => {
              this.router.navigate(['/todo']);
            }, 700);
          } else {
            this.data = res.data;
            //todo tags listesini burada çağırmam gerkecek.
            //bir liste olarak gelecek
            // this.selectedTag = this.cities.find(city => city.id === this.data.cityId);
            // this.getTowns();
          }
        });
      }
    });
  }

  getTags(){

  }

  save() {
    if (this.form.invalid) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Validator error. ' });
      return;
    }
    this.loading = true;
    if (this.id == undefined) {
      this.data.createdDate = new Date();
      this.mainService.add(this.data).subscribe((res: any) => {
        this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: res.message });
        setTimeout(() => {
          this.router.navigate(['/todo']);
        }, 700);
      }, (err: any) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: err.error.message });
        this.loading = false;
      });
    }
    else {
      this.data.updatedDate = new Date();
      this.mainService.update(this.data).subscribe((res: any) => {
        this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: res.message });
        setTimeout(() => {
          this.router.navigate(['/todo']);
        }, 700);
      }, (err: any) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: err.error.message });
        this.loading = false;
      });
    }
  }
}
