using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Model;
using Taotaole.Bll;
using System.Data;
using Taotaole.Business;


namespace RedBookPlatform.CardRecharge
{
    public partial class AddCardRecharge : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int num =Convert.ToInt32( TextBox1.Text);

            //根据主键查询datatable    得到0条数据
            DataTable dt = go_card_rechargeBusiness.LoadData(0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            //查询到最大ID，用于下面id赋值
            DataTable de = go_card_rechargeBusiness.GetListData(null, "*", "id desc", 1, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            int initID = de.Rows.Count == 0  ? 1 : Convert.ToInt32(de.Rows[0]["id"]);
            for (int i = 1; i <= num; i++)
            {
                Random rd=new Random ();
                DataRow dr = dt.NewRow();
                dr["id"] =initID + i;

                string code = GetRandomCode(Convert.ToInt32(txtcode.Text.Trim()));

                dr["code"]= code;
                dr["codepwd"] = GetRandomCode(Convert.ToInt32(txtpwd.Text.Trim()));
                dr["money"]=0;
                //卡密添加时间
                dr["time"]=DateTime.Now.ToString();
                //卡密到期时间
                dr["rechargetime"]="2022-8-18 19:28:55.222";
                dr["orderId"]="";
                dr["uid"]=0;
                dr["isrepeat"]=0;
                dr["usebeizhu"]="";
                dr["usetype"] = 0;
                dt.Rows.Add(dr);
            }
            //整表提交更新数据库
            if (CustomsBusiness.CommitDataTable(dt, "select * from go_card_recharge", YH.Utility.DbServers.DbServerName.LatestDB))
            {

            } Response.Redirect("CardRechargeList.aspx");
        }



        public string GetRandomCode(int numlen)
        {
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'  };
          
            string code = string.Empty;

            for (int i = 0; i < numlen; i++)
            {
                //这里是关键，传入一个seed参数即可保证生成的随机数不同           
              //  Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
               Random rnd = new Random(GetRandomSeed());
                code += chars[rnd.Next(0, 10)].ToString();
            }

            return code;
        }

        /// <summary>
        /// 加密随机数生成器 生成随机种子
        /// </summary>
        /// <returns></returns>

        static int GetRandomSeed()
        {

            byte[] bytes = new byte[4];

            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

            rng.GetBytes(bytes);

            return BitConverter.ToInt32(bytes, 0);

        }
    }
}