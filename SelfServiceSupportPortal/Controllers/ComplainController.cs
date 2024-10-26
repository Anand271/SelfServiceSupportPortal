using AadharVerify.Data.Core.DataRepository.RepositoryWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SelfServiceSupportPortal.Controllers
{
    [Authorize]
    public class ComplainController :  Controller
    {
        private readonly IDataRepositoryWrapper _dataRepositoryWrapper;

        public ComplainController(IDataRepositoryWrapper dataRepositoryWrapper)
        {
            _dataRepositoryWrapper = dataRepositoryWrapper;
        }
        public IActionResult Index()
        {
            var data = _dataRepositoryWrapper.RegisterComplainRepository.GetRegisterComplains("Open").ToList();
            return View(data);
        }

        public IActionResult ViewComplain(int Id)
        {
            var data = _dataRepositoryWrapper.ComplainsRepository.GetComplains(Id).ToList();
            return PartialView("_viewComplain", data);
        }

        public IActionResult UpdateTbl(string status)
        {
            var data = _dataRepositoryWrapper.RegisterComplainRepository.GetRegisterComplains(status).ToList();
            return PartialView("_complainTable", data);
        }

        [HttpPut]
        public bool UpdateStatus(int Id, string status, string comment)
        {
            try
            {
                var data = _dataRepositoryWrapper.RegisterComplainRepository.Find(Id);
                data.Status = status;
                data.Comment = comment;
                 _dataRepositoryWrapper.RegisterComplainRepository.Update(data);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
