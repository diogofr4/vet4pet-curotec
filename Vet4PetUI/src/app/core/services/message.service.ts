import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Message } from '../models/message.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private apiUrl = `${environment.apiUrl}/Message`;
  private hubConnection: HubConnection | null = null;
  private messageSubject = new Subject<Message>();
  public message$ = this.messageSubject.asObservable();

  constructor(private http: HttpClient) {}

  startConnection(token: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/chat`, {
        accessTokenFactory: () => token
      })
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection.on('ReceiveMessage', (message: Message) => {
      this.messageSubject.next(message);
    });

    return this.hubConnection.start();
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
      this.hubConnection = null;
    }
  }

  joinAnimalChat(animalId: number): Promise<void> {
    return this.hubConnection?.invoke('JoinAnimalChat', animalId) || Promise.resolve();
  }

  leaveAnimalChat(animalId: number): Promise<void> {
    return this.hubConnection?.invoke('LeaveAnimalChat', animalId) || Promise.resolve();
  }

  sendMessage(message: Partial<Message>): Promise<void> {
    return this.hubConnection?.invoke('SendMessage', message) || Promise.resolve();
  }

  getMessagesByAnimal(animalId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/by-animal/${animalId}`);
  }

  getMessagesBetweenUsers(senderId: number, receiverId: number, animalId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/between-users`, {
      params: {
        senderId: senderId.toString(),
        receiverId: receiverId.toString(),
        animalId: animalId.toString()
      }
    });
  }
} 