﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities
{
	public class Shop
	{
        public long Id { get; set; }
        public string PhotoUrl { get; set; }
        public string? WorkHours { get; set; }
        public long StorageId { get; set; }
		[ForeignKey("StorageId")]
        public virtual Storage Storage { get; set; }
		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();


        public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var otherTour = (Shop)obj;
			return Id == otherTour.Id;
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
