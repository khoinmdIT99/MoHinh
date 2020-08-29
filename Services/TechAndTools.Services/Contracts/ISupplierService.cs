namespace TechAndTools.Services.Contracts
{
    using Models;
    using TechAndTools.Data.Models.Enums;

    using System.Linq;
    using System.Threading.Tasks;

    public interface ISupplierService
    {
        Task<SupplierServiceModel> CreateAsync(SupplierServiceModel supplierServiceModel);

        Task<SupplierServiceModel> EditAsync(SupplierServiceModel supplierServiceModel);

        Task<bool> DeleteAsync(int id);

        SupplierServiceModel GetSupplierById(int id);

        IQueryable<SupplierServiceModel> GetAllSuppliers();

        decimal GetDeliveryPrice(int supplierId, ShippingTo shippingTo);
    }
}
