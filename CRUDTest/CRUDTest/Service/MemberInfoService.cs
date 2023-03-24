using MySql.Data.MySqlClient;
using Dapper;
using System;

namespace CRUDTest.Service

{
    public class MemberInfoService
    {
        public class MemberInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime EditTime { get; set; }
        }


        //連線相關資訊
        private IConfiguration _config;
        private string _dbPrc;
        public MemberInfoService(IConfiguration config)
        {
            _config = config;
            _dbPrc = _config["ConnectionStrings:Practice"];
        }

        /// <summary>
        /// 取得 mysql連線字串
        /// </summary>
        /// <returns></returns>
        public string GetDBConString()
        {
            var mysqlConString = _config["ConnectionStrings:Practice"];
            return mysqlConString;
        }
        public async Task<IEnumerable<dynamic>> GetMemberInfo()
        {
            string sql = @"Select * From practice.personinfo";
            using (var conn = new MySqlConnection(_dbPrc))
            {
                var obj = await conn.QueryAsync<dynamic>(sql);
                return obj.AsEnumerable();
            }
        }

        public async Task<MemberInfo> GetSingleMemberInfo(int id)
        {
            string sql = @"Select * from practice.personinfo where Id=@id";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("id", id);
            try
            {
                using (var conn = new MySqlConnection(_dbPrc))
                {
                    conn.Open();
                    var obj = await conn.QueryFirstOrDefaultAsync<MemberInfo>(sql, dynamic);
                    conn.Dispose();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Dennis實作 Create, Edit, Delete 這三個方法

        public async Task<bool> CreatePersonInfo(MemberInfo member)
        {
            using (var conn = new MySqlConnection(_dbPrc))
            {
                string sql = $"Insert into `practice`.personinfo(Name,Tel,CreateTime)" +
                $"Values(@Name,@Tel,@CreateTime);";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("Name", member.Name);
                dynamic.Add("Tel", member.Tel);
                dynamic.Add("CreateTime", DateTime.Now);

                var obj = await conn.ExecuteAsync(sql, dynamic);
                conn.Dispose();
                return obj > 0 ? true : false;
            }
        }

        public async Task<bool> EditMemberInfo(MemberInfo member)
        {
            using (var conn = new MySqlConnection(_dbPrc))
            {
                try
                {
                    string sql = $"Update `practice`.personinfo set Name = @Name, Tel = @Tel, EditTime = @EditTime " +
                        $" WHERE Id = @Id ";

                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("Name", member.Name);
                    dynamic.Add("Tel", member.Tel);
                    dynamic.Add("EditTime", DateTime.Now);
                    dynamic.Add("Id", member.Id);

                    var obj = await conn.ExecuteAsync(sql, dynamic);

                    conn.Dispose();
                    return obj > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<bool>DeleteMemberInfo(int id)
        {
            using ( var conn = new MySqlConnection(_dbPrc))
            {
                string sql = $"Delete from`practice`.personinfo where Id=@Id";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("Id", id);
                var obj = await conn.ExecuteAsync(sql, dynamic);
                conn.Dispose();
                return obj > 0 ? true : false;
            }
        }
    }
}
