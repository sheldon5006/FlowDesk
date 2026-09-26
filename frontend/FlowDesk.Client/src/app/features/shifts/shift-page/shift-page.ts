import { Component, viewChild } from '@angular/core';
import { ShiftList } from '../shift-list/shift-list';
import { ShiftForm } from '../shift-form/shift-form';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-shift-page',
  imports: [ShiftForm, ShiftList, ButtonModule, DialogModule],
  templateUrl: './shift-page.html',
  styleUrl: './shift-page.scss',
})
export class ShiftPage {
  shiftList = viewChild(ShiftList);

  reloadShifts() {
    this.shiftList()?.loadShifts();
  }

  createShiftDialogVisible = false;

  openCreateShiftDialog(): void {
    this.createShiftDialogVisible = true;
  }

  closeCreateShiftDialog(): void {
    this.createShiftDialogVisible = false;
  }

  onShiftCreated(): void {
    this.createShiftDialogVisible = false;
    this.shiftList()?.loadShifts();
  }
}
