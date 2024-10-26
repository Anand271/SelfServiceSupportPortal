using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository
{
    public class ComplainsRepository : DataRepositoryBase<Complains>, IComplainsRepository
    {
        private readonly SelfServiceSupportPortalDbContext _selfServiceSupportPortalcontext;
        public ComplainsRepository(SelfServiceSupportPortalDbContext appDbContext) : base(appDbContext)
        {
            _selfServiceSupportPortalcontext = appDbContext;
        }


        public IQueryable<ComplainsVM> GetComplains(int? Id)
        {
            if(Id != null && Id > 0)
            {
                return from compl in _selfServiceSupportPortalcontext.Complains.Where(x => x.RegisterComplainId == Id)
                       join pc in _selfServiceSupportPortalcontext.ProductComplaint on compl.ComplainId equals pc.ID into productGroup
                       from productComp in productGroup.DefaultIfEmpty()
                       join rc in _selfServiceSupportPortalcontext.RegisterComplain on compl.RegisterComplainId equals rc.ID into registeredCompGroup
                       from registeredComp in registeredCompGroup.DefaultIfEmpty()
                       select new ComplainsVM
                       {
                           ID = compl.ID,
                           ComplainId = compl.ComplainId,
                           RegisterComplainId = compl.RegisterComplainId,
                           Name = productComp.Name,
                           RegisterdDate = registeredComp.RegisterdDate,
                           Status = registeredComp.Status,
                           Comment = registeredComp.Comment,
                       };
            }
            else
            {
                return from compl in _selfServiceSupportPortalcontext.Complains
                       join pc in _selfServiceSupportPortalcontext.ProductComplaint on compl.ComplainId equals pc.ID into productGroup
                       from productComp in productGroup.DefaultIfEmpty()
                       join rc in _selfServiceSupportPortalcontext.RegisterComplain on compl.RegisterComplainId equals rc.ID into registeredCompGroup
                       from registeredComp in registeredCompGroup.DefaultIfEmpty()
                       select new ComplainsVM
                           { 
                             ID = compl.ID,
                             ComplainId = compl.ComplainId,
                             RegisterComplainId = compl.RegisterComplainId,
                             Name = productComp.Name,
                             RegisterdDate = registeredComp.RegisterdDate,
                             Status = registeredComp.Status,
                             Comment = registeredComp.Comment
                           };
            }
        }
    }
}
