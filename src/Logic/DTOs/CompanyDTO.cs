using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Logic.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Название может содержать от 3 до 50 символов")]
        public string Title { get; set; } = "";
        public EnumItemDTO LegalForm { get; set; } = new();

        public static CompanyDTO ToDTO(Company entity)
        {
            return new CompanyDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                LegalForm = new EnumItemDTO { Id = entity.LegalFormId }
            };
        }

        public static Company ToEntity(CompanyDTO dto)
        {
            return new Company
            {
                Id = dto.Id,
                Title = dto.Title,
                LegalFormId = dto.LegalForm.Id
            };
        }
    }
}