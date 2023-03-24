using CRUDTest.Service;
using Microsoft.AspNetCore.Mvc;
using static CRUDTest.Service.MemberInfoService;
//命名空間先using創建的Service來此頁做使用
namespace CRUDTest.Controllers
{
    public class MemberController : Controller
    {
        //僅供此頁使用
        private MemberInfoService _memberService;
        //在控制器注入此服務
        public MemberController(IServiceProvider service) {
            _memberService = service.GetService<MemberInfoService>();
        } 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        //會去呼叫Service 後傳值回來 return一個view
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _memberService.GetSingleMemberInfo(id);
            return View(obj);
        }
        //抓全部資料
        [HttpGet]
        public async Task<IActionResult>List() 
        {
            var obj = _memberService.GetMemberInfo();
            return Ok(obj.Result);
        }
        //新增
        [HttpPost]
        public async Task<IActionResult> Create(MemberInfo mem)
        {
            var obj = await _memberService.CreatePersonInfo(mem);
            return Ok(obj);
        }
        //修改
        [HttpPost]
        public async Task<IActionResult> Edit(MemberInfo Minfo)
        {
           var obj = await _memberService.EditMemberInfo(Minfo);
            return Ok(obj);
        }
        //刪除
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var obj = await _memberService.DeleteMemberInfo(id);
            return Ok(obj);
        }
    }
}
