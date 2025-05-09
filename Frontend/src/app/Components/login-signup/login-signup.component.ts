import { CommonModule } from '@angular/common';
import { Component, OnInit, Renderer2} from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
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
    private userService: UserService,
    private router: Router,
    private formBuilder: FormBuilder,
    private renderer: Renderer2
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
    console.log(
      'Forsøger at logge ind med:',
      email,
      password,
      'Husk mig?',
      rememberMe
    );
    this.authSerice.login(email, password, rememberMe).subscribe({
      next: (loginResp) => {
        console.log('Login succesfuldt! Modtog (LoginResponse):', loginResp);
        this.router.navigate(['/']);
        this.message = 'Login Successful';
        console.log('Navigerer til /');
        alert(this.message);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Login error:', err);
        if (err.status === 401) {
          this.message = 'Forkert brugernavn eller adgangskode.';
        } else {
          this.message = 'Noget gik galt ved login. Prøv igen.';
        }
        alert(this.message);
      },
    });
  }


  register(): void {
    this.message = '';
    // Læs data fra registerForm
    const { firstName, lastName, email, password, age} =
      this.registerForm.value;

    const newUser: User = {
      id: 0,
      firstName: firstName,
      lastName: lastName,
      role: 'Customer',
      email: email,
      birthDate: age,
      password: password,
      phoneNumber: 0,
      balance: 0,
      profit: 0,
      loss: 0,
      newsLetterIsSubscribed: false, 
    };

    console.log('Forsøger at registrere ny bruger:', newUser);

    // Kald userService.createUser(...) for at oprette bruger i backend
    this.userService.createUser(newUser).subscribe({
      next: (createdUser) => {
        console.log('Bruger oprettet!', createdUser);
        alert('Brugeren er nu oprettet! Du kan nu logge ind.');
      },
      error: (error: HttpErrorResponse) => {
        console.error('Fejl ved registrering:', error);
        if (error.status === 400 && error.error?.errors) {
          const validationErrors = Object.values(error.error.errors).join(', ');
          this.message = `Valideringsfejl: ${validationErrors}`;
          alert(this.message);
        } else {
          this.message = 'Noget gik galt ved registrering. Prøv igen.';
          alert(this.message);
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

