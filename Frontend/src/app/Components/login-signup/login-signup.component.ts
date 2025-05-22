import { CommonModule } from '@angular/common';
import { Component, OnInit, Renderer2 } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../Services/Security/auth.service';
import { UserService } from '../../Services/User/user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-login-signup',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.css'],
})
export class LoginSignupComponent implements OnInit {
  showPassword: boolean = false;
  registerForm: FormGroup;
  loginForm: FormGroup;
  message: string = '';

  constructor(
    private authSerice: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      rememberMe: [false],
    });

    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      birthDate: ['', Validators.required],
      newsLetterIsSubscribed: [false],
    });
  }

  ngOnInit(): void {
    // Hvis en bruger allerede er logget ind, redirect
    const currentUser = this.authSerice.currentUserValue;
    if (currentUser && currentUser.id > 0) {
      this.router.navigate(['/']);
    } else {
      console.log('Ingen bruger logget ind endnu.');
    }
  }

  login(): void {
    this.message = '';
    const { email, password, rememberMe } = this.loginForm.value;

    this.authSerice.login(email, password, rememberMe).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        if (error instanceof Error && error.message.includes('udelukket')) {
          this.message = error.message;
        } else if (error.status === 401) {
          this.message = 'Forkert e-mail eller adgangskode.';
        } else {
          this.message = 'Noget gik galt. Prøv igen senere.';
        }
      },
    });
  }

  register(): void {
    this.message = '';

    const user = this.registerForm.value;

    const newUser: User = {
      id: 0,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      password: user.password,
      birthDate: user.birthDate,
      phoneNumber: user.phoneNumber,
      role: 'Customer',
      balance: 0,
      profit: 0,
      loss: 0,
      newsLetterIsSubscribed: user.newsLetterIsSubscribed || false,
    };

    this.authSerice.registerUser(newUser).subscribe({
      next: () => {
        alert('Bruger oprettet! Du kan nu logge ind.');
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 409) {
          this.message = 'Denne e-mail er allerede i brug.';
        } else if (error.status === 400) {
          this.message = 'Udfyld venligst alle felter korrekt.';
        } else {
          this.message = 'Noget gik galt. Prøv igen senere.';
        }
      },
    });
  }

  togglePasswordVisibility() {
    this.showPassword = true;
  }

  hidePassword(input: HTMLInputElement) {
    this.showPassword = false;
  }
}
