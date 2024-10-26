
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AadharVerify.Data.Core.DataRepository.RepositoryWrapper
{
    public interface IDataRepositoryWrapper
    {
        public ILocationRepository LocationRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IProductComplaintRepository ProductComplaintRepository { get; }
        public IRegisterComplainRepository RegisterComplainRepository { get; }
        public IComplainsRepository ComplainsRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
      
    }
}
