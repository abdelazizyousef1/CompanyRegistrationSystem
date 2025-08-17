import { Component, ViewEncapsulation } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidationErrors
} from '@angular/forms';
import { CompanyService } from '../../services/auth';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-set-password',
  standalone: true,
  templateUrl: './company-set-password.html',
  encapsulation: ViewEncapsulation.None,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
})
export class CompanySetPassword {
  setPasswordForm: FormGroup;
  showPassword = false;
  showConfirmPassword = false;
  success: string | null = null;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    const emailFromQuery = this.route.snapshot.queryParamMap.get('email') || '';

    this.setPasswordForm = this.fb.group(
      {
        email: [{ value: emailFromQuery, disabled: true }, [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(/^(?=.*[A-Z])(?=.*[!@#$%^&*])/)
          ]
        ],
        confirmPassword: ['', Validators.required]
      },
      { validators: this.matchPasswords }
    );
  }

  matchPasswords(group: AbstractControl): ValidationErrors | null {
    const pass = group.get('password')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return pass === confirm ? null : { mismatch: true };
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }

  toggleShowConfirmPassword(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onSubmit(): void {
  if (this.setPasswordForm.invalid) {
    this.setPasswordForm.markAllAsTouched();
    return;
  }

  const { password, confirmPassword } = this.setPasswordForm.value;
  const email = this.setPasswordForm.getRawValue().email;

  this.companyService.setPassword({
    email,
    newPassword: password,
    confirmPassword
  }).subscribe({
    next: (res) => {
      if (res.status) {
        this.success = 'Password set successfully!';
        this.error = '';
        this.router.navigate(['/login']);
      } else {
        this.error = res.message || 'Setting password failed.';
        this.success = '';
      }
    },
    error: () => {
      this.error = 'Server error occurred.';
      this.success = '';
    }
  });
}



  get password() {
    return this.setPasswordForm.get('password');
  }

  get confirmPassword() {
    return this.setPasswordForm.get('confirmPassword');
  }

  get email() {
    return this.setPasswordForm.get('email');
  }
}
