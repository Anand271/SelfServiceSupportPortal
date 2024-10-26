using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface
{
    public interface IRegisterComplainRepository : IDataRepositoryBase<RegisterComplain>
    {
        IQueryable<RegisterComplainVM> GetRegisterComplains(string Status);
        IQueryable<ProductDetailsVM> GetRegisterComplainWithProductBranch();
    }
}
