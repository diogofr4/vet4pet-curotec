import { Component, OnInit } from '@angular/core';
import { AnimalService, Animal } from 'src/app/core/services/animal.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: false
})
export class DashboardComponent implements OnInit {
  animals: Animal[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private animalService: AnimalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchAnimals();
  }

  fetchAnimals(): void {
    this.loading = true;
    this.error = null;
    const userStr = localStorage.getItem('user');
    let vetId: number | null = null;
    
    if (userStr) {
      try {
        const user = JSON.parse(userStr);
        vetId = user.userId;
      } catch (e) {
        this.error = 'Invalid user data.';
        this.loading = false;
        return;
      }
    }

    if (!vetId) {
      this.error = 'Authentication required.';
      this.loading = false;
      return;
    }

    this.animalService.getAnimalsByVetId(vetId).subscribe({
      next: (animals) => {
        this.animals = animals;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching animals:', err);
        this.error = 'Failed to load animals.';
        this.loading = false;
      }
    });
  }

  getLastAppointment(animal: Animal): any | null {
    if (!animal.appointments?.length) return null;
    return animal.appointments.reduce((prev, curr) => 
      new Date(curr.date) > new Date(prev.date) ? curr : prev
    );
  }

  getNextAppointment(animal: Animal): any | null {
    if (!animal.appointments?.length) return null;
    const now = new Date();
    const future = animal.appointments.filter(a => new Date(a.date) > now);
    if (!future.length) return null;
    return future.reduce((prev, curr) => 
      new Date(curr.date) < new Date(prev.date) ? curr : prev
    );
  }

  onAnimalClick(animal: Animal): void {
    console.log('Clicked on animal:', animal);
  }

  goToAnimalDetails(animal: any) {
    this.router.navigate(['/animal-details', animal.id]);
  }
} 