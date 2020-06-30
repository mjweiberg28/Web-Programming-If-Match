using Gargoyles.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Gargoyles.Entities
{
    public class GargoyleEntity
    {
        public GargoyleEntity()
        {

        }

        public GargoyleEntity(GargoyleModel model)
        {
            this.Name = model.Name;
            this.Color = model.Color;
            this.Size = model.Size;
            this.Gender = model.Gender;
            this.Updated = model.Updated;
        }

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

        public GargoyleModel ToModel()
        {
            return new GargoyleModel()
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