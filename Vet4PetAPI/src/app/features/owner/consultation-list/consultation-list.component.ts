import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface Consultation {
  id: number;
  date: string;
  vet: string;
  animal: string;
}

@Component({
  selector: 'app-consultation-list',
  templateUrl: './consultation-list.component.html',
  styleUrls: ['./consultation-list.component.scss']
})
export class ConsultationListComponent implements OnInit {
  consultations: Consultation[] = [];
  loading = false;
  error: string | null = null;

  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit() {
    this.loading = true;
    this.http.get<Consultation[]>('/api/appointment').subscribe({
      next: (data) => {
        this.consultations = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load consultations.';
        this.loading = false;
      }
    });
  }

  goToDetail(id: number) {
    this.router.navigate([id]);
  }
} 