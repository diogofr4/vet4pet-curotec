import { Component, OnInit } from '@angular/core';

interface User {
  id: number;
  name: string;
  email: string;
}

interface Appointment {
  id: number;
  animalId: number;
  date: string;
  description: string;
}

interface Animal {
  id: number;
  name: string;
  species: string;
  breed: string;
  age: number;
  ownerId: number;
  owner: User;
  vetId: number;
  vet: User;
  appointments: Appointment[];
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: false
})
export class DashboardComponent implements OnInit {
  animals: Animal[] = [
    {
      id: 1,
      name: 'Rex',
      species: 'Dog',
      breed: 'Labrador',
      age: 5,
      ownerId: 10,
      owner: { id: 10, name: 'John Doe', email: 'john@example.com' },
      vetId: 2,
      vet: { id: 2, name: 'Dr. Smith', email: 'drsmith@example.com' },
      appointments: [
        { id: 100, animalId: 1, date: '2024-05-01T10:00:00', description: 'Checkup' },
        { id: 101, animalId: 1, date: '2024-07-01T10:00:00', description: 'Vaccination' }
      ]
    }
  ];
  userRole: number | null = 1; // For demo, assume vet

  constructor() {}

  ngOnInit(): void {}

  getLastAppointment(animal: Animal): Appointment | null {
    if (!animal.appointments.length) return null;
    return animal.appointments.reduce((prev, curr) => new Date(curr.date) > new Date(prev.date) ? curr : prev);
  }

  getNextAppointment(animal: Animal): Appointment | null {
    const now = new Date();
    const future = animal.appointments.filter(a => new Date(a.date) > now);
    if (!future.length) return null;
    return future.reduce((prev, curr) => new Date(curr.date) < new Date(prev.date) ? curr : prev);
  }

  onAnimalClick(animal: Animal): void {
    alert('Clicked on ' + animal.name);
  }
} 