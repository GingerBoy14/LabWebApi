import { Component, OnInit } from '@angular/core';

import { AdminService } from './../../../../core/services/Admin.service';
import { AlertService } from './../../../../core/services/Alert.service';
import { AuthorizationRoles } from 'src/app/configs/auth-roles';
import { EditUserDialogComponent } from './../edit-user-dialog/edit-user-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { UserInfo } from 'src/app/core/models/admin/UserInfo';

@Component({
  selector: 'users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css'],
})
export class UsersListComponent implements OnInit {
  users: UserInfo[];
  constructor(
    private adminService: AdminService,
    private alertService: AlertService,   
    private dialog: MatDialog
  ) {}
  async ngOnInit() {
    this.adminService
      .getUsers()
      .subscribe((data: UserInfo[]) => (this.users = data));
  }
  isAdminRole(user: UserInfo): boolean {
    return user.role == AuthorizationRoles.Admin;
  }
  async editUser(user: UserInfo) {
      console.log(user)
    const dialogRef = this.dialog.open(EditUserDialogComponent, {
      data: { ...user },
    });
    dialogRef.afterClosed().subscribe((result: UserInfo) => {
      if (result) {
        this.adminService.editUser(result).subscribe((data: any) => {
          let modelIndex = this.users.findIndex((x: any) => x.id == data.id);
          if (modelIndex !== -1) {
            this.users.splice(modelIndex, 1, data);
            this.users = [...this.users];
          } else {
            this.alertService.errorAlert('Update User', 'Failed!');
          }
        });
      }
    });
  }
  async deleteUser(userId: any) {
    const confirmed = await this.alertService.okCancalAlert(
      `Do you really want to delete this user?`
    );
    if (confirmed) {
      this.adminService.deleteUser(userId).subscribe(() => {
        const newList = this.users.filter((item) => item.id == userId);
        this.users = newList;
      });
    }
  }
}
