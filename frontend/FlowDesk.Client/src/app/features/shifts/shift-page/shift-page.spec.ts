import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftPage } from './shift-page';

describe('ShiftPage', () => {
  let component: ShiftPage;
  let fixture: ComponentFixture<ShiftPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShiftPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShiftPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
