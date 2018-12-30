using System;
using System.Collections.Generic;
using System.Linq;
using Product.Data.Entities;
using Product.Data.Repositories;
using Product.DomainObjects.Models;

namespace Product.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private const string Box1 = "Box 1";
        private const string Box2 = "Box 2";

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;

        }

        public MaterialModel GetById(Int32 id)
        {
            return Transfer(_materialRepository.GetById(id));
        }

        public IList<MaterialModel> GetAll()
        {
            return _materialRepository.GetAll().Select(Transfer).ToList();
        }

        public void Merge(Int32 materialIdToKeep, Int32 materialIdToDelete)
        {
            var entityToUpdate = _materialRepository.GetById(materialIdToKeep);
            foreach (var entity in entityToUpdate.ProductMaterials)
            {
                entity.MaterialId = materialIdToKeep;
            }

            _materialRepository.UpdateAssociated(materialIdToDelete, entityToUpdate.ProductMaterials);
            _materialRepository.Delete(materialIdToDelete);
        }

        public Material GetMaterialToDelete(Int32 materialId)
        {
            var x = _materialRepository.GetById(materialId);
            if (x.Name.Equals(Box2))
            {
                return x;
            }
            return null;
        }

        public Material GetMaterialToKeep(Int32 materialId)
        {
            return _materialRepository.GetAll().Where(x => x.Name.Equals(Box1)).SingleOrDefault();

        }

        private MaterialModel Transfer(Data.Entities.Material entity)
        {
            return new MaterialModel
            {
                MaterialId = entity.MaterialId,
                Name = entity.Name,
                Cost = entity.Cost
            };
        }       
    }
}