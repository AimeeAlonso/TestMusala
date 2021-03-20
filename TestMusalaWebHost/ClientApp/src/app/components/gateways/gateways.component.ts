import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Gateway, PaginationResultDto } from '../../models/gateway';

@Component({
  selector: 'app-gateways',
  templateUrl: './gateways.component.html',
  styleUrls: ['./gateways.component.css']
})
export class GatewaysComponent implements OnInit {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseURL : string
  ) { }

  gateways: Gateway[]

  ngOnInit() {
    this.http.get<PaginationResultDto>(this.baseURL + 'api/gateway').subscribe(result => {
      this.gateways = result.data;
    },
      error => { console.log(error) });
  }

}
