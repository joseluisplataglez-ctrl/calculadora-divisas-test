using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Domain.Common
{
    public enum EntityStatusFilter
    {
        OnlyActive,
        OnlyInactive,
        All
    }
    public abstract class BaseDomainModel
    {
        public int       Id         { get; set; }
        public DateTime  CreatedAt  { get; set; } = DateTime.UtcNow;
        public string    CreatedBy  { get; set; } = string.Empty;
        public DateTime? UpdatedAt  { get; set; }
        public string?   UpdatedBy  { get; set; }
        public bool      IsActive   { get; set; } = true;

        public void MarkAsCreated(string userId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userId;
        }

        public void MarkAsUpdated(string userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
