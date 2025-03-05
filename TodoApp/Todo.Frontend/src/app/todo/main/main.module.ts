import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainFormComponent } from './components/main-form.component';
import { MainListComponent } from './components/main-list.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { CheckboxModule } from 'primeng/checkbox';
import { TagModule } from 'primeng/tag';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MessagesModule } from 'primeng/messages';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';

const routes = [
  {
    path: '',
    component: MainListComponent,
  },
  {
    path: 'form',
    component: MainFormComponent,
  },
  {
    path: 'form/:id',
    component: MainFormComponent,
  },
];

@NgModule({
  declarations: [
    MainFormComponent,
    MainListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    InputTextModule,
    ButtonModule,
    ToastModule,
    TagModule,
    TableModule,
    DropdownModule,
    CheckboxModule,
    ConfirmDialogModule,
    DialogModule,
    MessagesModule,
  ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
  
})
export class MainModule { }
