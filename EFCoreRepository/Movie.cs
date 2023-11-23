using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFCoreRepository
{
    public class Movie
    {
        public Guid Id { get; set; }
        public int Articul { get; set; }
        public string Name { get; set; }
        public bool IsAdult { get; set; }
    }
}
