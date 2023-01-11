import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

/** PrimeNg Modules */
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { TooltipModule } from 'primeng/tooltip';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { RegisterComponent } from './components/register/register.component';
import { CheckboxModule } from 'primeng/checkbox';
import { BadgeModule } from 'primeng/badge';
import { MessageModule } from 'primeng/message';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RippleModule,
    StyleClassModule,
    AvatarModule,
    AvatarGroupModule,
    TooltipModule,
    ButtonModule,
    DynamicDialogModule,
    CheckboxModule,
    BadgeModule,
    MessageModule,
  ],
  exports: [
    FormsModule,
    RegisterComponent,
    CommonModule,
    RippleModule,
    StyleClassModule,
    AvatarModule,
    AvatarGroupModule,
    TooltipModule,
    ButtonModule,
    DynamicDialogModule,
    CheckboxModule,
    BadgeModule,
    MessageModule,
  ]
})
export class SharedModule { }
