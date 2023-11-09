import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchStocksComponent } from './search-stocks.component';

describe('SearchStocksComponent', () => {
  let component: SearchStocksComponent;
  let fixture: ComponentFixture<SearchStocksComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchStocksComponent]
    });
    fixture = TestBed.createComponent(SearchStocksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
