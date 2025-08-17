import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CompanyService {
  private readonly baseUrl = 'https://localhost:7018/api';

  constructor(private http: HttpClient) {}

  registerCompany(formData: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/register`, formData);
  }

  
verifyOtp(body: { email: string; otpCode: string }) {
  return this.http.post<{ status: boolean; message: string }>(
    `${this.baseUrl}/Otp/VerifyOTP`,
    body
  );
}

  sendOtp(body: { email: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/Otp/sendOTP`, body);
  }

  setPassword(body: { email: string; newPassword: string; confirmPassword: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/set-password`, body);
  }
  login(body: { email: string; password: string }): Observable<any> {
  return this.http.post(`${this.baseUrl}/Auth/login`, body);
}
getHome(): Observable<{ logoUrl: string; message: string }> {
  return this.http.get<{ logoUrl: string; message: string }>(
    `${this.baseUrl}/home/get-home`
  );
}

}
