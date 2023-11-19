import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexFill,
  ApexMarkers,
  ApexYAxis,
  ApexXAxis,
  ApexTooltip,
  ApexGrid
} from "ng-apexcharts";
import { ApexResponsive } from "ngx-apexcharts";

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnChanges  {
  @Input() item = [];
  @Input() symbol2 = "";
  
  public series!: ApexAxisChartSeries;
  public chart!: ApexChart;
  public dataLabels!: ApexDataLabels;
  public markers!: ApexMarkers;
  public title!: ApexTitleSubtitle;
  public fill!: ApexFill;
  public yaxis!: ApexYAxis;
  public xaxis!: ApexXAxis;
  public tooltip!: ApexTooltip;
  public grid!: ApexGrid;
  public responsive!: ApexResponsive;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes["item"] && changes["item"].currentValue) {
      this.initChartData();
    }
  }

  constructor() {
    this.initChartData();
  }

  public initChartData(): void {
    this.series = [
      {
        name: this.symbol2,
        data: this.item,
        color: "#F5C249"
      },
    ];
    this.chart = {
      type: "area", 
      stacked: false,
      height: "350%",
      width: "200%",
      zoom: {
        type: "x",
        enabled: false,
        autoScaleYaxis: true
      },
      toolbar: {
        show: false
      },
      background: "#21242D",
      foreColor: "#FFF",
    };
    this.grid = {
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
    };
    this.dataLabels = {
      enabled: false,
    };
    this.markers = {
      size: 0
    };
    this.fill = {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        inverseColors: false,
        opacityFrom: 0.5,
        opacityTo: 0,
        stops: [0, 90, 100]
      }
    };
    this.yaxis = {
      opposite: true,

      labels: {
        formatter: function (val) {
          return "$" + val.toFixed(2); // Format the label to display the price with two decimal places
        },
        style: {
          colors: ["#fff"]
        }
      }
    };
    this.xaxis = {
      type: "datetime",
    };
    this.tooltip = {
      y: {
        formatter: function (val) {
          return "$" + val.toFixed(2); // Format the tooltip for y-axis
        }
      },
      theme: "dark"
    };
  }
}
