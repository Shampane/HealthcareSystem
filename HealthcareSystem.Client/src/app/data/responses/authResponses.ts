export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
  expireInMinutes: number;
}

export interface UserInfoResponse {
  name: string;
  email: string;
}

export interface IsAuthenticatedResponse {
  isAuthenticated: boolean;
}
