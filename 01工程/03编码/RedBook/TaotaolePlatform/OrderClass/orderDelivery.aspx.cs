using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.OrderClass
{
    public partial class orderDelivery : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderInfo = Request.QueryString["orderId"].ToString();
            string orderID = orderInfo.Split(',').GetValue(0).ToString();
            string ordertype = orderInfo.Split(',').GetValue(1).ToString();
            load(orderID, ordertype);
        }
        protected void load(string orderID, string ordertype)
        {   
            hidOrderId.Value = orderID;
            if (ordertype != null && ordertype == "yuan")
            {
                #region  一元购
                divTuangou.Visible = false;
                if (orderID != null)
                {
                    DataTable dtOrderInfo = go_ordersBusiness.LoadDataYuan(orderID);
                    if (dtOrderInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtOrderInfo.Rows)
                        {
                            //商品信息
                            trFahuo.Visible = false; ;
                            litOrderID.Text = orderID;
                            litSendTime.Text = dr["sendtime"].ToString();
                            litPayType.Text = dr["pay_type"].ToString();
                            litProductName.Text = dr["title"].ToString();
                            litQishu.Text = dr["qishu"].ToString();
                            litMaxQishu.Text = dr["maxqishu"].ToString();
                            litLuckyName.Text = dr["username"].ToString();
                            litGoumai.Text = dr["quantity"].ToString();
                            litZongxu.Text = dr["zongrenshu"].ToString();
                            litJiage.Text = Convert.ToDouble(dr["pdmoney"]).ToString("0.00");
                            litYunGou.Text = dr["goucode"].ToString();
                            litPay.Text = Convert.ToDouble(dr["money"].ToString()).ToString("0.00");
                            //用户信息
                            litMemberID.Text = dr["uid"].ToString();
                            litMemberName.Text = dr["username"].ToString();
                            litMemberPhone.Text = dr["phone"].ToString();
                            litMemberEmail.Text = dr["email"].ToString();
                            litTime.Text = dr["time"].ToString();
                            litaddress.Text = dr["order_address_info"].ToString();
                            //发货信息
                            litState.Text = dr["status"].ToString();
                            if (dr["status"].ToString() == "已发货" || dr["status"].ToString() == "已收货")
                            {
                                btnSave.Visible = false;
                                inpCompany.Visible = false;
                                litCompany.Text = dr["express_company"].ToString();
                                trFahuo.Visible = true;
                            }
                        }
                    }
                }
                #endregion
            }
            if(ordertype != null && ordertype == "tuan")
            {
                #region 团购
                divYuangou.Visible = false;
                DataTable dtOrderInfo = go_ordersBusiness.LoadDataTuan(orderID);
                if (dtOrderInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtOrderInfo.Rows)
                    {   
                        //商品信息
                        trFaHuoHou.Visible = false;
                        litTuanOrderID.Text = orderID;
                        litTuanProductName.Text=dr["ProductName"].ToString();
                        litNum.Text=dr["total_num"].ToString();
                        litProductJiage.Text = Convert.ToDouble(dr["pdmoney"]).ToString("0.00");
                        litStarTime.Text = dr["start_time"].ToString();
                        litEndTime.Text = dr["end_time"].ToString();
                        litLuckyMan.Text = dr["username"].ToString();
                        litGouMaiMa.Text = dr["goucode"].ToString();
                        litCiShu.Text = dr["quantity"].ToString();
                        litDanJia.Text = Convert.ToDouble(dr["per_price"].ToString()).ToString("0.00");
                        litOutGoods.Text=dr["sendtime"].ToString();
                        litPayTypeTuan.Text = dr["pay_type"].ToString();
                        //用户信息
                            litMemberID.Text = dr["uid"].ToString();
                            litMemberName.Text = dr["username"].ToString();
                            litMemberPhone.Text = dr["phone"].ToString();
                            litMemberEmail.Text = dr["email"].ToString();
                            litTime.Text = dr["time"].ToString();
                            litaddress.Text = dr["order_address_info"].ToString();
                            //发货信息
                            litState.Text = dr["status"].ToString();
                            if (dr["status"].ToString() == "已发货" || dr["status"].ToString() == "已收货")
                            {
                                btnSave.Visible = false;
                                inpCompany.Visible = false;
                                litCompany.Text = dr["express_company"].ToString();
                                trFaHuoHou.Visible = true;
                            }
                    }
                }
                #endregion
            }
        }
        [System.Web.Services.WebMethod]
        public static string delivery(string company, string orderId)
        {
            go_ordersEntity orderEntity = go_ordersBusiness.LoadEntity(orderId);
            if (orderEntity != null)
            {
                orderEntity.Express_company = company;
                orderEntity.Status ="已发货";
                orderEntity.Sendtime = DateTime.Now;
                go_ordersBusiness.SaveEntity(orderEntity, false);
            }
            return "发货成功！";
        }
    }
}