import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from '../stocks-page/stock.service';
import { StocksPortfolioService } from './portfolio-item.service';
import { PortfolioService } from '../portfolio/portfolio.service';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-portfolio-item',
  templateUrl: './portfolio-item.component.html',
  styleUrls: ['./portfolio-item.component.css']
})
export class PortfolioItemComponent implements OnInit {
  portfolio_id: any;
  portfolio_name: any;
  token: any;
  username: any;
  stocks_list: any;
  stocks_price: number[] = [];
  stocks_names: string[] = [];



  selectedPortfolioId: number | null = null;
  moneyValue: number | null = null;
  quantityValue: number | null = null;
  selectedDate: string | null = null;

  constructor(private cdr: ChangeDetectorRef, private route: ActivatedRoute, private stocksPortfolio: StocksPortfolioService, private portfolioService: PortfolioService, private auth: AuthService) { }

  ngOnInit() {
    const token = localStorage.getItem("jwtToken");

    this.auth.getUsername(token!).subscribe(res => this.username = res)
    this.route.paramMap.subscribe(params => {
      this.portfolio_id = params.get("portfolio_id")!;
      this.portfolio_name = params.get("name")!;
    });

    // Assuming stocks_names is an array of strings

    // Now within your code block:
    this.stocksPortfolio.getStocksPortfolios(token!, this.portfolio_id).subscribe((data: any[]) => {
      this.stocks_list = data;

      this.stocks_list.forEach((stockObj: { stock: string, price: number, qnt: number }) => {
        this.stocks_names.push(stockObj.stock);
        this.stocks_price.push(stockObj.price * stockObj.qnt);
      });
    });
  }


  onPortfolioSelected(event: any) {
    this.selectedPortfolioId = event.target.value; // Access the value of the selected option
    // Use 'selectedId' as needed
    console.log('Selected Portfolio ID:', this.selectedPortfolioId);
    // Update other variables or perform actions based on the selected ID
  }

  formatMoney() {
    // Format the money input (this logic can be customized as needed)
    if (this.moneyValue !== null) {
      const cleanedValue = this.moneyValue.toString().replace(/[^0-9,.]/g, ''); // Remove non-numeric characters
      const formattedValue = parseFloat(cleanedValue).toFixed(2); // Convert to number, format to 2 decimal places as a string
      this.moneyValue = parseFloat(formattedValue); // Convert the formatted string back to a number
      console.log(this.moneyValue);
    }

  }

  // You can handle the selected date change or perform actions based on this value
  onDateSelected() {
    console.log('Selected Date:', this.selectedDate);
    // You can use this.selectedDate in other parts of your component logic
  }

  // Function to check if all inputs are filled
  areInputsFilled(): boolean {
    return (
      this.quantityValue !== null &&
      this.moneyValue !== null &&
      this.selectedDate !== null
    );
  }

  // Function to handle submission when the button is clicked
  submitData(id: any, symbol: any) {
    if (this.areInputsFilled()) {
      this.UpdateStocks(id, symbol); // Call the method to add stock
    } else {
      console.log('Please fill in all the inputs.');
      // Optionally, you can show a message to the user to fill in all inputs
    }
  }

  // Function to add stock
  UpdateStocks(id: any, symbol: any) {
    // Add your logic here to add the stock
    // This method will be called when all inputs are filled
    this.token = localStorage.getItem('jwtToken');

    this.stocksPortfolio.UpdateStocksPortfolio(
      this.token,
      this.username,
      this.portfolio_id,
      symbol,
      this.quantityValue,
      this.moneyValue,
      this.selectedDate,
      id
    ).subscribe(
      (res) => {
        console.log('Stock updated:', res);
        // Manually update the respective item in the stocks_list array with the new values
        const updatedStock = this.stocks_list.find((item: any) => item.id === id);
        if (updatedStock) {
          // Update properties of the specific item
          updatedStock.quantityValue = this.quantityValue;
          updatedStock.moneyValue = this.moneyValue;
          updatedStock.selectedDate = this.selectedDate;
        }
        console.log('Stock added:', res);
        // Optionally, perform any actions after adding the stock
      },
      (error) => {
        console.error('Error adding stock:', error);
        // Optionally, handle the error
      }
    );
    this.fetchUpdatedStocksList();

  }

  deleteStock(id: any) {
    var token = localStorage.getItem('jwtToken');

    this.stocksPortfolio.DeleteStocksPortfolio(token!, id, this.portfolio_id).subscribe(res => {
      console.log(res);
      // Fetch updated list of stocks after deletion
      this.fetchUpdatedStocksList();
    });
  }

  fetchUpdatedStocksList() {
    const token = localStorage.getItem('jwtToken');
    this.stocksPortfolio.getStocksPortfolios(token!, this.portfolio_id).subscribe(data => {
      this.stocks_list = data;
      console.log(this.stocks_list);
    });
  }

}
