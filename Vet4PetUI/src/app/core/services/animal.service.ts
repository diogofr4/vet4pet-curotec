import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Animal {
  id: number;
  name: string;
  species: string;
  breed: string;
  age: number;
  ownerId: number;
  owner: {
    id: number;
    name: string;
    email: string;
  };
  vetId: number;
  vet: {
    id: number;
    name: string;
    email: string;
  };
  appointments: {
    id: number;
    animalId: number;
    date: string;
    description: string;
  }[];
}

@Injectable({
  providedIn: 'root'
})
export class AnimalService {
  private apiUrl = `${environment.apiUrl}/Animal`;

  constructor(private http: HttpClient) {}

  getAnimalsByVetId(vetId: number): Observable<Animal[]> {
    return this.http.get<Animal[]>(`${this.apiUrl}/vet/${vetId}`);
  }
} 