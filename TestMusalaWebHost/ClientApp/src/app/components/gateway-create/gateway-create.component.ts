import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Gateway, Result } from '../../models/models';

@Component({
  selector: 'app-gateway-create',
  templateUrl: './gateway-create.component.html',
  styleUrls: ['./gateway-create.component.css']
})
export class GatewayCreateComponent implements OnInit {

  gateway: Gateway;
  gatewayId: number;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseURL: string,
    private router: Router) {

  }

  ngOnInit() {
    this.gateway = new Gateway();
  }
  save() {
    var url = this.baseURL + 'api/gateway';

    this.http.post<Result>(url, this.gateway)
      .subscribe(result => {
        if (result.messages) {
          result.messages.forEach(item => {
            console.error(item);
            alert(item);
          });
          
        }
        else this.return();

      },
        error => {
          console.error(error);
          alert(error.message);
        });
  }
  return() {
    this.router.navigate(['']);
  }
}
