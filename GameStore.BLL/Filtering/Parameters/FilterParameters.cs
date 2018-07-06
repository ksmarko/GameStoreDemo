using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Filtering.Parameters
{
    public class FilterParameters
    {
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Platforms { get; set; }
        public IEnumerable<string> Publishers { get; set; }
        public bool Views { get; set; }
        public bool Comments { get; set; }
        public bool Price { get; set; }
        public bool Date { get; set; }
        public DateTime? PublicationDate { get; set; }
        public double PriceFrom { get; set; } = 0.0;
        public double PriceTo { get; set; } = Double.MaxValue;

        [MinLength(3)]
        public string Name { get; set; }
        public DirectionType Direction { get; set; }

        public FilterParameters()
        {
            Genres = new List<string>();
            Platforms = new List<string>();
            Publishers = new List<string>();
        }
    }
}
