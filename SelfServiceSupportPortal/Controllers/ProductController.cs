using AadharVerify.Data.Core.DataRepository.RepositoryWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SelfServiceSupportPortal.Data.Core.Data;
using SelfServiceSupportPortal.Data.Model;
using SelfServiceSupportPortal.Data.Model.ViewModels;

namespace SelfServiceSupportPortal.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly SelfServiceSupportPortalDbContext _context;
        private readonly IDataRepositoryWrapper _dataRepositoryWrapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(SelfServiceSupportPortalDbContext context, IDataRepositoryWrapper dataRepositoryWrapper, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _dataRepositoryWrapper = dataRepositoryWrapper;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var data = _dataRepositoryWrapper.ProductsRepository.GetProducts().ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            var productVm = new ProductVM
            {
                CompanyList = _dataRepositoryWrapper.CompanyRepository.GetAll().ToList()
            };
            return PartialView("_create", productVm);
        }
        
        public IActionResult Edit(int Id)
        {
            if(Id > 0)
            {
                var data = _dataRepositoryWrapper.ProductsRepository.Find(Id);
                if(data != null)
                {
                    var productVm = new ProductVM
                    {
                        Tag = data.Tag,
                        McSerialNo = data.McSerialNo,
                        BrandModuleNo = data.BrandModuleNo,
                        Department = data.Department,
                        Location = data.Location,
                        Address = data.Address,
                        CompanyId = data.CompanyId,
                        CompanyList = _dataRepositoryWrapper.CompanyRepository.GetAll().ToList()
                    };
                    return PartialView("_edit", productVm);
                }
            }
            return PartialView("_create");
        }

        public IActionResult ReloadProductTbl()
        {
            var data = _dataRepositoryWrapper.ProductsRepository.GetProducts().ToList();
            return PartialView("_productTbl", data);
        }

        public bool CreateWithCSV(IFormFile file, int comanyId)
        {
            if (file != null && file.Length > 0)
            {

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // or LicenseContext.Commercial

                using (var stream = file.OpenReadStream())
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    IList<Products> dataList = new List<Products>();

                    for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                    {
                        string? Tag = worksheet.Cells[row, 1].Value?.ToString();
                        string? McSerialNo = worksheet.Cells[row, 2].Value?.ToString();
                        string? Model = worksheet.Cells[row, 3].Value?.ToString();
                        string? Department = worksheet.Cells[row, 4].Value?.ToString();
                        string? Location = worksheet.Cells[row, 5].Value?.ToString();
                        string? Address = worksheet.Cells[row, 6].Value?.ToString();

                        var myData = new Products
                        {
                            Tag = Tag,
                            McSerialNo = McSerialNo,
                            BrandModuleNo = Model,
                            Department = Department,
                            Location = Location,
                            Address = Address,
                            CompanyId = comanyId,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            StartedDate = DateTime.Now,
                        };

                        dataList.Add(myData);
                    }

                    try
                    {
                        _dataRepositoryWrapper.ProductsRepository.InsertRange(dataList);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

            }
            return false;
        }

        public IActionResult Save(Products products)
        {
            if(products.ID > 0)
            {
                var data = _dataRepositoryWrapper.ProductsRepository.Find(products.ID);
                if (data != null)
                {
                    data.Tag = products.Tag;
                    data.McSerialNo = products.McSerialNo;
                    data.BrandModuleNo = products.BrandModuleNo;
                    data.Department = products.Department;
                    data.Location = products.Location;
                    data.Address = products.Address;
                    data.CompanyId = products.CompanyId;
                    data.UpdatedDate = DateTime.Now;
                    _dataRepositoryWrapper.ProductsRepository.Update(data);
                }
            }
            else
            {
                var product = new Products
                {
                    Tag = products.Tag,
                    McSerialNo = products.McSerialNo,
                    BrandModuleNo = products.BrandModuleNo,
                    Department = products.Department,
                    Location = products.Location,
                    Address = products.Address,
                    CompanyId = products.CompanyId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    StartedDate = DateTime.Now,
                };
            _dataRepositoryWrapper.ProductsRepository.Create(product);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                var data = _dataRepositoryWrapper.ProductsRepository.Find(Id);
                if (data != null)
                {
                    var productVm = new ProductVM
                    {
                        ID = data.ID,
                        Tag = data.Tag,
                        McSerialNo = data.McSerialNo,
                        BrandModuleNo = data.BrandModuleNo,
                        Department = data.Department,
                        Location = data.Location,
                        Address = data.Address,
                        CompanyId = data.CompanyId,
                        CompanyList = _dataRepositoryWrapper.CompanyRepository.GetAll().ToList(),
                        IsReadOnlyPage = true
                    };
                    return PartialView("_edit", productVm);
                }
            }
            return PartialView("_create");
        }

        public IActionResult DeleteSave(int Id)
        {
            if (Id > 0)
            {
                var data = _dataRepositoryWrapper.ProductsRepository.Find(Id);
                if (data != null)
                {
                    _dataRepositoryWrapper.ProductsRepository.Delete(data);
                    return RedirectToAction("Index");
                }
            }
            return PartialView("_create");
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var productData = _dataRepositoryWrapper.ProductsRepository.GetProductDetailsByProductId(id);
            if(productData != null)
            {
                var productComplaints = _dataRepositoryWrapper.ProductComplaintRepository.GetAll();
                ViewBag.ProductComplaints = productComplaints;
                productData.Id = productData.Id;
                return View(productData);
            }
            return Content("Content not found");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RegisterComplains(ProductDetailsVM productDetailsVM)
        {
            //string logInUserId = _userManager.GetUserAsync(User).Result.Id;
            if (productDetailsVM.ComplainIds != null)
            {
                RegisterComplain registerComplain = new RegisterComplain()
                {
                    ProductId = productDetailsVM.ProductId,
                    Remark = productDetailsVM.ComplainDescription,
                    RegisterdDate = DateTime.Now,
                    Status = "Open",
                    Phonenumber = productDetailsVM.Phonenumber
                };
                _dataRepositoryWrapper.RegisterComplainRepository.Create(registerComplain);

                foreach(var v in productDetailsVM.ComplainIds)
                {
                    Complains c = new Complains()
                    {
                        RegisterComplainId = registerComplain.ID,
                        ComplainId = v
                    };

                    _dataRepositoryWrapper.ComplainsRepository.Create(c);
                }
            }
              //return  RedirectToAction("Details", new {id = productDetailsVM.ProductId});
              return  RedirectToAction("ThanksScreen");
        }

        [AllowAnonymous]
        public IActionResult ThanksScreen()
        {
            return View();
        }
    }
}
