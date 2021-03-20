import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Gateway, PaginationInfoDto, PaginationResultDto } from '../../models/gateway';

@Component({
  selector: 'app-gateways',
  templateUrl: './gateways.component.html',
  styleUrls: ['./gateways.component.css']
})
export class GatewaysComponent implements OnInit {
  pagination: PaginationInfoDto;
  prevPageDisabled: boolean;
  nextPageDisabled: boolean;
  totalPages: number;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseURL : string
  ) {
    this.pagination = new PaginationInfoDto(1,3,0);
  }

  gateways: Gateway[]
  prevPage() {
    this.getGateways(this.pagination.pageNumber - 1);
  }
  nextPage() {
    this.getGateways(this.pagination.pageNumber + 1);
  }
  getGateways(pageNumber: number) {
    var url = this.baseURL + 'api/gateway';
    var queryParams = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', this.pagination.pageSize.toString());
    this.http.get<PaginationResultDto>(url, { params: queryParams })
      .subscribe(result => {
        this.gateways = result.data;
        this.pagination.pageNumber = result.pagination.pageNumber;
        this.pagination.pageSize = result.pagination.pageSize;
        this.pagination.total = result.pagination.total;
        this.totalPages = this.pagination.pageSize == 0 ? 0 : Math.round((this.pagination.total / this.pagination.pageSize)) + 1;
        this.nextPageDisabled = this.pagination.pageNumber == this.totalPages;
        this.prevPageDisabled = this.pagination.pageNumber <= 1;
      },
        error => { console.log(error) });
  }
  ngOnInit() {
    this.getGateways(1);
  }

}
