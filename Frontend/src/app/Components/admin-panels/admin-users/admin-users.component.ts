import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { resetUser, User } from '../../../Models/user.model';
import { Role, constRoles } from '../../../Models/role.model';
import { UserService } from '../../../Services/User/user.service';

declare var bootstrap: any; // Til at styre Bootstrap Modal

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent implements OnInit {
  users: User[] = [];
  user: User = resetUser();
  roles: Role[] = [];
  message: string = '';
  now: Date = new Date();
  isEditing: boolean = false; // Ny! For at vide om vi er i "opret" eller "rediger"

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
  }

  loadRoles(): void {
    this.roles = constRoles;
  }

  openCreateModal(): void {
    this.user = resetUser();
    this.isEditing = false;
    this.showModal();
  }

  openEditModal(user: User): void {
    this.user = { ...user };
    this.isEditing = true;
    this.showModal();
  }

  showModal(): void {
    const modalElement = document.getElementById('userModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  createUser(): void {
    this.message = '';
    if (this.user.id == 0) {
      this.userService.createUser(this.user).subscribe({
        next: () => {
          this.loadUsers(); // <-- Hent ny liste
          this.user = resetUser();
          this.closeModal();
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
        },
      });
    }
  }

  updateUser(): void {
    if (!this.user || this.user.id === 0) return;
    this.userService.updateUser(this.user).subscribe({
      next: () => {
        this.loadUsers(); // <-- Hent ny liste
        this.user = resetUser();
        this.closeModal();
      },
      error: (err) => console.error('Fejl ved opdatering af bruger:', err),
    });
  }

  deleteUser(userId: number): void {
    if (confirm('Er du sikker på, at du vil slette denne bruger?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.users = this.users.filter(user => user.id !== userId);
        },
        error: (err) => console.error('Fejl ved sletning af bruger:', err),
      });
    }
  }

  closeModal(): void {
    const modalElement = document.getElementById('userModal');
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
  }
}
