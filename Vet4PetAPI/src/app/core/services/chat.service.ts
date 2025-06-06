import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ChatService {
  private hubConnection: HubConnection | null = null;
  private messagesSubject = new BehaviorSubject<any[]>([]);
  public messages$ = this.messagesSubject.asObservable();

  connect(token: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/api/chat', { accessTokenFactory: () => token })
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection.on('ReceiveMessage', (message) => {
      const current = this.messagesSubject.value;
      this.messagesSubject.next([...current, message]);
    });

    this.hubConnection.start().catch(err => console.error('SignalR Connection Error:', err));
  }

  disconnect() {
    this.hubConnection?.stop();
    this.hubConnection = null;
  }

  sendMessage(message: any) {
    this.hubConnection?.invoke('SendMessage', message);
  }

  onMessage(): Observable<any[]> {
    return this.messages$;
  }
} 