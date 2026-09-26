import { Component } from '@angular/core';
import { AssignmentList } from '../assignment-list/assignment-list';

@Component({
  selector: 'app-assignment-page',
  imports: [AssignmentList],
  templateUrl: './assignment-page.html',
  styleUrl: './assignment-page.scss',
})
export class AssignmentPage {

}
