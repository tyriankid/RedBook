using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using YH.Weixin.MP.Messages;

namespace RedBookPlatform.OrderClass
{
    public partial class orderSendGoods :Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderId = "";
            if (!string.IsNullOrEmpty(Request.QueryString["orderIds"]))
            {
                orderId= Request.QueryString["orderIds"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["orderId"]))
            {
                orderId = Request.QueryString["orderId"].ToString();
            }
            if (!IsPostBack)
            {
                load(orderId);
            }
        }
        protected void load(string ID)
        {
             hidOrderId.Value = ID;
             string  orderIds=ID.TrimEnd(',');
             DataTable dtOrderInfo = go_ordersBusiness.LoadAllSend(orderIds);
             this.rptAdmin.DataSource = dtOrderInfo;
             this.rptAdmin.DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string allSendGoods(string company, string orderId, string code)
        {   
           string[] orderIds=orderId.TrimEnd(',').Split(',');
           string[] companys = company.TrimEnd(',').Split(',');
           string[] codes = code.TrimEnd(',').Split(',');
           for (int i = 0; i < orderIds.Length; i++)
            {
                go_ordersEntity orderEntity = go_ordersBusiness.LoadEntity(orderIds.GetValue(i).ToString());
                if (orderEntity != null)
                {
                    orderEntity.Express_company = companys.GetValue(i).ToString();
                    orderEntity.Status = "已发货";
                    orderEntity.Express_code = codes.GetValue(i).ToString();
                    orderEntity.Sendtime = DateTime.Now;
                    go_ordersBusiness.SaveEntity(orderEntity, false);

                    //发货通知
                    go_memberEntity memberEntity = go_memberBusiness.LoadEntity(orderEntity.Uid);
                    int originalid = 0; int productid = 0; string title = string.Empty;
                    switch (orderEntity.Ordertype)
                    { 
                        case "yuan":
                            go_yiyuanEntity yiyuanEntity = go_yiyuanBusiness.LoadEntity(orderEntity.BusinessId);
                            originalid = yiyuanEntity.originalid;
                            productid = yiyuanEntity.Productid;
                            title = yiyuanEntity.Title;
                            break;
                        case "tuan":
                            go_tuan_listinfoEntity tuan_listinfoEntity = go_tuan_listinfoBusiness.LoadEntity(orderEntity.BusinessId);
                            if (tuan_listinfoEntity != null)
                            {
                                go_tuanEntity tuanEntity= go_tuanBusiness.LoadEntity(tuan_listinfoEntity.TuanId);
                                if (tuanEntity != null)
                                {
                                    originalid = productid = tuanEntity.Productid;
                                    title = tuanEntity.Title;
                                }
                            }
                            break;
                    }
                    if (!string.IsNullOrEmpty(memberEntity.Wxid) && productid!=0)
                    {
                        string url = string.Format("user.winningconfirm.aspx?shopid={0}&orderid={1}&productid={2}", orderEntity.BusinessId, orderEntity.OrderId, originalid);
                        Messenger.SendGoodsSuccess(memberEntity.Wxid, title, orderEntity.Express_code, orderEntity.Express_company, url);
                    }
                    
                }
            }
            return "发货成功！";
        } 
    }
}