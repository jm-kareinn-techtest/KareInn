import { Component, Input, OnInit } from '@angular/core';
import { Beer } from 'src/app/data/beer';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {

  @Input() beers: Beer[] = []

  constructor() { }

  ngOnInit(): void {
  }

}
