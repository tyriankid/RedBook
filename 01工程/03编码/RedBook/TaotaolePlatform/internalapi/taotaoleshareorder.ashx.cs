using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using System.Data;

namespace RedBookPlatform.internalapi
{
    /// <summary>
    /// taotaoleshareorder 的摘要说明
    /// </summary>
    public class taotaoleshareorder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "publishshaidan"://发表商品晒单
                    publishshaidan(context);
                    break;
                case "updateuserimg"://更改用户头像
                    updateuserimg(context);
                    break;
            }
        }
       
        /// <summary>
        /// 发布商品晒单
        /// 返回状态state 0发表成功 2未知的签名参数 3第一张图片不存在 4保存失败 5订单id不存在 6你没有中奖不能发表晒单 7你已经对此订单发表过晒单
        /// </summary>
        private void publishshaidan(HttpContext context)
        {
            #region *******************************************参数的验证和获取*******************************************

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string orderid = context.Request["orderid"];
            string qishu = context.Request["qishu"];
            string title = context.Request["title"];
            string contents = context.Request["contents"];

            //根据订单号，查询用户是否中奖，是否发布过晒单，等一系列操作
            go_ordersEntity ordersEntity = go_ordersBusiness.LoadEntity(orderid, DbServers.DbServerName.ReadHistoryDB);
            if (ordersEntity == null)
            {
                context.Response.Write("{\"state\":5}");    //订单id不存在
                return;
            }
            if (Convert.ToInt32(uid) != ordersEntity.Uid || ordersEntity.Iswon != 1)
            {
                context.Response.Write("{\"state\":6}");    //你没有中奖不能发表晒单
                return;
            }
            DataTable dt = go_shaidanBusiness.GetListData("sd_orderid='" + orderid + "'", "*", null, 1, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count != 0)
            {
                context.Response.Write("{\"state\":7}");    //你已经对此订单发表过晒单
                return;
            }

            //获取第一张图片base64编码
            string file1 = context.Request["file1"];
            #endregion

            if (string.IsNullOrEmpty(file1))
            {
                context.Response.Write("{\"state\":3}");    //第一张图片不存在
                return;
            }
            string sqlimg = "";
            sqlimg += ShopOrders.uploadsimg(file1, "shaidan", context) + "|";


            //最多上传9张图片  循环接收base64编码，并保存
            string currFile=string.Empty;
            for (int i = 2; i < 10; i++)
            {
                currFile = context.Request["file" + i.ToString() + ""];
                if (string.IsNullOrEmpty(currFile)) break;
                sqlimg += ShopOrders.uploadsimg(currFile,"shaidan", context) + "|";
            }
            
            //根据信息保存实体类，并保存到数据库
            go_shaidanEntity shaidanEntity = new go_shaidanEntity
            {
                Sd_ip=NetworkHelper.GetBuyIP(),
                Sd_orderid=orderid,
                Sd_title=title,
                Sd_content=contents,
                Sd_time=DateTime.Now,
                Sd_zan=0,
                Sd_userid= Convert.ToInt32( uid),
                Sd_ping=0,
                Sd_qishu=Convert.ToInt32(qishu),
                Sd_photolist=sqlimg
            };

            DataTable sdt = go_shaidanBusiness.GetListData("sd_orderid='" + orderid + "' and sd_userid='" + uid + "'", "sd_id", null, 1, DbServers.DbServerName.LatestDB);
            if (!go_shaidanBusiness.SaveEntity(shaidanEntity, true))
            {
                context.Response.Write("{\"state\":4}");    //保存失败
                return;
            }

            go_activity_codeBusiness.sentUserRedpack(uid, "31");//晒单活动发包接口
            //context.Response.Write("{\"state\":0,\"sd_id\":" + sdt.Rows[0]["sd_id"] + "}");    //保存失败
            context.Response.Write("{\"state\":0}");//保存成功
            return;
        }


        /// <summary>
        /// 更改用户头像
        /// </summary>
        private void updateuserimg(HttpContext context)
        {
            #region *******************************************参数的验证和获取*******************************************

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //获取第一张图片base64编码
            string file1 = context.Request["file1"];
            #endregion
            if (string.IsNullOrEmpty(file1))
            {
                context.Response.Write("{\"state\":3}");    //第一张图片不存在
                return;
            }
                string date = DateTime.Now.ToString("yyyyMMdd");
                //将base64编码解析，返回后缀名
                byte[] buffer = Convert.FromBase64String(file1);
                string fileurl = context.Server.MapPath(Globals.UPLOAD_PATH + "member" + "/" + date + "/" + Guid.NewGuid().ToString() + ".jpg" );
                string bcfile = "member" + "/" + date + "/" + Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists(Path.GetDirectoryName(fileurl)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileurl));
                }
                string sfilepaths = context.Server.MapPath(Globals.UPLOAD_PATH + bcfile);
                File.WriteAllBytes(sfilepaths, buffer);
                go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
                memberEntity.Headimg = "";
                memberEntity.Img = bcfile;
                if (go_memberBusiness.SaveEntity(memberEntity, false, DbServers.DbServerName.LatestDB))
                {
                    context.Response.Write("{\"state\":0}");    //更新成功
                    return;
                }

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