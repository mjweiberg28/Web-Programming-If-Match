using Gargoyles.Entities;
using Gargoyles.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gargoyles.Services
{
    public class GargoylesDatabase
    {

        private Dictionary<string, GargoyleModel> gargoyles = new Dictionary<string, GargoyleModel>();

        public GargoylesDatabase()
        {
            gargoyles.Add("Bronx", new GargoyleModel() { Name = "Bronx", Color = "Navy", Size = "Large", Gender = "Male", Updated = DateTime.UtcNow});
            gargoyles.Add("Queens", new GargoyleModel() { Name = "Queens", Color = "Scarlet", Size = "Small", Gender = "Female" , Updated = DateTime.UtcNow});
        }

        public IEnumerable<GargoyleEntity> GetAll()
        {
            var results = this.gargoyles.Select(element => element.Value.toEntity());
            return results;
        }

        public GargoyleModel Get(string index)
        {
            return this.gargoyles[index];
        }

        public void AddOrReplace(GargoyleModel model)
        {
            model.Updated = DateTime.UtcNow;
            this.gargoyles[model.Name] = model;
        }

        public GargoyleModel Update(string index, GargoyleModel model)
        {
            var modelToUpdate = this.gargoyles[index];

            // true if null
            // true if ""
            // true if "    "
            if (!string.IsNullOrWhiteSpace(model.Color))
            {
                modelToUpdate.Updated = DateTime.UtcNow;
                modelToUpdate.Color = model.Color;
                modelToUpdate.Size = model.Size;
                modelToUpdate.Gender = model.Gender;
            }

            return modelToUpdate;
        }

        public bool Contains(string index)
        {
            return gargoyles.ContainsKey(index);
        }
    }
}