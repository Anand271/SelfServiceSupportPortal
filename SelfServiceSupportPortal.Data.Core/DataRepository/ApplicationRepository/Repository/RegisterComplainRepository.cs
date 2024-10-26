using AadharVerify.Data.Core.DataRepository.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Interface;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfServiceSupportPortal.Data.Core.DataRepository.ApplicationRepository.Repository
{
    public class RegisterComplainRepository : DataRepositoryBase<RegisterComplain>, IRegisterComplainRepository
    {
        private readonly SelfServiceSupportPortalDbContext _selfServiceSupportPortal;
        public RegisterComplainRepository(SelfServiceSupportPortalDbContext appDbContext) : base(appDbContext)
        {
            _selfServiceSupportPortal = appDbContext;
        }

        public IQueryable<RegisterComplainVM> GetRegisterComplains(string Status)
        {
            var data = from regCom in _selfServiceSupportPortal.RegisterComplain.Where(x=>x.Status == Status)
                       join product in _selfServiceSupportPortal.Products on regCom.ProductId equals product.ID
                       orderby regCom.RegisterdDate descending
                       select new RegisterComplainVM
                       {
                           ID = regCom.ID,
                           ProductId = regCom.ProductId,
                           Remark = regCom.Remark,
                           RegisterdDate = regCom.RegisterdDate,
                           Products = product,
                           Status = regCom.Status
                       };

            //var data = from regCom in _selfServiceSupportPortal.RegisterComplain
            //           join product in _selfServiceSupportPortal.Products on regCom.ProductId equals product.ID
            //           join complain in _selfServiceSupportPortal.Complains.Include(x => x.ComplainId) on regCom.ID equals complain.RegisterComplainId
            //           join productComplain in _selfServiceSupportPortal.ProductComplaint on complain.ComplainId equals productComplain.ID
            //           select new RegisterComplainVM
            //           {
            //               ID = regCom.ID,
            //               ProductId = regCom.ProductId,
            //               Remark = regCom.Remark,
            //               RegisterdDate = regCom.RegisterdDate,
            //               Products = product,
            //               ProductComplaint = productComplain,
            //               ProductComplaints = complain.ProductComplaints
            //           };
            return data;
        }


        public IQueryable<ProductDetailsVM> GetRegisterComplainWithProductBranch()
        {
            return from regCom in _selfServiceSupportPortal.RegisterComplain
                   join prod in _selfServiceSupportPortal.Products on regCom.ProductId equals prod.ID
                   select new ProductDetailsVM
                   {
                       Id = regCom.ProductId,
                       BrandModuleNo = prod.BrandModuleNo,
                       RegisterdDate = regCom.RegisterdDate,
                   };
        }
    }
}
