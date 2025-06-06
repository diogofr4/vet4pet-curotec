import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Message } from '../models/message.model';

@Injectable({ providedIn: 'root' })
export class MessageService {
  private apiUrl = 'http://localhost:8080/api/Message';

  constructor(private http: HttpClient) {}

  getMessagesByAnimal(animalId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/by-animal`, {
      params: { animalId: animalId.toString() }
    });
  }

  sendMessage(message: Partial<Message>): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/send`, message);
  }
} 