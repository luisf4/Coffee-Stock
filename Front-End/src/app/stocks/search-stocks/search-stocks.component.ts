import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { SearchService } from './search.service';
@Component({
  selector: 'app-search-stocks',
  templateUrl: './search-stocks.component.html',
  styleUrls: ['./search-stocks.component.css']
})
export class SearchStocksComponent {
  stock = new FormControl('');
  searchResults: any[] = [];
  
  constructor(private searchService: SearchService) { }

  ngOnInit() {
    this.searchService.searchStock('aa')
    .subscribe((data: any) => {
      this.searchResults = data['stocks'];
      console.log(this.searchResults)
    });
    this.stock.valueChanges.pipe(
      debounceTime(300), // Wait for user to stop typing
      distinctUntilChanged(), // Ignore if the same value is typed
      switchMap(keyword => this.searchService.searchStock(keyword!))
    ).subscribe((data: any) => {
      this.searchResults = data['stocks'];
    });
  }
}
