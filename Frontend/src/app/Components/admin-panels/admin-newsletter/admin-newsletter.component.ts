import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators, FormControl } from '@angular/forms';
import { User } from '../../../Models/user.model';
import { UserService } from '../../../Services/User/user.service';
import { EmailService } from '../../../Services/Email/email.service';
import { emailModel } from '../../../Models/email.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-newsletter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './admin-newsletter.component.html',
  styleUrl: './admin-newsletter.component.css'
})
export class AdminNewsletterComponent implements OnInit {
  users: User[] = [];
  sendToAll: boolean = false;
  emailForm!: FormGroup;

  isSending: boolean = false;

  constructor(
    private userService: UserService,
    private emailService: EmailService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.emailForm = this.formBuilder.group({
      to: new FormControl({ value: '', disabled: false }, Validators.required),
      subject: ['', Validators.required],
      body: ['', Validators.required],
    });

    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe(users => {
      this.users = users.filter(u => u.newsLetterIsSubscribed);
    });
  }

  selectUser(user: User): void {
    this.sendToAll = false;
    this.emailForm.get('to')?.enable();
    this.emailForm.get('to')?.setValue(user.email);
  }

  toggleSendToAll(): void {
    this.sendToAll = !this.sendToAll;
    const toControl = this.emailForm.get('to');

    if (this.sendToAll) {
      toControl?.disable(); 
      toControl?.setValue('Alle brugere');
    } else {
      toControl?.enable();
      toControl?.setValue('');
    }
  }

  sendEmail(): void {
    if (this.emailForm.invalid) {
      alert('Udfyld alle felter korrekt, før du sender.');
      return;
    }

    const emailData: emailModel = {
      to: this.sendToAll
        ? this.users.map((user) => user.email).join(',')
        : this.emailForm.get('to')?.value,
      subject: this.emailForm.get('subject')?.value,
      body: this.emailForm.get('body')?.value,
    };

    this.isSending = true;
    this.emailService.SendEmail(emailData).subscribe(
      () => {
        this.isSending = false;
        alert('E-mail sendt!');
        this.emailForm.reset();
        this.toggleSendToAll(); 
      },
      (error: any) => {
        this.isSending = false;
        alert('Der opstod en fejl under afsendelse af e-mail.');
        console.error(error);
      }
    );
  }
}
