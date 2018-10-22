using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.UsersFinderService
{
    public class SearchQueryDto
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? HeightFrom { get; set; }
        public int? HeightTo { get; set; }
        public int? WeightFrom { get; set; }
        public int? WeightTo { get; set; }
        public char? Gender { get; set; }
        public bool? Tatoos { get; set; }
        public int[] InterestIds { get; set; }
        public int? HairColor { get; set; } 
        public int? EyesColor { get; set; }
        public bool IsOnline { get; set; }
    }
}
