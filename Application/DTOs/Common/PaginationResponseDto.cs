namespace Application.DTOs.Common
{
    public class PaginationResponseDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }  // Nombre más descriptivo
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationResponseDto(
            IEnumerable<T> data, 
            int totalCount,  // Asegúrate de recibir el conteo total
            int pageNumber, 
            int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

}