import { Component, OnInit } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DecodeService } from '../../service/decode.service';
import { MainService } from '../service/main.service';
import { TodoList } from '../model/todo-list';
import { Todo } from '../model/todo';
import { Tag } from 'primeng/tag';

@Component({
  selector: 'app-main-list',
  templateUrl: './main-list.component.html',
  styles: ``
})
export class MainListComponent implements OnInit {

  constructor(
    private mainService: MainService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private decodeService: DecodeService,

  ) { }

  data: TodoList[] = [];
  tags:Tag[] = [];

  loading: boolean = true;
  userId: string = "";

  ngOnInit(): void {
    const userIdFromService = this.decodeService.getUserId();
    this.userId = userIdFromService ? userIdFromService.toString() : '';
    this.getList();

  }

  getList() {
    this.mainService.getList(this.userId).subscribe((res: any) => {
      this.data = res.data;
      this.loading = false;
      this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: res.data.length + " Kayıt Listelendi" });
    }
    );
  }
  delete(data: Todo) {
    this.confirmationService.confirm({
      key: 'confirm1',
      //target: event.target || new EventTarget,
      message: data.title + " ürününü silmek istediğine emin misiniz?",
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet',
      rejectLabel: 'Hayır',
      rejectButtonStyleClass: 'p-button-secondary',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        data.deletedDate = new Date();
        
        this.mainService.delete(data).subscribe((res: any) => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: res.message });
          setTimeout(() => {
            this.getList();
          }, 700);
        },
          (err: any) => {
            this.messageService.add({ severity: 'error', summary: 'Hata', detail: err.error.message });
          });
      },
    })
  }
  
  saveCheckbox(data: Todo) {
    data.isCompleted = !data.isCompleted;
    data.updatedDate = new Date();
    this.mainService.update(data).subscribe((res: any) => {
      setTimeout(() => {
        this.getList();
      }, 700);
    },
      (err: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message });
      });
  }

  getBackgroundColor(backgroundColor: number): string {
    switch(backgroundColor) {
      case 1:
        return '#ffcccc'; 
      case 2:
        return '#ffffcc'; 
      case 3:
        return '#cce0ff';
      case 4:
        return '#e6ccff'; 
      case 5:
        return '#ccffcc'; 
      default:
        return 'transparent'; 
    }
  }
  
  clear(table: any) {
    table.clear();
  }
}
