using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository
{
    public class ProductsRepository : DataRepositoryBase<Products>, IProductsRepository
    {
        private readonly SelfServiceSupportPortalDbContext selfServiceSupportPortalcontext;
        public ProductsRepository(SelfServiceSupportPortalDbContext appDbContext) : base(appDbContext)
        {
            selfServiceSupportPortalcontext = appDbContext;
        }

        public async Task InsertRange(IList<Products> products)
        {
           await selfServiceSupportPortalcontext.AddRangeAsync(products);
            await selfServiceSupportPortalcontext.SaveChangesAsync();
        }

        public IQueryable<ProductVM> GetProducts()
        {
            return from p in selfServiceSupportPortalcontext.Products
                   join c in selfServiceSupportPortalcontext.Company on p.CompanyId equals c.Id into companyGroup
                   from company in companyGroup.DefaultIfEmpty()
                   orderby p.ID descending
                       select new ProductVM 
                       { 
                           ID = p.ID,
                           BrandModuleNo = p.BrandModuleNo,
                           StartedDate = p.StartedDate,
                           Location = p.Location,
                           Tag = p.Tag,
                           McSerialNo = p.McSerialNo,
                           Department = p.Department,
                           Address = p.Address,
                           CompanyId = p.CompanyId,
                           CompanyName = company.Name,
                           CreatedDate = p.CreatedDate,
                           CreatedBy = p.CreatedBy,
                       };
        }

        public ProductDetailsVM GetProductDetailsByProductId(string Tag)
        {
            var data = (from p in selfServiceSupportPortalcontext.Products
                        where p.Tag == Tag
                        select new ProductDetailsVM
                        {
                            ProductId = p.ID,
                            BrandModuleNo = p.BrandModuleNo,
                            Tag = p.Tag,
                            Location = p.Location,
                            Address = p.Address,
                            McSerialNo = p.McSerialNo,
                            Department = p.Department,
                            CreatedDate = p.CreatedDate
                        }).FirstOrDefault();
            return data;
        }
    }
}
