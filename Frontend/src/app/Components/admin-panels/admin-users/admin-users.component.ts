import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { resetUser, User } from '../../../Models/user.model';
import { Role, constRoles } from '../../../Models/role.model';
import { UserService } from '../../../Services/User/user.service';

@Component({
  selector: 'app-admin-users',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent implements OnInit {
  users: User[] = [];
  user: User = resetUser();
  roles: Role[] = [];
  message: string = '';
  showCreateForm: boolean = false;
  now: Date = new Date(); 

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadUsers();
    this.loadRoles();

  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users.map(user => {
          if (user.excludedUntil) {
            user.excludedUntil = new Date(user.excludedUntil); 
          }
          return user;
        });
  
        console.log('Brugere hentet:', this.users);
      },
      error: (err) => console.error('Fejl ved hentning af brugere:', err),
    });
  
    this.roles = constRoles; 
  }
  

  updateUser(): void {
    if (!this.user || this.user.id === 0) return; // Undgå ugyldige requests
  
    this.userService.updateUser(this.user).subscribe({
      next: (updatedUser) => {
        console.log('Modtaget opdateret bruger:', updatedUser);
  
        // Find og opdater brugeren i listen
        const index = this.users.findIndex(u => u.id === updatedUser.id);
        if (index !== -1) {
          this.users[index] = updatedUser;
        }

        this.user = resetUser();
      },
      error: (err) => console.error('Fejl ved opdatering af bruger:', err),
    });
  }

  createUser(): void {
    this.message = '';
    if (this.user.id == 0) {
      // Opret
      this.userService.createUser(this.user).subscribe({
        next: (x) => {
          this.users.push(x);
          this.user = resetUser();
          this.showCreateForm = false;
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
        },
      });
    } 
  }

  deleteUser(userId: number): void {
    if (confirm('Er du sikker på, at du vil slette denne bruger?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.users = this.users.filter((user) => user.id !== userId);
        },
        error: (err) => console.error('Fejl ved sletning af bruger:', err),
      });
    }
  }

  editUser(user: User): void {
    this.user = { ...user };
  }

  loadRoles(): void{
    this.roles = constRoles;
  }

  createdUser(): void {
    this.user = resetUser();
    this.user.id = 0;
    this.showCreateForm = true;
  }

  cancelCreate(): void {
    this.showCreateForm = false;
    this.user = resetUser();
  }

  cancelEdit(): void {
    this.user = resetUser();
  }

}
