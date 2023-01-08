import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/** PrimeNg Modules */
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { TooltipModule } from 'primeng/tooltip';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogModule } from 'primeng/dynamicdialog';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RippleModule,
    StyleClassModule,
    AvatarModule,
    AvatarGroupModule,
    TooltipModule
  ],
  exports: [
    CommonModule,
    RippleModule,
    StyleClassModule,
    AvatarModule,
    AvatarGroupModule,
    TooltipModule,
    ButtonModule,
    DynamicDialogModule
  ]
})
export class SharedModule { }
