namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public class ShopEmployeeDTO : EmployeeDTO
	{
        public virtual ShopDTO Shop { get; set; }
    }
}
