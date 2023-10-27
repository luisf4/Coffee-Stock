import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { StockService } from './search.service';
@Component({
  selector: 'app-search-stocks',
  templateUrl: './search-stocks.component.html',
  styleUrls: ['./search-stocks.component.css']
})
export class SearchStocksComponent {
  stock = new FormControl('');
  searchResults: any[] = [];

  constructor(private stockService: StockService) { }

  ngOnInit() {
    this.stock.valueChanges.pipe(
      debounceTime(300), // Wait for user to stop typing
      distinctUntilChanged(), // Ignore if the same value is typed
      switchMap(keyword => this.stockService.searchStock(keyword!))
    ).subscribe((data: any) => {
      this.searchResults = data['bestMatches'];
    });
  }
}
