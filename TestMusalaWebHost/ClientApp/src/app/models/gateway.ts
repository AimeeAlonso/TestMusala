export class Gateway {
  id: number;
  serialNumber: string;
  name: string;
  ipV4Address: string;
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
