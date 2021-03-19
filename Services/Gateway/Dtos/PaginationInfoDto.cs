namespace Services.Gateway.Dtos
{
    public class PaginationInfoDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        
        public PaginationInfoDto(int pageNumber = 1, int pageSize = 100)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 100;
            
            PageNumber = pageNumber;
            PageSize = pageSize;
            Total = 0;
        }
    }
}