import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatService } from '../../../core/services/chat.service';
import { AuthService } from '../../../core/auth.service';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss']
})
export class PatientDetailComponent implements OnInit, OnDestroy {
  patientId: string | null = null;
  patient: any = null;
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
    this.patientId = this.route.snapshot.paramMap.get('id');
    if (this.patientId) {
      this.loading = true;
      this.http.get(`/api/animal/${this.patientId}`).subscribe({
        next: (data) => {
          this.patient = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load patient details.';
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
        patientId: this.patientId,
        sender: 'Vet',
        timestamp: new Date().toISOString()
      });
      this.chatInput = '';
    }
  }
} 