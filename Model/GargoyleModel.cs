using Gargoyles.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gargoyles.Model
{
    public class GargoyleModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(3)]
        public string Color { get; set; }

        [MinLength(3)]
        public string Size { get; set; }

        [MinLength(3)]
        public string Gender { get; set; }

        public DateTime Updated { get; set; }


        public string ETag()
        {
            return this.Updated.ToString();
        }

        public GargoyleEntity toEntity()
        {
            return new GargoyleEntity()
            {
                Name = this.Name,
                Color = this.Color,
                Size = this.Size,
                Gender = this.Gender,
                Updated = this.Updated
            };
        }
    }
}