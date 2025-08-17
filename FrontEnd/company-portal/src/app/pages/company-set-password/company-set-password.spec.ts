import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanySetPassword } from './company-set-password';

describe('CompanySetPassword', () => {
  let component: CompanySetPassword;
  let fixture: ComponentFixture<CompanySetPassword>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanySetPassword]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanySetPassword);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
