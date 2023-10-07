using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc_ETicaret_WebMvcUI.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFilterSpecService _filterSpecService;
        private readonly IProductSpecsService _productSpecsService;
        private readonly ICommentService _commentService;
        private readonly IAccountService _accountService;
        CookieOptions options = new CookieOptions();
        public UrunlerController(IProductService productService, IFilterSpecService filterSpecService, IProductSpecsService productSpecsService, ICommentService commentService, IAccountService accountService)
        {
            _productService = productService;
            _filterSpecService = filterSpecService;
            _productSpecsService = productSpecsService;
            _commentService = commentService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index(int? id, string[]? value)
        {
            var products = await _productService.GetListAllByFilter(x => x.IsDeleted == false && x.CategoryId == id);
            options.Expires = DateTime.Now.AddDays(1);
            if (value.Count() > 0)
            {
                var values = await _productService.GetProductsBySpecs(products.ToList(), id, value);
                ViewBag.specs = await _filterSpecService.GetAll(x => x.CategoryId == id);
                return View(values);
            }
            ViewBag.specs = await _filterSpecService.GetAll(x => x.CategoryId == id);
            Response.Cookies.Append("category", id.ToString(), options);
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.GetByFilter(x => x.Id == id,x=>x.Category);
            ViewBag.specs = await _productSpecsService.GetListAllByFilter(x => x.ProductId == id);
            ViewBag.Comments = await _commentService.GetAllByProductId(id);
            options.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("category", model.CategoryId.ToString(), options);
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> CreateComment(int Id, string Message)
        {
            var user = await _accountService.Find(User.Identity.Name);//al
            CommentViewModel model = new()
            {
                ProductId = Id,
                Description = Message,
                UserId = user.Id,
            };
            await _commentService.Add(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
    }
}
