import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../core/services/dashboard/dashboard.service';
import { DashboardSummary } from '../../models/dashboard.models';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-dashboard',
  imports: [CardModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {

  summary: DashboardSummary | null = null;

  loading = false;

  errorMessage: string | null = null;

  constructor(
    private readonly dashboardService: DashboardService
  ) {}

  ngOnInit(): void {
    this.loadSummary();
  }

  loadSummary(): void {
    this.loading = true;
    this.errorMessage = null;

    this.dashboardService.getSummary().subscribe({
      next: (summary) => {
        this.summary = summary;
        this.loading = false;
      },

      error: (error) => {
        console.error(
          'Failed to load dashboard summary',
          error
        );

        this.errorMessage =
          'Unable to load dashboard data.';

        this.loading = false;
      }
    });
  }
}
