import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DecodeService } from '../../service/decode.service';
import { Todo } from '../model/todo';
import { MainService } from '../service/main.service';
import { MessageService } from 'primeng/api';
import { TodoList } from '../model/todo-list';

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

  data: TodoList = new TodoList();
  id: number = 0;
  userId: string = "";
  loading: boolean = false;
  selectedColor: number;

  colors = [
    { id: 1, name: 'red', selected: false, class: 'red-checkbox' },
    { id: 2, name: 'yellow', selected: false, class: 'yellow-checkbox' },
    { id: 3, name: 'blue', selected: false, class: 'blue-checkbox' },
    { id: 4, name: 'purple', selected: false, class: 'purple-checkbox' },
    { id: 5, name: 'green', selected: false, class: 'green-checkbox' }
  ];

  @ViewChild('todoForm') form: any;

  ngOnInit(): void {

    const userIdFromService = this.decodeService.getUserId();
    this.userId = userIdFromService ? userIdFromService.toString() : '';
    this.data.userId = this.userId;
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
            this.selectedColor = res.data.backgroundColor;
            this.updateColorSelection();
            
          }
        });
      } else {
        this.selectedColor = 1;
        this.updateColorSelection();
      }
    });
  }
  updateColorSelection() {
    this.colors.forEach(c => c.selected = c.id === this.selectedColor);
  }
  getTags() {

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

  changeBackgroundColor(color: any) {
    this.data.backgroundColor = color.id;
  }

  getBackgroundColor(backgroundColor: number): string {
    switch (backgroundColor) {
      case 1:
        return '#ffcccc'; // Red
      case 2:
        return '#ffffcc'; // Yellow
      case 3:
        return '#cce0ff'; // Blue
      case 4:
        return '#e6ccff'; // Purple
      case 5:
        return '#ccffcc'; // Green
      default:
        return 'transparent';
    }
  }


 

  // Her checkbox tıklandığında, sadece bir tanesinin seçili olmasını sağlamak
  onColorChange(color: any) {
    // Seçili olan tüm renkleri sıfırlıyoruz
    this.colors.forEach(c => c.selected = false);
    // Tıklanan rengi seçili yapıyoruz
    color.selected = true;
    this.data.backgroundColor = color.id;
  }


}
