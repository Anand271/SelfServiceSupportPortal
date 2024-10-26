using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Model;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository
{
    public class CompanyRepository : DataRepositoryBase<Company>, ICompanyRepository
    {
        private readonly SelfServiceSupportPortalDbContext selfServiceSupportPortal;
        public CompanyRepository(SelfServiceSupportPortalDbContext appDbContext) : base(appDbContext)
        {
            selfServiceSupportPortal = appDbContext;
        }
    }
}
