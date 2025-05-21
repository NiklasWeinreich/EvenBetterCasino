import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../Services/Security/auth.service';

@Component({
  selector: 'app-forgot-password',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
  standalone: true
})
export class ForgotPasswordComponent {
  forgotForm: FormGroup;
  submitted = false;
  message: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  submit() {
    this.submitted = true;

    if (this.forgotForm.invalid) return;

    this.authService.forgotPassword(this.forgotForm.value.email).subscribe({
      next: (res) => {
        this.message = res.message || 'En e-mail med nulstillingslink er sendt.';
      },
      error: () => {
        this.message = 'Kunne ikke finde bruger med denne e-mail.';
      }
    });
  }
}


