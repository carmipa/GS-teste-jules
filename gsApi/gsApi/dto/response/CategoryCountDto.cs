// File: SeuProjetoNET/DTOs/Response/CategoryCountDto.cs
namespace SeuProjetoNET.DTOs.Response
{
    public class CategoryCountDto
    {
        public string CategoryTitle { get; set; }
        public long Count { get; set; }

        public CategoryCountDto(string categoryTitle, long count)
        {
            CategoryTitle = categoryTitle;
            Count = count;
        }
    }
}