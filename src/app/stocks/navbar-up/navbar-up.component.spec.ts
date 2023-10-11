import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarUpComponent } from './navbar-up.component';

describe('NavbarUpComponent', () => {
  let component: NavbarUpComponent;
  let fixture: ComponentFixture<NavbarUpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavbarUpComponent]
    });
    fixture = TestBed.createComponent(NavbarUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
