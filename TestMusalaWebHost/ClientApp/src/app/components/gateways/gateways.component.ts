import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Gateway, PaginationInfoDto, PaginationResultDto, Result } from '../../models/models';

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
    private router: Router,
    @Inject('BASE_URL') private baseURL : string
  ) {
    this.pagination = new PaginationInfoDto(1,5,0);
  }

  gateways: Gateway[]
  prevPage() {
    this.getGateways(this.pagination.pageNumber - 1);
  }
  nextPage() {
    this.getGateways(this.pagination.pageNumber + 1);
  }
  view(id: number)
  {
    this.router.navigate(['/gateway', id]);
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
        this.totalPages = this.pagination.pageSize == 0 || this.pagination.total == 0 ? 1 : Math.floor(((this.pagination.total - 1) / this.pagination.pageSize)) + 1;
        this.nextPageDisabled = this.pagination.pageNumber == this.totalPages;
        this.prevPageDisabled = this.pagination.pageNumber <= 1;
      },
        error => { console.log(error) });
  }
  ngOnInit() {
    this.getGateways(1);
  }
  addGateway() {
    this.router.navigate(['/gateways/add']);
  }
  deleteRow(id: number) {
    var url = this.baseURL + 'api/gateway/' + id;

    this.http.delete<Result>(url)
      .subscribe(result => {
        if (result.messages) {
          result.messages.forEach(item => {
            console.error(item);
            alert(item);
          });
        }
        this.pagination.pageNumber = this.gateways.length == 1 ? Math.max(1, this.pagination.pageNumber - 1) : this.pagination.pageNumber
        this.getGateways(this.pagination.pageNumber);
      },
        error => { console.log(error) });
  }
}
