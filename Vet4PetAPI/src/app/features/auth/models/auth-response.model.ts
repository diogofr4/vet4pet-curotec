export interface AuthResponse {
  token: string;
  userId: string;
  username: string;
  email: string;
  role: 'Vet' | 'Owner';
} 