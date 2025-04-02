namespace Application.DTOs.Common
{
    public class PagedResponse<Dto>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }  //  TotalCount
        public IEnumerable<Dto> Data { get; set; }

        public static PagedResponse<Dto> Ok(PaginationResponseDto<Dto> model)
        {
            return new PagedResponse<Dto>
            {
                Data = model.Data,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalItems = model.TotalCount,  
                TotalPages = (int)Math.Ceiling(model.TotalCount / (double)model.PageSize)
            };
        }
    }
}