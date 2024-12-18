using AutoMapper;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.Controllers
{
    public class EmployeeQueryPL
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? RoleName { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
        public string? StringIds { get; set; }
    }

    public class EmployeeMapperConfig
    {
        public MapperConfiguration EmployeeConfig = new MapperConfiguration(mc =>
        {
            mc.CreateMap<EmployeeQueryPL, EmployeeQueryBLL>();
        });
    }
}
