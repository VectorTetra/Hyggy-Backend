namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public class ShopEmployeeDTO : EmployeeDTO
	{
        public long? ShopId { get; set; }
        public virtual ShopDTO Shop { get; set; }
    }
}
