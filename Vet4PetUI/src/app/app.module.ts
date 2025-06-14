import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './features/auth/auth.module';
import { authReducer } from './core/store/auth/auth.reducer';
import { AuthEffects } from './core/store/auth/auth.effects';
import { environment } from '../environments/environment';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { AnimalDetailsComponent } from './features/animal-details/animal-details.component';

@NgModule({
  declarations: [
    AppComponent,
    AnimalDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AuthModule,
    MatCardModule,
    FormsModule,
    StoreModule.forRoot({ auth: authReducer }),
    EffectsModule.forRoot([AuthEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production
    }),
    MatFormFieldModule,
    MatIconModule,
    MatInputModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
