import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentService, Appointment, PagedResult } from '../../core/services/appointment.service';
import { AnimalService, Animal } from '../../core/services/animal.service';
import { MessageService } from '../../core/services/message.service';
import { Message } from '../../core/models/message.model';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.scss']
})
export class AnimalDetailsComponent implements OnInit {
  animalId!: string;
  pagedAppointments: Appointment[] = [];
  page = 1;
  pageSize = 10;
  totalPages = 1;
  animal: Animal | null = null;
  loading = false;
  error: string | null = null;

  // Chat state
  messages: Message[] = [
    {
      id: 1,
      animalId: 1,
      senderId: 1,
      senderRole: 'veterinarian',
      receiverId: 2,
      receiverRole: 'owner',
      content: 'Hello, how is your pet today?',
      timestamp: new Date().toISOString()
    },
    {
      id: 2,
      animalId: 1,
      senderId: 2,
      senderRole: 'owner',
      receiverId: 1,
      receiverRole: 'veterinarian',
      content: 'He is doing well, thank you!',
      timestamp: new Date(Date.now() - 60000).toISOString()
    }
  ];
  newMessage: string = '';
  sending = false;
  chatError: string | null = null;
  user: any = { userId: 1, role: 1, name: 'Dr. Vet' };

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

  // Chat logic
  // loadMessages() {
  //   this.messageService.getMessagesByAnimal(Number(this.animalId)).subscribe({
  //     next: (msgs) => {
  //       this.messages = msgs;
  //     },
  //     error: (err) => {
  //       this.chatError = 'Failed to load messages.';
  //     }
  //   });
  // }

  sendMessage() {
    if (!this.newMessage.trim()) return;
    this.sending = true;
    this.chatError = null;
    const senderRole = this.user?.role === 1 ? 'veterinarian' : 'owner';
    const receiverRole = senderRole === 'veterinarian' ? 'owner' : 'veterinarian';
    const senderId = this.user?.userId;
    const receiverId = receiverRole === 'owner' ? 2 : 1;
    const newMsg: Message = {
      id: this.messages.length + 1,
      animalId: 1,
      senderId,
      senderRole,
      receiverId,
      receiverRole,
      content: this.newMessage,
      timestamp: new Date().toISOString()
    };
    this.messages.push(newMsg);
    this.newMessage = '';
    this.sending = false;
  }
}
