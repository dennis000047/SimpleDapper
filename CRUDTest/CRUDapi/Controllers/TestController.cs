using CRUDapi.TestService;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection;


namespace CRUDapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IConfiguration _config;
        private string _dbPrc;
        public TestController(IConfiguration config)
        {
            _config = config;
            _dbPrc = _config["ConnectionStrings:Practice"];
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetMemberInfo()
        {
            string sql = @"Select * From practice.personinfo";
            using (var conn = new MySqlConnection(_dbPrc))
            {
                var obj = await conn.QueryAsync<dynamic>(sql);
                return obj;
            }
        }
        [HttpGet("{id}")]
        public async Task<Practice> GetSingleMemberInfo(int id)//這邊使用自己創建的Practice類別也可以換成dynamic直接連資料庫拿
        {
            string sql = @"Select * from practice.personinfo where Id=@id";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("id", id);
            try
            {
                using (var conn = new MySqlConnection(_dbPrc))//有使用using 會自動關閉連接 else的話要自己加 Dispose
                {
                    //conn.Open();
                    var obj = await conn.QueryFirstOrDefaultAsync<Practice>(sql, dynamic);
                    //conn.Dispose();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
