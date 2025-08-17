import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyService } from '../../services/auth';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-verify-otp',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './company-verify-otp.html',
  encapsulation: ViewEncapsulation.None,
})
export class VerifyOtpComponent {
  form: FormGroup;
  success: string | null = null;
  error: string | null = null;
  countdown = 0;
  otpFromState: any;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private companyService: CompanyService
  ) {
    const otpFromState = this.router.getCurrentNavigation()?.extras?.state?.['otp'] ?? '';
    this.otpFromState = otpFromState;
    console.log('OTP from state:', otpFromState);

    const emailFromQuery = this.route.snapshot.queryParamMap.get('email') ?? '';
    this.form = this.fb.group({
      email: [{ value: emailFromQuery, disabled: true }, [Validators.required, Validators.email]],
      otpCode: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(6)]]
    });
  }

  sendOtp() {
  const email = this.form.getRawValue().email;
  this.companyService.sendOtp({ email }).subscribe({
    next: (res) => {
      this.success = 'OTP resent to your email.';
      this.error = null;
      this.startCountdown();

      if (res && res.data) {
        this.otpFromState = res.data;
        console.log('New OTP from resend:', res.data);
      }
    },
    error: () => {
      this.error = 'Failed to resend OTP.';
      this.success = null;
    }
  });
}


  startCountdown() {
    this.countdown = 30;
    const interval = setInterval(() => {
      this.countdown--;
      if (this.countdown === 0) {
        clearInterval(interval);
      }
    }, 1000);
  }

  onSubmit() {
    const body = {
      email: this.form.get('email')?.value,
      otpCode: this.form.get('otpCode')?.value
    };

    this.companyService.verifyOtp(body).subscribe({
      next: (res) => {
        if (res.status === true) {
          this.success = 'OTP verified successfully!';
          this.error = '';
          this.router.navigate(['/set-password'], { queryParams: { email: body.email } });
        } else {
          this.error = res.message || 'Invalid OTP.';
          this.success = '';
        }
      },
      error: () => {
        this.error = 'Something went wrong while verifying the OTP.';
        this.success = '';
      }
    });
  }
}
