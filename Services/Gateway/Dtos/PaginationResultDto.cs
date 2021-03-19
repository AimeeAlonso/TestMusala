using System.Collections.Generic;

namespace Services.Gateway.Dtos
{
    public class PaginationResultDto<T>
    {
        public PaginationInfoDto Pagination { get; }
        public IEnumerable<T> Data { get; }

        public PaginationResultDto(PaginationInfoDto pageInfo, IEnumerable<T> pageData)
        {
            Pagination = pageInfo;
            Data = pageData;
        }
    }
}