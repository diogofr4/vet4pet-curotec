export interface User {
  id: string;
  username: string;
  email: string;
  role: 'Vet' | 'Owner';
  token: string;
} 