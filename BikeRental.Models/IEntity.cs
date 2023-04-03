using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
