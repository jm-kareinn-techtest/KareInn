import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Style } from 'src/app/data/style';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  @Input() styles: Style[] = []
  @Output() doSearch = new EventEmitter<SearchData>()

  selectedStyle: number = -1

  searchTerms: string = ''

  constructor() { }

  ngOnInit(): void {
  }

  onSearch():void {
    this.doSearch.emit({ searchTerms: this.searchTerms, style: this.selectedStyle })
  }
}

export interface SearchData {
  searchTerms: string
  style: number
}