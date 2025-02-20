export interface RegisterRequest {
  username: string;
  email: string;
  gender: string;
  password: string;
  confirmPassword: string;
  enableTwoFactor: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface TwoFactorRequest {
  email: string;
  token: string;
  provider: string;
}

export interface RefreshTokenRequest {
  accessToken: string;
  refreshToken: string;
}

export interface ResetPasswordRequest {
  email: string;
  token: string;
  password: string;
  confirmPassword: string;
}
