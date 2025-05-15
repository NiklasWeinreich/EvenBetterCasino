import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { User } from '../Models/user.model';
import { AuthService } from '../Services/Security/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;  
  currentUser: User | null = null;
  roleChecker: string = 'Admin'; 

  constructor(private authService: AuthService, private router: Router) {
    this.authService.currentUser.subscribe((user) => {
      this.currentUser = user;
      this.checkLoginStatus();
    });
  }

  ngOnInit(): void {
    this.checkLoginStatus();
  }

  checkLoginStatus(): void {
    const currentUser = this.authService.currentUserValue;
    this.isLoggedIn = !!currentUser && currentUser.id > 0;
  
    if (this.isLoggedIn && currentUser) {
      // Tjek om brugerens rolle er 'Admin'
      this.roleChecker = currentUser.role; 
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
