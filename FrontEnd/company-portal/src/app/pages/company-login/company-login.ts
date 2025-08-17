import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/auth'; // غيّر المسار حسب مشروعك

@Component({
  selector: 'app-company-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-login.html',
})
export class CompanyLoginComponent {
  loginForm: FormGroup;
  showPassword = false;
  error: string = '';
  success: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: CompanyService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }

  login(): void {
    if (this.loginForm.invalid) return;

    const { email, password } = this.loginForm.value;

    this.authService.login({ email, password }).subscribe({
      next: (res) => {
        console.log('Login response:', res); // لمساعدتك في التتبع

        if (!res.status) {
          this.error = res.message || 'Login failed';
          this.success = '';
          return;
        }

        const token = res.data?.token;
        if (token) {
          localStorage.setItem('token', token);
          this.success = res.message || 'Login successful';
          this.error = '';
          console.log('Navigating to /home...'); // تتبع التنقل
          this.router.navigate(['/home']);
        } else {
          this.error = 'No token received!';
          this.success = '';
        }
      },
      error: (err) => {
        console.error('Login error:', err); // تتبع الخطأ
        this.error = 'Something went wrong. Please try again.';
        this.success = '';
      }
    });
  }
}
