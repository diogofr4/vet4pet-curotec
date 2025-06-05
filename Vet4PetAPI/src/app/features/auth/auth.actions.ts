import { createAction, props } from '@ngrx/store';
import { LoginRequest } from './models/login-request.model';
import { AuthResponse } from './models/auth-response.model';

export const login = createAction('[Auth] Login', props<{ credentials: LoginRequest }>());
export const loginSuccess = createAction('[Auth] Login Success', props<{ response: AuthResponse }>());
export const loginFailure = createAction('[Auth] Login Failure', props<{ error: string }>());
export const logout = createAction('[Auth] Logout');
export const clearError = createAction('[Auth] Clear Error'); 