import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

interface Appointment {
  id: number;
  date: string;
  description: string;
}

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  standalone: false,
  styleUrls: ['./appointments.component.scss']
})
export class AppointmentsComponent implements OnInit {
  animalId!: string;
  appointments: Appointment[] = [];
  pagedAppointments: Appointment[] = [];
  page = 1;
  pageSize = 5;
  totalPages = 1;

  ngOnInit(): void {
    this.animalId = this.route.snapshot.paramMap.get('animalId') || '';
    this.mockAppointments();
    this.updatePagedAppointments();
  }

  constructor(private route: ActivatedRoute) {}

  mockAppointments() {
    this.appointments = Array.from({ length: 23 }).map((_, i) => ({
      id: i + 1,
      date: new Date(Date.now() - i * 86400000).toISOString(),
      description: `Mocked appointment #${i + 1} for animal ${this.animalId}`
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
