import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ApiResponse } from './models/api-response';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) { }

  status: string;

  ngOnInit() {
      this._httpService.get('/api/cpu/status').subscribe(response => {
      this.status = (response.json() as ApiResponse<string>).data;
    });
  }
}
