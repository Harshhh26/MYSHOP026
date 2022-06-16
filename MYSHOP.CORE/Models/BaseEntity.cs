﻿using System;
using System.Collections.Generic;
using System.Linq;
using MYSHOP.CORE.Models;
using System.Threading.Tasks;

namespace MYSHOP.CORE.Models
{
    public abstract class BaseEntity
    {
        public  string Id{ get; set; }
        public  DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
           this.CreatedAt = DateTime.Now;
        }
    }
}
