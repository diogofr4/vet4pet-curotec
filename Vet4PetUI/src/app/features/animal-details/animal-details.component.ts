import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentService, Appointment, PagedResult } from '../../core/services/appointment.service';
import { AnimalService, Animal } from '../../core/services/animal.service';
import { MessageService } from '../../core/services/message.service';
import { Message } from '../../core/models/message.model';
import { AuthService } from '../../core/services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.scss'],
  standalone: false,
})
export class AnimalDetailsComponent implements OnInit, OnDestroy {
  animalId!: string;
  pagedAppointments: Appointment[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  animal: Animal | null = null;
  loading = false;
  error: string | null = null;

  // Chat state
  messages: Message[] = [];
  newMessage: string = '';
  sending = false;
  chatError: string | null = null;
  user: any = null;
  private messageSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private appointmentService: AppointmentService,
    private animalService: AnimalService,
    private messageService: MessageService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.animalId = this.route.snapshot.paramMap.get('animalId') || '';
    this.user = this.authService.getUser();
    this.loadAnimal();
    this.loadAppointments();
    this.initializeChat();
  }

  ngOnDestroy(): void {
    this.messageSubscription?.unsubscribe();
    this.messageService.stopConnection();
  }

  private async initializeChat() {
    try {
      const token = this.authService.getToken();
      if (!token) {
        this.chatError = 'Authentication required for chat';
        return;
      }

      await this.messageService.startConnection(token);
      await this.messageService.joinAnimalChat(Number(this.animalId));

      // Carregar histórico de mensagens
      this.messageService.getMessagesByAnimal(Number(this.animalId)).subscribe({
        next: (msgs) => {
          this.messages = msgs;
        },
        error: (err) => {
          this.chatError = 'Failed to load messages';
        }
      });

      // Inscrever para novas mensagens
      this.messageSubscription = this.messageService.message$.subscribe(message => {
        this.messages.push(message);
      });
    } catch (err) {
      this.chatError = 'Failed to initialize chat';
    }
  }

  loadAnimal() {
    this.loading = true;
    this.error = null;
    this.animalService.getAnimalsByVetId(Number(this.animalId)).subscribe({
      next: (animals) => {
        this.animal = animals.find(a => a.id === Number(this.animalId)) || null;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching animal:', err);
        this.error = 'Failed to load animal information.';
        this.loading = false;
      }
    });
  }

  loadAppointments() {
    this.appointmentService.getAppointmentsByAnimal(Number(this.animalId), this.page, this.pageSize)
      .subscribe(result => {
        this.pagedAppointments = result.items;
        this.totalPages = result.totalPages;
        this.page = result.pageNumber;
      });
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.page = page;
    this.loadAppointments();
  }

  async sendMessage() {
    if (!this.newMessage.trim() || !this.animal) return;
    this.sending = true;
    this.chatError = null;

    try {
      const senderRole = this.user?.role === 1 ? 'veterinarian' : 'owner';
      const receiverRole = senderRole === 'veterinarian' ? 'owner' : 'veterinarian';
      const senderId = this.user?.userId;
      const receiverId = receiverRole === 'owner' ? this.animal.ownerId : this.animal.vetId;

      await this.messageService.sendMessage({
        animalId: Number(this.animalId),
        senderId,
        senderRole,
        receiverId,
        receiverRole,
        content: this.newMessage
      });

      this.newMessage = '';
      
      // Recarregar mensagens após envio bem-sucedido
      this.messageService.getMessagesByAnimal(Number(this.animalId)).subscribe({
        next: (msgs) => {
          this.messages = msgs;
          // Scroll para a última mensagem
          setTimeout(() => {
            const chatMessages = document.querySelector('.chat-messages');
            if (chatMessages) {
              chatMessages.scrollTop = chatMessages.scrollHeight;
            }
          });
        },
        error: (err) => {
          this.chatError = 'Failed to refresh messages';
        }
      });
    } catch (err) {
      this.chatError = 'Failed to send message';
    } finally {
      this.sending = false;
    }
  }
}
