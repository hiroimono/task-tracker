import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

/** Modules */
import { HomeModule } from './home/home.module';
import { CoreModule } from './core/core.module';

/** Components */
import { AppComponent } from './app.component';
import { WorkshopComponent } from './workshop/workshop.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

/** PrimeNg Modules */
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';

@NgModule({
  declarations: [
    AppComponent,
    WorkshopComponent,
    FetchDataComponent
  ],
  imports: [
    CommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
    HomeModule,
    RippleModule,
    StyleClassModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
