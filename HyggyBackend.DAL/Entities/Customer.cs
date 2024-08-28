using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class Customer : User
    {
        /*
         Клас IdentityUser є частиною бібліотеки ASP.NET Identity і надає базові властивості для користувача в системі аутентифікації та авторизації. 
        Коли ви успадковуєте від IdentityUser, ви отримуєте доступ до таких властивостей, які не зазначені у вашому класі Customer:

            UserName: string — Ім'я користувача.
            NormalizedUserName: string — Нормалізоване ім'я користувача (зазвичай зберігається в верхньому регістрі для порівняння).
            Email: string — Адреса електронної пошти користувача.
            NormalizedEmail: string — Нормалізована електронна пошта (зазвичай зберігається в верхньому регістрі для порівняння).
            EmailConfirmed: bool — Вказує, чи підтверджена адреса електронної пошти.
            PasswordHash: string — Хеш пароля користувача.
            SecurityStamp: string — Мітка безпеки, яка використовується для додаткового захисту (наприклад, для скидання пароля).
            ConcurrencyStamp: string — Мітка для підтримки одночасності (для запобігання конфліктів при одночасних оновленнях).
            PhoneNumber: string — Номер телефону користувача.
            PhoneNumberConfirmed: bool — Вказує, чи підтверджений номер телефону.
            TwoFactorEnabled: bool — Вказує, чи увімкнена двофакторна аутентифікація.
            LockoutEnd: DateTimeOffset? — Дата та час, коли користувач буде розблокований.
            LockoutEnabled: bool — Вказує, чи включено блокування користувача.
            AccessFailedCount: int — Кількість невдалих спроб входу.
        */

        //public long Id { get; set; }
        //public string Name { get; set; }
        //public string Surname { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (Customer)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
