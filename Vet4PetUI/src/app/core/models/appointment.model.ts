import { Animal } from '../services/animal.service';
import { User } from './user.model';

export interface Appointment {
  id: string;
  date: string;
  animal: Animal;
  vet: User;
  owner: User;
  description: string;
} 