
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AadharVerify.Data.Core.DataRepository.RepositoryWrapper
{
    public class DataRepositoryWrapper : IDataRepositoryWrapper
    {
        private readonly SelfServiceSupportPortalDbContext selfServiceSupportPortalDbContext;
        public DataRepositoryWrapper(SelfServiceSupportPortalDbContext appDbContext) 
        {
            selfServiceSupportPortalDbContext = appDbContext;
        }
        public ILocationRepository LocationRepository => new LocationRepository(selfServiceSupportPortalDbContext);
        public IProductComplaintRepository ProductComplaintRepository => new ProductComplaintRepository(selfServiceSupportPortalDbContext);
        public IProductsRepository ProductsRepository => new ProductsRepository(selfServiceSupportPortalDbContext);
        public IRegisterComplainRepository RegisterComplainRepository => new RegisterComplainRepository(selfServiceSupportPortalDbContext);
        public IComplainsRepository ComplainsRepository => new ComplainsRepository(selfServiceSupportPortalDbContext);
        public ICompanyRepository CompanyRepository => new CompanyRepository(selfServiceSupportPortalDbContext);
  
    }
}
