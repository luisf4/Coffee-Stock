import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from '../stocks-page/stock.service';
import { StocksPortfolioService } from './portfolio-item.service';

@Component({
  selector: 'app-portfolio-item',
  templateUrl: './portfolio-item.component.html',
  styleUrls: ['./portfolio-item.component.css']
})
export class PortfolioItemComponent implements OnInit {
  portfolio_id: any;
  portfolio_name: any;
  stocks_list: any;


  constructor(private route: ActivatedRoute, private stocksPortfolio: StocksPortfolioService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.portfolio_id = params.get("portfolio_id")!; // Store the symbol in the class property
      this.portfolio_name = params.get("name")!; // Store the symbol in the class property
    });

    const token = localStorage.getItem("jwtToken");
    this.stocksPortfolio.getStocksPortfolios(token!, this.portfolio_id).subscribe(data => {
      this.stocks_list = data
      console.log(this.stocks_list)
    }
    )
  }
}
