import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from './stock.service';

@Component({
  selector: 'app-stocks-page',
  templateUrl: './stocks-page.component.html',
  styleUrls: ['./stocks-page.component.css']
})

export class StocksPageComponent implements OnInit {
  symbol?: string; // Define a class property to store the symbol
  stockDetails: any = {};  // Change 'stock' to 'stockDetails' for clarity
  stockPrice: any = [];
  stockSymbol: string = ''; // Define a class property to store the symbol

  
  constructor(private route: ActivatedRoute, private stockService: StockService) {}
  
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.symbol = params.get("symbol")!; // Store the symbol in the class property
      this.stockSymbol = this.symbol;
      this.getStockDetails(); // Call the function to fetch stock details
    });
  }

  getStockDetails() {
    if (this.symbol) {
      this.stockService.GetStock(this.symbol).subscribe((data) => {
        // Assuming 'data' contains a property that needs to be converted to uppercase
        // For example, if the property is 'name', you can convert it like this:
        if (data && data.name) {
          data.name = data.name.toUpperCase(); // Convert 'name' property to uppercase
        }
        this.stockDetails = data;
        this.stockPrice = this.stockDetails.historicalDataPrice.map((dataPoint: any) => {
          return {
            x: new Date(dataPoint.date * 1000).getTime(), // Convert Unix timestamp to milliseconds
            y: dataPoint.close
          };
        });
        console.log(this.stockPrice)
      });
    } else {
      this.stockDetails = null; // Reset stockDetails if symbol is not available
      console.log("Request Error");
    }
  }
}
