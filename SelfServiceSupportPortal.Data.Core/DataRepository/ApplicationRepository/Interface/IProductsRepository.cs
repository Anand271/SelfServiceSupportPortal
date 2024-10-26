using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface
{
    public interface IProductsRepository : IDataRepositoryBase<Products>
    {
        Task InsertRange(IList<Products> products);
        IQueryable<ProductVM> GetProducts();
        public ProductDetailsVM GetProductDetailsByProductId(string Tag);
    }
}
