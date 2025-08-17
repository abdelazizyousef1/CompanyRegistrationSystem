import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
export const routes: Routes = [
  {
    path: '',
    redirectTo: 'signup',
    pathMatch: 'full',
  },
  {
    path: 'signup',
    loadComponent: () => import('./pages/company-signup/company-signup').then(m => m.SignUpComponent)
  },
  {
  path: 'verify-otp',
  loadComponent: () =>
    import('./pages/company-verify-otp/company-verify-otp').then(m => m.VerifyOtpComponent)
},
  {
    path : 'set-password',
    loadComponent : () => import('./pages/company-set-password/company-set-password').then(m =>m.CompanySetPassword)
      
    },
  {
    path : 'login',
    loadComponent : () => import('./pages/company-login/company-login').then(m =>m.CompanyLoginComponent)
  },
  
{
path: 'home',
  loadComponent: () => import('./pages/home/home').then(m => m.HomeComponent),
  canActivate: [AuthGuard]
}
];

