import { Component, OnInit } from '@angular/core';
import { FavoritesServices } from './favorites.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { StockService } from '../stocks-page/stock.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {
  favorites: string[];
  userName: string;
  listStocks: any[] = [];

  constructor(private favServices: FavoritesServices, private auth: AuthService, private stocks: StockService) { }

  ngOnInit(): void {
    const token = localStorage.getItem("jwtToken");
    this.auth.getUsername(token!).subscribe(
      (response) => {
        this.userName = response;
        console.log(response); // Moved console.log inside the callback
        this.loadFavorites();
      }
    );
  }
  
  loadFavorites() {
    this.favServices.getFavorites(this.userName).subscribe(
      (res) => {
        this.favorites = res;
        for (let stock of this.favorites) {
          this.stocks.GetStock(stock).subscribe((res) => {
            this.listStocks.push(res);
            console.log(res);
          });
        }
      }
    );
  }

  deleteFavorite(symbol: string) {
    this.favServices.removeFavorites(this.userName, symbol).subscribe(() => {
      this.listStocks = this.listStocks.filter(stock => stock.symbol !== symbol);
      this.loadFavorites(); // Reload favorites after deletion
    });
  }
  
}
