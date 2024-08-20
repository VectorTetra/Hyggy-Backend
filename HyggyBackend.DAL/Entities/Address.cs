using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{

    /*
     Загальні рекомендації

    Нормалізація: Розгляньте нормалізацію даних, щоб уникнути дублювання і зменшити ризики помилок.
    Геокодування: Використовуйте геокодування для отримання координат, що може бути корисним для візуалізації та логістики.
    Автоматизація: Застосовуйте сервіси автозаповнення адрес для покращення користувацького досвіду.
     */
    public class Address
    {
        public long Id { get; set; }

        // Адреса доставки, розділена на компоненти
        public string Street { get; set; } // Назва вулиці
        public string HouseNumber { get; set; } // Номер будинку
        public string City { get; set; } // Місто
        public string State { get; set; } // Область або штат
        public string PostalCode { get; set; } // Поштовий індекс



        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


        // Замовлення
        public virtual ICollection<Order> Orders { get; set; }
        //Адреса магазину
        //public virtual Storage Storage{ get; set; }
    }

}
