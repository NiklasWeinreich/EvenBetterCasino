import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { registerLocaleData } from '@angular/common';
import localeDa from '@angular/common/locales/da';
import { LOCALE_ID } from '@angular/core';
import { appConfig } from './app/app.config';

registerLocaleData(localeDa);

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    ...(appConfig.providers || []),
    { provide: LOCALE_ID, useValue: 'da-DK' },
    // hvis du bruger router:
    // provideRouter(routes),
  ],
}).catch((err) => console.error(err));
