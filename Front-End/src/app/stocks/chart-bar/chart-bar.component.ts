import { Component, Input, ViewChild } from "@angular/core";
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexTitleSubtitle,
  ApexXAxis,
  ApexFill,
  ApexGrid,
  ApexTooltip
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  title: ApexTitleSubtitle;
  grid: ApexGrid
  tooltip: ApexTooltip
};

@Component({
  selector: 'app-chart-bar',
  templateUrl: './chart-bar.component.html',
  styleUrls: ['./chart-bar.component.css']
})
export class ChartBarComponent {

  @Input() data = [];
  @Input() categories = [];
  

  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  constructor() {
    console.log(this.data);
    console.log(this.categories);
    this.chartOptions = {
      series: [
        {
          name: "Inflation",
          data: this.data
        }
      ],
      chart: {
        height: 350,
        type: "bar"
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: "top" // top, center, bottom
          }
        }
      },
      dataLabels: {
        enabled: true,
        formatter: function(val) {
          return val + "%";
        },
        offsetY: -20,
        style: {
          fontSize: "12px",
          colors: ["white"]
        }
      },

      xaxis: {
        categories: this.categories,
        position: "top",
        labels: {
          offsetY: -18
        },
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        },
        crosshairs: {
          fill: {
            type: "gradient",
            color: "#F5C249"
          }
        },
        tooltip: {
          enabled: false,
          offsetY: -35,
          style: {
            fontSize: "12px",
          }
        }
      },
      fill: {
       colors: ["#F5C249"]
      },
      yaxis: {
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        },
        labels: {
          show: false,
          formatter: function(val) {
            return val + "%";
          }
        }
      },
      grid: {
        borderColor: "#313542",
        row: {
          opacity: 0.01
        },
        column: {
          // opacity:0.1
        },
        xaxis: {
          lines: {
            show: false
          }
        },
        yaxis: {
          lines: {
            show: true
          }
        }
      }
    };
  }

  
  ngOnInit(): void {
    // Fetch data here (assuming it's fetched asynchronously)
    this.fetchData();
  }

  fetchData() {
    setTimeout(() => {
      // Update only the series and categories
      this.chartOptions = {
        ...this.chartOptions,
        series: [{ name: 'Dividendo', data: this.data }],
        xaxis: {
          ...this.chartOptions.xaxis,
          categories: this.categories,
          labels: {
            ...this.chartOptions.xaxis!.labels,
            show: false // Hide x-axis labels
          }
        },
        tooltip: {
          ...this.chartOptions.tooltip,
          theme: 'dark' // Change the tooltip theme to dark
        }
      };
    }, 500);
  }
  
  
}
