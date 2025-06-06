import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatService } from '../../../core/services/chat.service';
import { AuthService } from '../../../core/auth.service';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-consultation-detail',
  templateUrl: './consultation-detail.component.html',
  styleUrls: ['./consultation-detail.component.scss']
})
export class ConsultationDetailComponent implements OnInit, OnDestroy {
  consultationId: string | null = null;
  consultation: any = null;
  loading = false;
  error: string | null = null;
  chatMessages: any[] = [];
  chatInput = '';
  private chatSub?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private chatService: ChatService,
    private authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.consultationId = this.route.snapshot.paramMap.get('id');
    if (this.consultationId) {
      this.loading = true;
      this.http.get(`/api/appointment/${this.consultationId}`).subscribe({
        next: (data) => {
          this.consultation = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load consultation details.';
          this.loading = false;
        }
      });
    }
    const token = this.authService.getToken();
    if (token) {
      this.chatService.connect(token);
      this.chatSub = this.chatService.onMessage().subscribe(messages => {
        this.chatMessages = messages;
      });
    }
  }

  ngOnDestroy() {
    this.chatService.disconnect();
    this.chatSub?.unsubscribe();
  }

  sendMessage() {
    if (this.chatInput.trim()) {
      this.chatService.sendMessage({
        text: this.chatInput,
        consultationId: this.consultationId,
        sender: 'Owner',
        timestamp: new Date().toISOString()
      });
      this.chatInput = '';
    }
  }
} 