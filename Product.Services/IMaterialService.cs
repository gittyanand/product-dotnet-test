using System;
using System.Collections.Generic;
using Product.Data.Entities;
using Product.DomainObjects.Models;

namespace Product.Services
{
    public interface IMaterialService
    {
        MaterialModel GetById(Int32 id);
        IList<MaterialModel> GetAll();
        void Merge(Int32 materialIdToKeep, Int32 materialIdToDelete);
        Material GetMaterialToDelete(Int32 id);
        Material GetMaterialToKeep(Int32 id);
    }
}