import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { HttpClient } from '@angular/common/http';
import { emailModel } from '../../Models/email.model';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class EmailService {
    private readonly EmailUrl = environment.apiUrl + '/Email';
  
    constructor(private Http: HttpClient) {}
  
    SendEmail(emailData: emailModel): Observable<any> {
      console.log('Sender email:', emailData);
      return this.Http.post(this.EmailUrl, emailData);
    }
  }
