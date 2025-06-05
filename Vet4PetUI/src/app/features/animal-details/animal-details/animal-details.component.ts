import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// Modelo semelhante ao backend
interface Appointment {
  id: number;
  animalId: number;
  date: string;
  description: string;
  status: string;
}

@Component({
  selector: 'app-animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.scss']
})
export class AnimalDetailsComponent implements OnInit {
  animalId!: string;
  appointments: Appointment[] = [];
  pagedAppointments: Appointment[] = [];
  page = 1;
  pageSize = 5;
  totalPages = 1;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.animalId = this.route.snapshot.paramMap.get('animalId') || '';
    this.mockAppointments();
    this.updatePagedAppointments();
  }

  mockAppointments() {
    this.appointments = Array.from({ length: 23 }).map((_, i) => ({
      id: i + 1,
      animalId: Number(this.animalId),
      date: new Date(Date.now() - i * 86400000).toISOString(),
      description: `Consulta mock #${i + 1} para animal ${this.animalId}`,
      status: i % 2 === 0 ? 'Concluída' : 'Agendada'
    }));
    this.totalPages = Math.ceil(this.appointments.length / this.pageSize);
  }

  updatePagedAppointments() {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.pagedAppointments = this.appointments.slice(start, end);
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.page = page;
    this.updatePagedAppointments();
  }
}
