import { Component,inject} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Api } from './core/services/api';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
   private api = inject(Api);

  message = '';

  ngOnInit(): void {
    this.api.getTest().subscribe({
      next: (response) => {
        this.message = response.message;
      },
      error: (error) => {
        console.error('API error:', error);
        this.message = 'API connection failed';
      }
    });
  }
}
