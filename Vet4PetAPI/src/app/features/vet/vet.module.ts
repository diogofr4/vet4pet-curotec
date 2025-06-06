import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { VetRoutingModule } from './vet-routing.module';
import { PatientListComponent } from './patient-list/patient-list.component';
import { PatientDetailComponent } from './patient-detail/patient-detail.component';

@NgModule({
  declarations: [PatientListComponent, PatientDetailComponent],
  imports: [CommonModule, FormsModule, RouterModule, VetRoutingModule],
  exports: [PatientListComponent, PatientDetailComponent]
})
export class VetModule {} 