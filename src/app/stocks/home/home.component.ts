import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { Route, Router } from '@angular/router';
import { SearchService } from '../search-stocks/search.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  searchResults: any[] = [];
  constructor(private searchService: SearchService) { }

  ngOnInit() {
    this.searchService.searchStock('mi')
      .subscribe((data: any) => {
        this.searchResults = data['stocks'];
        console.log(this.searchResults)
      });
  }
}