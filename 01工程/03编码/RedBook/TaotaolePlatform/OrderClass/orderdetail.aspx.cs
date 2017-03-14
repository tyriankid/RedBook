using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Business;
using System.Data;
namespace RedBookPlatform.OrderClass
{
    public partial class orderdetail : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindinfo();
            }
        }
        public string showname = null;
        public string showphone = null;
        public string showaddress = null;
        public string shuwexpress = null;
        private void bindinfo()
        {
            string orderid = Request["orderid"].ToString();
            DataTable dt = CustomsBusiness.GetListData("go_orders o join go_member m on o.uid=m.uid left join go_activity_code a on a.order_id=o.orderId join go_gifts g on o.businessId=g.giftid join go_products p on g.productid =p.productid left join go_card_recharge c on o.orderId=c.orderId", "o.orderId='" + orderid + "'", "o.orderId,o.status,o.phone,o.order_address_info,m.username,p.typeid,p.title,o.money,o.express_company,o.express_code ", null, 1, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count > 0)
            {
                this.Money.Text = Convert.ToDecimal(dt.Rows[0]["money"]).ToString("0.00");
                this.Name.Text = dt.Rows[0]["username"].ToString();
                this.Shoptype.Text = Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.productType, Convert.ToInt32(dt.Rows[0]["typeid"]));
                this.Num.Text = dt.Rows[0]["orderid"].ToString();
                this.Phone.Text = dt.Rows[0]["phone"].ToString();
                this.ShopName.Text = dt.Rows[0]["title"].ToString();
                this.status.Text = dt.Rows[0]["status"].ToString();

                if (dt.Rows[0]["order_address_info"].ToString() == "")
                {
                    showname = "收货人姓名：";
                    showphone = "收货人号码：";
                    showaddress = "收货地址：";
                    shuwexpress = "";
                }
                else
                {
                    switch (dt.Rows[0]["typeid"].ToString())
                    {
                        case "0"://0:实体,
                            showname = "收货人姓名：";
                            showaddress = "收货地址：";
                            showphone = "收货人号码：";
                            shuwexpress = "快递信息：";
                            this.Address.Text = dt.Rows[0]["order_address_info"].ToString().Split('*')[0];
                            this.Sname.Text = dt.Rows[0]["order_address_info"].ToString().Split('*')[1];
                            this.Express.Text = dt.Rows[0]["express_company"].ToString() + "：" + dt.Rows[0]["express_code"].ToString();
                            break;
                        case "1"://1:卡密
                            showaddress = "卡密信息：";
                            showname = "收货人姓名：";
                            showphone = "备注：";
                            this.Phone.Text = dt.Rows[0]["order_address_info"].ToString().Split('*').Length == 0 ? dt.Rows[0]["order_address_info"].ToString() : dt.Rows[0]["order_address_info"].ToString().Split('*')[0].Split('：').Length == 1 ? dt.Rows[0]["order_address_info"].ToString().Split('*')[0] : dt.Rows[0]["order_address_info"].ToString().Split('*')[0].Split('：')[1];
                            this.Address.Text = dt.Rows[0]["order_address_info"].ToString().Split('*')[0];
                            this.Sname.Text = dt.Rows[0]["username"].ToString();
                            break;
                        case "2"://2:游戏
                            showaddress = "游戏地址：";
                            showname = "游戏账号：";
                            showphone = "收货人号码：";
                            this.Address.Text = dt.Rows[0]["order_address_info"].ToString().Split('@')[0];
                            this.Sname.Text = dt.Rows[0]["order_address_info"].ToString().Split('@')[1].Split('*')[0];
                            break;
                        case "3"://,3:Q币,4:账号
                            break;
                    }
                }
            }
        }
    }
}