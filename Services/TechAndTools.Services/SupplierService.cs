namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SupplierService : ISupplierService
    {
        private readonly TechAndToolsDbContext context;

        public SupplierService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public async Task<SupplierServiceModel> CreateAsync(SupplierServiceModel supplierServiceModel)
        {
            Supplier supplier = supplierServiceModel.To<Supplier>();

            await this.context.Suppliers.AddAsync(supplier);
            await this.context.SaveChangesAsync();

            return supplierServiceModel;
        }

        public async Task<SupplierServiceModel> EditAsync(SupplierServiceModel supplierServiceModel)
        {
            Supplier supplierFromDb = this.context.Suppliers
                .Find(supplierServiceModel.Id);

            if (supplierFromDb == null)
            {
                throw new ArgumentNullException(nameof(supplierFromDb));
            }

            supplierFromDb.Name = supplierServiceModel.Name;
            supplierFromDb.DeliveryTimeInDays = supplierServiceModel.DeliveryTimeInDays;
            supplierFromDb.PriceToAddress = supplierServiceModel.PriceToAddress;
            supplierFromDb.PriceToOffice = supplierServiceModel.PriceToOffice;

            this.context.Suppliers.Update(supplierFromDb);
            await this.context.SaveChangesAsync();

            return supplierFromDb.To<SupplierServiceModel>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Supplier supplierFromDb = this.context.Suppliers
                .Find(id);

            if (supplierFromDb == null)
            {
                throw new ArgumentNullException(nameof(supplierFromDb));
            }

            this.context.Suppliers.Remove(supplierFromDb);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public SupplierServiceModel GetSupplierById(int id)
        {
            Supplier supplierFromDb =  this.context.Suppliers
                .Find(id);

            if (supplierFromDb == null)
            {
                throw new ArgumentNullException(nameof(supplierFromDb));
            }

            return supplierFromDb.To<SupplierServiceModel>();
        }

        public IQueryable<SupplierServiceModel> GetAllSuppliers()
        {
            return this.context.Suppliers.To<SupplierServiceModel>();
        }

        public decimal GetDeliveryPrice(int supplierId, ShippingTo shippingTo)
        {
            Supplier supplierFromDb = this.context.Suppliers
                .Find(supplierId);

            if (supplierFromDb == null)
            {
                throw new ArgumentNullException(nameof(supplierFromDb));
            }

            return shippingTo == ShippingTo.Office ? supplierFromDb.PriceToOffice : supplierFromDb.PriceToAddress;
        }
    }
}