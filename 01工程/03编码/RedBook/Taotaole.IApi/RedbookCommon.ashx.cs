using Newtonsoft.Json;
using System.Data;
using System.Web;
using Taotaole.Business;
using Taotaole.Common;
using YH.Utility;

namespace Taotaole.IApi
{
    /// <summary>
    /// RedbookCommon 的摘要说明
    /// </summary>
    public class RedbookCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "getBookList":
                    getBookList(context);
                    break;
                case "getBookContent":
                    getBookContent(context);
                    break;
            }
        }

        private void getBookContent(HttpContext context)
        {
            #region 参数验证和获取
            //验证参数合法性
            string bookid = context.Request["bookid"];
            if (string.IsNullOrEmpty(bookid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion

            string result = "";
            string where = "BusinessType = 'book' and BusinessId = '"+bookid+"'";
            DataTable dt = CustomsBusiness.GetListData("RB_Book_Content", where, "[Content]", "", 0, DbServers.DbServerName.LatestDB);
            result = JsonConvert.SerializeObject(dt, Formatting.Indented);
            context.Response.Write(result);
        }

        private void getBookList(HttpContext context)
        {
            //获取传参
            string categoryid = context.Request["categoryid"];
            if (string.IsNullOrEmpty(categoryid) || !PageValidate.IsNumberPlus(categoryid)) categoryid = "";
            string order = context.Request["order"];
            if (string.IsNullOrEmpty(order)) order = "0";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            //获取总条数、当前页数据、总页码
            string ps = context.Request["pagesize"];
            if (string.IsNullOrEmpty(ps) || !PageValidate.IsNumberPlus(ps)) ps = "10";

            //设置条件、排序
            string where = "Where 1=1 ";

            string orderby = "SortBaseNum asc";    //默认根据主键排序(分页最快)
            if (!string.IsNullOrEmpty(categoryid)) where += string.Format(" And categoryid='{0}'", categoryid);



            int currPage = int.Parse(p);
            string from = "(select rb.*,gm.headimg from RB_Book rb left join go_member gm on rb.UserId = gm.uid  ) t";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, ps.ToInt(), where, DbServers.DbServerName.LatestDB);
            int pageCount = count ;
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (int.Parse(ps) > 1)
                nextPage = (currPage < pageCount) ? (int.Parse(ps) + 1) : 0; //下一页为0时，表示无数据可加载  ;否则为当前的pagesize+1 (用于一次性加载相应数量的商品,避免重复调用该接口)
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}