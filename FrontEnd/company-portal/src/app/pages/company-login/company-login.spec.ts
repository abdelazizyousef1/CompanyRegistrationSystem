import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyLogin } from './company-login';

describe('CompanyLogin', () => {
  let component: CompanyLogin;
  let fixture: ComponentFixture<CompanyLogin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyLogin]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyLogin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
