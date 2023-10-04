import { Component } from '@angular/core';

@Component({
  selector: 'app-search-stocks',
  templateUrl: './search-stocks.component.html',
  styleUrls: ['./search-stocks.component.css']
})
export class SearchStocksComponent {
  searchTerm: string = '';

  search(): void {
    // Implement your search logic here
    console.log('Search term:', this.searchTerm);
    // You can perform search operations here, such as making API requests or filtering data.
  }
}
