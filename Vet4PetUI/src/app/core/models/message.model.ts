export interface Message {
  id: number;
  animalId: number;
  senderId: number;
  senderRole: 'veterinarian' | 'owner';
  receiverId: number;
  receiverRole: 'veterinarian' | 'owner';
  content: string;
  timestamp: string;
} 