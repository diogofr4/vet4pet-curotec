import { Component } from '@angular/core';
import { Router } from '@angular/router';

interface Patient {
  id: string;
  name: string;
  species: string;
}

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss']
})
export class PatientListComponent {
  patients: Patient[] = [
    { id: '1', name: 'Bella', species: 'Dog' },
    { id: '2', name: 'Max', species: 'Cat' }
  ];

  constructor(private router: Router) {}

  goToDetail(id: string) {
    this.router.navigate([id]);
  }
} 