import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DecodeService } from '../../service/decode.service';
import { MainService } from '../service/main.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TodoList } from '../model/todo-list';
import { TagService } from '../../tag/service/tag.service';
import { Tag } from '../../tag/model/tag';
import { TodoTagService } from '../../todoTag/service/todo-tag.service';
import { TodoTag } from '../../todoTag/model/todoTag';

@Component({
  selector: 'app-main-form',
  templateUrl: './main-form.component.html',
  styles: ``,

})
export class MainFormComponent implements OnInit {

  constructor(
    private mainService: MainService,
    private tagService: TagService,
    private todoTagService: TodoTagService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private decodeService: DecodeService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  data: TodoList = new TodoList();
  id: number = 0;
  userId: string = "";
  loading: boolean = false;
  display: boolean = false;
  displayForRemove: boolean = false;
  selectedColor: number;

  todoTag: TodoTag = new TodoTag();
  selectedTag: Tag = new Tag();
  deletedTag: Tag = new Tag();
  unchosenTags: Tag[] = [];
  tag: Tag = new Tag();
  tags: Tag[] = [];
  colors = [
    { id: 1, name: 'red', selected: false, class: 'red-checkbox' },
    { id: 2, name: 'yellow', selected: false, class: 'yellow-checkbox' },
    { id: 3, name: 'blue', selected: false, class: 'blue-checkbox' },
    { id: 4, name: 'purple', selected: false, class: 'purple-checkbox' },
    { id: 5, name: 'green', selected: false, class: 'green-checkbox' }
  ];
  displayedTags: any;
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

  saveTag() {
    this.tag.createdDate = new Date
    this.tag.userId = this.userId;
    this.tag.countedUses = 0;
    this.tagService.add(this.tag).subscribe((res: any) => {
      this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: res.message });
      this.display = false;
      this.saveTodoTag(res.data);
    }, (err: any) => {
      this.messageService.add({ severity: 'error', summary: 'Hata', detail: err.error.errorMessages });
    });
  }


  saveTodoTag(tag: string) {
    this.todoTag.todoId = this.data.id;
    this.todoTag.tagId = tag;
    this.todoTag.createdDate = new Date();
    this.todoTagService.add(this.todoTag).subscribe((res: any) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: res.message });
      this.getTags();
      this.filterTags();
      this.getUnchosenTags();
      this.display = false;
    }, (err: any) => {
      this.messageService.add({ severity: 'error', summary: 'Hata', detail: err.error.errorMessages });
    });
  }

  getTags() {
    this.mainService.getById(this.id.toString()).subscribe((res: any) => {
      this.data = res.data;
      this.selectedColor = res.data.backgroundColor;
      this.updateColorSelection();
    }
    );
  }

  getUnchosenTags() {
    this.tagService.getListOfUnchosenTags(this.userId, this.data.id).subscribe((res: any) => {
      this.unchosenTags = res.data;
      this.displayedTags = [...this.unchosenTags];

    });
  }

  delete(tag: Tag) {
    this.displayForRemove = true;
    this.deletedTag = tag;
  }

  confirmDelete() {
    this.todoTag.deletedDate = new Date();
    this.todoTag.tagId = this.deletedTag.id;
    this.todoTag.todoId = this.data.id;
    this.todoTagService.deleteWithItems(this.data.id, this.deletedTag.id).subscribe((res: any) => {
      this.messageService.add({ severity: 'success', summary: 'Succes', detail: res.message });
      setTimeout(() => {
        this.deletedTag = new Tag();
        this.displayForRemove = false;
        this.getTags();
      }, 700);
    },
      (err: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message });
      });
  }

  cancelDelete() {
    this.display = false;
  }








  filterTags() {
    const query = this.tag.name.toLowerCase();
    if (query) {
      this.displayedTags = this.unchosenTags.filter(tag => tag.name.toLowerCase().includes(query));
    } else {
      this.displayedTags = [...this.unchosenTags]; // Reset to show all tags when the input is empty
    }
  }

  getBackgroundColor(backgroundColor: number): string {
    switch (backgroundColor) {
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
  onColorChange(color: any) {
    this.colors.forEach(c => c.selected = false);
    color.selected = true;
    this.data.backgroundColor = color.id;
  }
  openDialog() {
    this.display = true;
    this.getUnchosenTags();
  }
}
