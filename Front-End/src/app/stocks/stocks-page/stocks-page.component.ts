import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from './stock.service';
import { PortfolioService } from '../portfolio/portfolio.service';
import { StocksPortfolioService } from '../portfolio-item/portfolio-item.service';

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
  portfoliosResult: any;
  selectedPortfolioId: number | null = null; // Initialize the variable to store the selected portfolio ID
  moneyValue: number | null = null;
  quantityValue: number | null = null;
  selectedDate: string | null = null;
  divdendsCategories: any = [];
  dividendsData: any = []

  constructor(private route: ActivatedRoute, private stockService: StockService, private portfolioService: PortfolioService, private stocksPortfolioServices: StocksPortfolioService) { }

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
        for (var dividend of this.stockDetails.dividendsData) { 
          const rateAsPercentage = (dividend.rate * 1).toFixed(2) + '%';
          this.dividendsData.push(rateAsPercentage);
        
          const paymentDate = new Date(dividend.paymentDate);
          const formattedDate = `${paymentDate.getDate()}-${paymentDate.getMonth() + 1}-${paymentDate.getFullYear()}`;
          this.divdendsCategories.push(formattedDate);
        }
        
        console.log(this.stockPrice)
        console.log(this.divdendsCategories)
        console.log(this.dividendsData)
      });
    } else {
      this.stockDetails = null; // Reset stockDetails if symbol is not available
      console.log("Request Error");
    }
  }


  getPortfolio(): void {
    try {
      const token = localStorage.getItem("jwtToken");
      if (token) {
        this.portfolioService.getPortfolios(token).subscribe(
          (response: any) => {
            try {
              this.portfoliosResult = response;
              console.log(this.portfoliosResult)
            } catch (error) {
              console.error('Error parsing response:', error);
            }
          },
          (error: any) => {
            console.error('Error fetching portfolios:', error);
          }
        );
      }
    } catch (e) {
      console.error(e);
    }
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
      this.selectedPortfolioId !== null &&
      this.quantityValue !== null &&
      this.moneyValue !== null &&
      this.selectedDate !== null
    );
  }

  // Function to handle submission when the button is clicked
  submitData() {
    if (this.areInputsFilled()) {
      this.addStock(); // Call the method to add stock
    } else {
      console.log('Please fill in all the inputs.');
      // Optionally, you can show a message to the user to fill in all inputs
    }
  }

  // Function to add stock
  addStock() {
    // Add your logic here to add the stock
    // This method will be called when all inputs are filled
    const token = localStorage.getItem('jwtToken');
    if (token && this.selectedPortfolioId && this.symbol && this.quantityValue !== null && this.moneyValue !== null && this.selectedDate) {
      this.stocksPortfolioServices.CreateStocksPortfolios(
        token,
        this.portfoliosResult[0].user_name,
        this.selectedPortfolioId,
        this.symbol,
        this.quantityValue,
        this.moneyValue,
        this.selectedDate
      ).subscribe(
        (res) => {
          console.log('Stock added:', res);
          // Optionally, perform any actions after adding the stock
        },
        (error) => {
          console.error('Error adding stock:', error);
          // Optionally, handle the error
        }
      );
    } else {
      console.log('Missing required inputs.');
      // Optionally, inform the user about missing inputs
    }
  }
}
