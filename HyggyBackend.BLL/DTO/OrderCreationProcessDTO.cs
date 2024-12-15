using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class OrderCreationProcessDTO
    {
        // --- Для перевірки користувача
        public string? RegisteredCustomerId { get; set; }
        public CustomerDTO? GuestCustomer { get; set; }
        // --- Для перевірки адреси
        public AddressDTO? Address { get; set; }
        // --- Для створення об'єкта замовлення (Order)
        public OrderDTO? OrderData { get; set; }
        /*
        public long OrderStatusId { get; set; }
        public long ShopId { get; set; }
        public string CustomerId { get; set; }            --- Брати з RegisteredCustomerId або з GuestCustomer
        public long DeliveryAddressId { get; set; }       --- Брати з Address
        public string Phone { get; set; }
        public string Comment { get; set; }
        public DateTime OrderDate { get; set; }           --- DateTime.Now
        public int OrderDeliveryTypeId { get; set; }
         */

        // --- Для створення об'єкта замовлення (OrderItem)
        public IEnumerable<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
