import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

/** Modules */
import { HomeModule } from './home/home.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

/** Components */
import { AppComponent } from './app.component';
import { WorkshopComponent } from './screens/workshop/workshop.component';
import { FetchDataComponent } from './screens/fetch-data/fetch-data.component';

@NgModule({
  declarations: [
    AppComponent,
    WorkshopComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    SharedModule,
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
    HomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
