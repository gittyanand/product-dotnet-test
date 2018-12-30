using System;
using System.Collections.Generic;
using System.Linq;
using Product.Data.Context;
using Product.Data.Entities;
using System.Data.Entity;


namespace Product.Data.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ProductDataContext _context;
        public MaterialRepository(ProductDataContext context)
        {
            _context = context;
        }

        public Material GetById(Int32 id)
        {
            return _context.Materials.Include(x => x.ProductMaterials).FirstOrDefault(x => x.MaterialId == id);
        }

        public IList<Material> GetAll()
        {
            return _context.Materials.ToList();
        }

        public void Update(Int32 id, String name, Decimal cost)
        {
            var entity = _context.Materials.Include(x => x.ProductMaterials).First(x => x.MaterialId == id);
            entity.Name = name;
            entity.Cost = cost;
        }

        public void Delete(Int32 id)
        {
            var entity = _context.Materials.First(x => x.MaterialId == id);
            _context.Materials.Remove(entity);
            _context.SaveChanges();

        }

        public void UpdateAssociated(Int32 id, ICollection<ProductMaterial> productMaterials)
        {            
            _context.ProductMaterials.Where(x => x.MaterialId == id).ToList().ForEach(x => x.MaterialId = productMaterials.First().MaterialId);
            _context.SaveChanges();

        }
    }
}