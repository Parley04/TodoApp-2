import { Component, OnInit } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DecodeService } from '../../service/decode.service';
import { TagService } from '../service/tag.service';
import { Tag } from '../model/tag';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styles: ``
})
export class TagListComponent implements OnInit {

  constructor(
    private tagService: TagService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private decodeService: DecodeService,
  ) { }
  data: Tag[] = [];
  tag: Tag = new Tag();
  loading: boolean = true;
  display: boolean = false;
  userId: string = "";

  ngOnInit(): void {
    const userIdFromService = this.decodeService.getUserId();
    this.userId = userIdFromService ? userIdFromService.toString() : '';
    this.getList();
  }

  getList() {
    this.loading = true;
    this.tagService.getList(this.userId).subscribe((res: any) => {
      this.data = res.data;
      this.loading = false;
    }, (error) => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An error occurred while fetching the list' });
      this.loading = false;
    });
  }

  delete(data: Tag) {
    this.confirmationService.confirm({
      key: 'confirm1',
      message: 'Are you sure you want to delete ' + data.name + '?',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Yes',
      rejectLabel: 'Nor',
      rejectButtonStyleClass: 'p-button-secondary',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        data.deletedDate = new Date();
        this.tagService.delete(data).subscribe((res: any) => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Deleted' });
          setTimeout(() => {
            this.getList();
          }, 700);
        }, (error) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.errorMessages });
        });
      },
    });
  }

  saveTag() {
    this.tag.userId = this.userId;
    this.tag.createdDate = new Date();
    this.tag.countedUses = 0;
    this.tagService.add(this.tag).subscribe((res: any) => {
      this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Saved' });
      this.getList();
      this.display = false;
    }, (error) => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.errorMessages });
    });
  }
  clear(table: any) {
    table.clear();
  }

  openDialog() {
    this.display = true;
  }
}
