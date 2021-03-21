import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { GatewaysComponent } from './components/gateways/gateways.component';
import { GatewayDetailsComponent } from './components/gateway-details/gateway-details.component';
import { GatewayCreateComponent } from './components/gateway-create/gateway-create.component';
import { GatewayDetail } from './models/models';
import { DeviceCreateComponent } from './components/device-create/device-create.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    GatewaysComponent,
    GatewayDetailsComponent,
    GatewayCreateComponent,
    DeviceCreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: GatewaysComponent, pathMatch: 'full' },
      { path: 'gateway/:id', component: GatewayDetailsComponent },
      { path: 'gateways/add', component: GatewayCreateComponent },
      { path: 'gateway/:id/add-device', component: DeviceCreateComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
