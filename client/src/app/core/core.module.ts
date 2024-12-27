import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { UnAuthenticatedComponent } from './un-authenticated/un-authenticated.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { HeaderComponent } from './header/header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';
// import { MatToolbarModule, MatSidenavModule, MatListModule, MatButtonModule, MatIconModule } from "@angular/material";
// import { FlexLayoutModule } from "@angular/flex-layout";


@NgModule({
  declarations: [
      NavbarComponent,
      NotFoundComponent,
      UnAuthenticatedComponent,
      ServerErrorComponent,
      HeaderComponent
    ],
  imports: [
    CommonModule,
    RouterModule,
    BreadcrumbModule,
    NgxSpinnerModule,
    // MatToolbarModule,
    // MatSidenavModule,
    // MatListModule,
    // MatIconModule,
    // MatButtonModule,
    // FlexLayoutModule
  ],
  exports:[
    NavbarComponent,
    HeaderComponent,
    NgxSpinnerModule
  ]
})
export class CoreModule { }
