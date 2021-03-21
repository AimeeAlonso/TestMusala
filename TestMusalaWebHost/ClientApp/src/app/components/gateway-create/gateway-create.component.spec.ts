import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GatewayCreateComponent } from './gateway-create.component';

describe('GatewayCreateComponent', () => {
  let component: GatewayCreateComponent;
  let fixture: ComponentFixture<GatewayCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GatewayCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GatewayCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
