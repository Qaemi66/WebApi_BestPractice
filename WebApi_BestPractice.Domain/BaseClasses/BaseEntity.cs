using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_BestPractice.Domain.BaseClasses
{

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
    public class BaseEntity : BaseEntity<int>
    {
    }
}
