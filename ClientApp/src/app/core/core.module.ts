import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

/** Services */
import { SuccessesHubService } from './services/successes-hub.service';

import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { FooterComponent } from './components/footer/footer.component';

/** PrimeNg */
import { RippleModule } from 'primeng/ripple';

@NgModule({
  declarations: [
    NavMenuComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    RippleModule
  ],
  providers: [
    SuccessesHubService
  ],
  exports: [
    NavMenuComponent,
    FooterComponent
  ]
})
export class CoreModule { }
