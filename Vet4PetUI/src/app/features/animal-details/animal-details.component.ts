import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentService, Appointment, PagedResult } from '../../core/services/appointment.service';
import { AnimalService, Animal } from '../../core/services/animal.service';

@Component({
  selector: 'app-animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.scss']
})
export class AnimalDetailsComponent implements OnInit {
  animalId!: string;
  pagedAppointments: Appointment[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  animal: Animal | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private appointmentService: AppointmentService,
    private animalService: AnimalService
  ) {}

  ngOnInit(): void {
    this.animalId = this.route.snapshot.paramMap.get('animalId') || '';
    this.loadAnimal();
    this.loadAppointments();
  }

  loadAnimal() {
    this.loading = true;
    this.error = null;
    this.animalService.getAnimalsByVetId(Number(this.animalId)).subscribe({
      next: (animals) => {
        this.animal = animals.find(a => a.id === Number(this.animalId)) || null;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching animal:', err);
        this.error = 'Failed to load animal information.';
        this.loading = false;
      }
    });
  }

  loadAppointments() {
    this.appointmentService.getAppointmentsByAnimal(Number(this.animalId), this.page, this.pageSize)
      .subscribe(result => {
        this.pagedAppointments = result.items;
        this.totalPages = result.totalPages;
        this.page = result.pageNumber;
      });
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.page = page;
    this.loadAppointments();
  }
}
