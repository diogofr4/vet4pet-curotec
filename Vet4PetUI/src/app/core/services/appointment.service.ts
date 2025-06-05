import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Appointment {
  id: number;
  animalId: number;
  animal: any;
  vetId: number;
  vet: {
    id: number;
    name: string;
    email: string;
    role: number;
  };
  ownerId: number;
  owner: {
    id: number;
    name: string;
    email: string;
    role: number;
  };
  date: string;
  description: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private apiUrl = 'http://localhost:8080/api';

  constructor(private http: HttpClient) { }

  getAppointmentsByAnimal(animalId: number, page: number = 1, pageSize: number = 10): Observable<PagedResult<Appointment>> {
    return this.http.get<PagedResult<Appointment>>(`${this.apiUrl}/Appointment/by-animal`, {
      params: {
        animalId: animalId.toString(),
        page: page.toString(),
        pageSize: pageSize.toString()
      }
    });
  }
} 