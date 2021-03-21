export class Gateway {
  id: number;
  serialNumber: string;
  name: string;
  ipV4Address: string;

}
export class GatewayDetail {
  id: number;
  serialNumber: string;
  name: string;
  ipV4Address: string;
  devices: Device[];
  
}

export class Device {
  id: number;
  uid: number;
  vendor: string;
  dateCreated: Date;
  status: boolean;
  gatewayId: number;

  constructor(gatewayId:number,
  //  uid: number,
   // vendor: string,
    status: boolean) {
    this.gatewayId = gatewayId;
   // this.uid = uid;
   // this.vendor = vendor;
    this.status = status;
  }
}

export class PaginationResultDto
{
  pagination: PaginationInfoDto
  data: Gateway[];
 
}

export class PaginationInfoDto {
  pageNumber: number;
  pageSize: number;
  total: number;
  constructor(pageNumber: number,
    pageSize: number,
    total: number) {
    this.pageSize = pageSize;
    this.pageNumber = pageNumber;
    this.total = total;
  }
}

export class Result
{
  messages: string[];
  error: boolean;
  success: boolean;
}

export class GenericResult<T> extends Result
{
  content: T;
}
