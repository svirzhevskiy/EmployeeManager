using Data.Entities;

namespace Logic.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public LegalFormDTO LegalForm { get; set; } = new LegalFormDTO();

        public CompanyDTO ToDTO(Company entity)
        {
            return new CompanyDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                LegalForm = new LegalFormDTO { Id = entity.LegalFormId }
            };
        }

        public Company ToEntity(CompanyDTO dto)
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