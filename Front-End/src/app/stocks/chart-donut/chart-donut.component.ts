import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { ChartComponent } from "ng-apexcharts";

import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-chart-donut',
  templateUrl: './chart-donut.component.html',
  styleUrls: ['./chart-donut.component.css']
})
export class ChartDonutComponent implements OnInit {
  @Input() stocksPrice: number[] = [];
  @Input() stocks: string[] = [];

  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  constructor() {
    this.chartOptions = {
      series: [],
      chart: {
        type: "donut"
      },
      labels: [],

      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {},
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };
  }

  ngOnInit(): void {
    // Fetch data here (assuming it's fetched asynchronously)
    this.fetchData();
  }

  fetchData() {
    // Simulated asynchronous data fetching (replace this with your actual data fetching logic)
    setTimeout(() => {
      // Assign the fetched data to chartOptions after it's available
      this.chartOptions = {
        ...this.chartOptions,
        series: this.stocksPrice,
        labels: this.stocks
      };
    }, 500); // Simulating a delay of 1 second (adjust as per your actual data fetching)
  }
}
