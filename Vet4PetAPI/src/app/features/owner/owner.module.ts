import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { OwnerRoutingModule } from './owner-routing.module';
import { ConsultationListComponent } from './consultation-list/consultation-list.component';
import { ConsultationDetailComponent } from './consultation-detail/consultation-detail.component';

@NgModule({
  declarations: [ConsultationListComponent, ConsultationDetailComponent],
  imports: [CommonModule, FormsModule, RouterModule, OwnerRoutingModule],
  exports: [ConsultationListComponent, ConsultationDetailComponent]
})
export class OwnerModule {} 