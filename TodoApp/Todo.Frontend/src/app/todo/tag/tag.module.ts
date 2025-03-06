import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TagListComponent } from './components/tag-list.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MessagesModule } from 'primeng/messages';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';


const routes = [
  {
    path: '',
    component: TagListComponent,
  },

];


@NgModule({
  declarations: [
    TagListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    InputTextModule,
    ButtonModule,
    ToastModule,
    TableModule,
    DropdownModule,
    CheckboxModule,
    ConfirmDialogModule,
    DialogModule,
    MultiSelectModule,
    MessagesModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]

})
export class TagModule { }
