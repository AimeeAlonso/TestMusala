import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GatewayDetail, GenericResult, Result } from '../../models/models';

@Component({
  selector: 'app-gateway-details',
  templateUrl: './gateway-details.component.html',
  styleUrls: ['./gateway-details.component.css']
})
export class GatewayDetailsComponent implements OnInit {
  gatewayId: number;
  gateway: GatewayDetail;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseURL: string,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    this.gatewayId=+this.activatedRoute.snapshot.paramMap.get("id");
  }

  ngOnInit() {
    this.getGateway();
  }
  getGateway() {
    var url = this.baseURL + 'api/gateway/' + this.gatewayId;

    this.http.get<GenericResult<GatewayDetail>>(url)
      .subscribe(result => {
        if (result.messages) {
          result.messages.forEach(item => {
            console.error(item);
            alert(item);
          });  
        }
        else this.gateway = result.content;
        
      },
        error => { console.log(error) });
  }

  addDevice() {
    if (this.gateway.devices.length==10) {
      alert("The gateway has reached the devices limit. Please delete a device in order to add a new one");
    }
    else this.router.navigate(['/gateway', this.gateway.id, 'add-device']);
  }
  deleteRow(id: number) {
    var url = this.baseURL + 'api/gateway/DeleteDevice/' + id;

    this.http.delete<Result>(url)
      .subscribe(result => {
        if (result.messages) {
          result.messages.forEach(item => {
            console.error(item);
            alert(item);
          });
        }
        this.gateway.devices = this.gateway.devices.filter(x => x.id != id);
      },
        error => { console.log(error) });
  }

  return() {
    this.router.navigate(['']);
  }
}
