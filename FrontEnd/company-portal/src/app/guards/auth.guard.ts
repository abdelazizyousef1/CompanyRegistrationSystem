import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');

    // تحقق إن التوكن موجود وطوله معقول
    if (token && token.length > 10) {
      return true;
    }

    // لو مفيش توكن، رجّع المستخدم لصفحة تسجيل الدخول
    this.router.navigate(['/login']);
    return false;
  }
}
