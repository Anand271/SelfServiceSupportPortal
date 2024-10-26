using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface
{
    public interface IComplainsRepository : IDataRepositoryBase<Complains>
    {
        IQueryable<ComplainsVM> GetComplains(int? Id);
    }
}
