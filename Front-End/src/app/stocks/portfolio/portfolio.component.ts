import { Block } from '@angular/compiler';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';
import { PortfolioService } from './portfolio.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css']
})
export class PortfolioComponent implements OnInit {
  nomePortfolio = new FormControl('');
  portfoliosResult: any;
  editId: any;

  constructor(private portfolioService: PortfolioService,private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.searchPortfolio()
  }

  searchPortfolio(): void {
    try {
      const token = localStorage.getItem("jwtToken");
      if (token) {
        this.portfolioService.getPortfolios(token).subscribe(
          (response: any) => {
            try {
              const data = JSON.parse(JSON.stringify(response));
              this.portfoliosResult = data;
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
createPortfolio(): void {
  try {
    const token = localStorage.getItem("jwtToken");
    if (token) {
      this.portfolioService.CreatePortfolios(token, this.nomePortfolio.value!).subscribe(
        () => {
          // After creating a portfolio, refresh the list
          this.searchPortfolio();
          this.cdr.detectChanges();
        },
        (error: any) => {
          console.error('Error creating portfolio:', error);
        }
      );
      this.nomePortfolio.reset();
    }
  } catch (e) {
    console.error(e);
  }
}

deletePortfolio(id: string): void {
  try {
    const token = localStorage.getItem("jwtToken");
    if (token) {
      this.portfolioService.DeletePortfolio(token, id).subscribe(
        () => {
          // After creating a portfolio, refresh the list
          this.searchPortfolio();
          this.cdr.detectChanges();
        },
        (error: any) => {
          console.error('Error creating portfolio:', error);
        }
      );
    }
  } catch (e) {
    console.error(e);
  }
}
updatePortfolio(){
  try {
    const token = localStorage.getItem("jwtToken");
    if (token) {
      this.portfolioService.UpdatePortfolio(token, this.editId, this.nomePortfolio.value!).subscribe(
        () => {
          // After creating a portfolio, refresh the list
          this.searchPortfolio();
          this.cdr.detectChanges();
        },
        (error: any) => {
          console.error('Error creating portfolio:', error);
        }
      );
    }
  } catch (e) {
    console.error(e);
  }
}
  
  clickEdit(id: string) {
    this.editId = id;
    console.log(id)
  }



}