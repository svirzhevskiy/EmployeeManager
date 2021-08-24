using System.Collections.Generic;
using Logic.DTOs;

namespace Web.Models.Company
{
    public class IndexModel
    {
        public IEnumerable<CompanyDTO> Companies { get; init; }
    }
}