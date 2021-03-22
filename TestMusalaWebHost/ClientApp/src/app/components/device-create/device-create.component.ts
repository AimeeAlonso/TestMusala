import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Device, Result } from '../../models/models';

@Component({
  selector: 'app-device-create',
  templateUrl: './device-create.component.html',
  styleUrls: ['./device-create.component.css']
})
export class DeviceCreateComponent implements OnInit {
  device: Device;
  gatewayId: number;
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseURL: string,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    
  }

  ngOnInit() {
    this.gatewayId = +this.activatedRoute.snapshot.paramMap.get("id");
    this.device = new Device(this.gatewayId, true);
  }
  save() {
    
    var url = this.baseURL + 'api/gateway/AddDevice';
    
    this.http.post<Result>(url, this.device )
      .subscribe(result => {
        if (result.messages) {
          result.messages.forEach(item => {
            console.error(item);
            alert(item);
          });
        }
        else this.router.navigate(['/gateway', this.gatewayId]);

      },
        error => {
          console.error(error);
          alert(error.message);
        });
  }
  return() {
    this.router.navigate(['/gateway', this.gatewayId]);
  }
}
