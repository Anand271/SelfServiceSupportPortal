using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository
{
    public class LocationRepository : DataRepositoryBase<Location>, ILocationRepository
    {
        private readonly SelfServiceSupportPortalDbContext selfServiceSupportPortal;
        public LocationRepository(SelfServiceSupportPortalDbContext appDbContext) : base(appDbContext)
        {
            selfServiceSupportPortal = appDbContext;
        }
    }
}
