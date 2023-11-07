import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from './stock.service';

@Component({
  selector: 'app-stocks-page',
  templateUrl: './stocks-page.component.html',
  styleUrls: ['./stocks-page.component.css']
})

export class StocksPageComponent implements OnInit {
  symbol?: string; // Define a class property to store the symbol

  constructor(private route: ActivatedRoute, private stock: StockService) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.symbol = params.get("symbol")!; // Store the symbol in the class property
      var stock = this.getStockDetails(); // Call the function to fetch stock details
      console.log(stock);
      
    });
  }

  getStockDetails() {
    if (this.symbol) {
      this.stock.GetStock(this.symbol).subscribe();
    }
  }
}
