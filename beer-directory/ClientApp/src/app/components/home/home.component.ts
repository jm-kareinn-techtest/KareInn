import { Component } from '@angular/core';
import { Style } from 'src/app/data/style';
import { BeerService } from 'src/app/services/beer.service';
import { StyleService } from 'src/app/services/style.service';
import { Beer } from '../../data/beer';
import { SearchData } from '../search/search.component';

/*
 * Notes: This uses a rather naive method of state control. Ideally, this would be handled in a better way, such 
 * as using ngRx stores and events and the reducer pattern. I've opted to take the naive approach partly for time
 * reasons, and partly because I'm somewhat rusty with my angular and don't fully remember the implementation details
 * of ngRx. 
 */

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  styles: Style[] = []
  beers: Beer[] = []
  resultsStart: number = 0
  resultsSize: number = 10
  search: SearchData =  {searchTerms: '', style: -1}

  constructor(private styleService: StyleService, private beerService: BeerService) {}

  ngOnInit(): void {
    this.styleService.getStyles().subscribe(r => this.styles = r)
    this.performSearch()
  }

  lastPage(): void {
    if(this.resultsStart < this.resultsSize) {
      this.resultsStart = 0
    }
    else {
      this.resultsStart = this.resultsStart - this.resultsSize
    }

    this.performSearch(this.search.searchTerms, this.search.style, this.resultsStart)
  }

  nextPage(): void {
    if(this.beers.length >= this.resultsSize) {
      this.resultsStart = this.resultsStart + this.resultsSize
    }

    this.performSearch(this.search.searchTerms, this.search.style, this.resultsStart)
  }

  performSearch(terms: string = '', style: number = -1, start: number = 0): void {
    this.search = { searchTerms: terms, style: style }
    this.beerService.getBeers(terms, style, start, this.resultsSize).subscribe(r => this.beers = r)
  }
}
