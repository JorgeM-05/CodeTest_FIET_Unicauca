import { exampleComponent } from './component/configurations-mcas/example/example.component';
import { NgModule, Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { MatDialogModule } from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';




import { McasService } from './service/mcas.service';
import { AppComponent } from './app.component';
import { MenuComponent } from './component/menu/menu.component';
import { ConfigurationsCustomerComponent } from './component/menu/configurations-customer/configurations-customer.component';
import { LoginComponent } from './component/menu/login/login.component';
import { ConfigurationsMCAsComponent } from './component/configurations-mcas/configurations-mcas.component';
import { AppRoutingModule } from './app-routing.module';
import { ListProjectsComponent } from './component/configurations-mcas/list-projects/list-projects.component';
import { CreationNewProjectsComponent } from './component/configurations-mcas/creation-new-projects/creation-new-projects.component';
import { CookieService } from 'ngx-cookie-service';
import { Highlight, HighlightModule, HighlightOptions, HIGHLIGHT_OPTIONS } from 'ngx-highlightjs';
import { HighlightJsModule } from 'ngx-highlight-js';

import 'highlightjs-line-numbers.js'
import 'highlightjs-line-numbers.js/dist/highlightjs-line-numbers.min.js';
import { TablaDatosComponent } from './component/tabla-datos/tabla-datos.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EncuestaModalComponent } from './component/configurations-mcas/creation-new-projects/encuesta-modal/encuesta-modal.component'

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ConfigurationsCustomerComponent,
    LoginComponent,
    ConfigurationsMCAsComponent,
    ListProjectsComponent,
    CreationNewProjectsComponent,
    exampleComponent,
    TablaDatosComponent,
    EncuestaModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HighlightModule,
    HighlightJsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatRadioModule,
    MatSnackBarModule,
    MatIconModule
  ],
  entryComponents: [EncuestaModalComponent],
  providers: [
    McasService,
    CookieService,
    {
      provide:HIGHLIGHT_OPTIONS,
      useValue:<HighlightOptions>{
        //lineNumbers:true,
        //lineNumbersLoader: () => import('highlightjs-line-numbers.js/src/'),
        coreLibraryLoader: () => import('highlight.js/lib/core'),
        fullLibraryLoader: () => import('highlight.js')
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
