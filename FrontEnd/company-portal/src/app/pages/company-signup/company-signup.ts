import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/auth';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-signup.html',
  encapsulation: ViewEncapsulation.None
})
export class SignUpComponent {
  signupForm: FormGroup;
  signupFormSubmitted = false;
  selectedLogoFile: File | null = null;
  logoInvalid = false;
  success: string | null = null;
  error: string | null = null;
  logoPreview: string | ArrayBuffer | null = null;


  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companyService: CompanyService
  ) {
    this.signupForm = this.fb.group({
      arabicName: ['', Validators.required],
      englishName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      websiteUrl: [''],
      logo: [null]
    });
  }

  onFileChange(event: Event): void {
  const input = event.target as HTMLInputElement;
  const file = input?.files?.[0];

  if (file && this.isImageFile(file)) {
    this.selectedLogoFile = file;
    this.signupForm.patchValue({ logo: file.name });
    this.logoInvalid = false;

    const reader = new FileReader();
    reader.onload = () => {
      this.logoPreview = reader.result;
    };
    reader.readAsDataURL(file);
  } else {
    this.selectedLogoFile = null;
    this.logoInvalid = true;
    this.logoPreview = null;
  }
}


  private isImageFile(file: File): boolean {
    return file.type.startsWith('image/') && ['image/jpeg', 'image/png', 'image/jpg'].includes(file.type);
  }

  onSubmit() {
  this.signupFormSubmitted = true;

  if (this.signupForm.invalid) {
    this.signupForm.markAllAsTouched();
    return;
  }

  const formData = new FormData();
  formData.append('arabicName', this.signupForm.get('arabicName')?.value);
  formData.append('englishName', this.signupForm.get('englishName')?.value);
  formData.append('email', this.signupForm.get('email')?.value);
  formData.append('phoneNumber', this.signupForm.get('phoneNumber')?.value || '');
  formData.append('websiteUrl', this.signupForm.get('websiteUrl')?.value || '');

  if (this.selectedLogoFile) {
    formData.append('logo', this.selectedLogoFile);
  }

  this.companyService.registerCompany(formData).subscribe({
  next: (res: any) => {
    if (res?.status === true) {
      this.success = res.data || 'sign up seccessful.';
      this.router.navigate(['/verify-otp'], {
        queryParams: { 
          email: this.signupForm.get('email')?.value 
        },
        state: {
          otp: res.data  // 
        }
      });
    } else {
      this.error = res.message || 'there is wrong.';
    }
  },
    error: (err) => {
      if (err?.error?.errors) {
        const validationErrors = err.error.errors;
        const messages = [];

        for (const key in validationErrors) {
          if (validationErrors.hasOwnProperty(key)) {
            messages.push(...validationErrors[key]);
          }
        }

        this.error = messages.join(' | ');
      } else {
        this.error = err?.error?.message || 'there is wrong.';
      }
    }
  });
}
}
