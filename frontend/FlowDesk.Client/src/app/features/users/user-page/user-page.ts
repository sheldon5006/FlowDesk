import {
  Component,
  ViewChild
} from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

import { UserList } from '../user-list/user-list';
import { UserForm } from '../user-form/user-form';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [
    ButtonModule,
    DialogModule,
    UserList,
    UserForm
  ],
  templateUrl: './user-page.html',
  styleUrl: './user-page.scss'
})
export class UserPage {

  createDialogVisible = false;

  @ViewChild(UserList)
  userList?: UserList;

  openCreateDialog(): void {
    this.createDialogVisible = true;
  }

  closeCreateDialog(): void {
    this.createDialogVisible = false;
  }

  onUserCreated(): void {
    this.createDialogVisible = false;
    this.userList?.loadUsers();
  }
}