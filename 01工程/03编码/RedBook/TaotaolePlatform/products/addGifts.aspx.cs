using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace RedBookPlatform.products
{
    public partial class addGifts : Base
    {
        public int giftId;

        protected void Page_Load(object sender, EventArgs e)
        {
            giftId = Convert.ToInt32(Request.QueryString["giftId"]);
            if (!this.IsPostBack)
            {
                init();
            }
        }

        protected void init()
        {
            //载入分类下拉框
            DataTable categoryRows = go_categoryBusiness.GetListData("model=1", "name,cateid");
            ddlCategory.DataSource = categoryRows;
            ddlCategory.DataTextField = "name";
            ddlCategory.DataValueField = "cateid";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("请选择分类", "0"));
            ddlCategory_SelectedIndexChanged(null, null);
            //加载区间选项
            DataTable giftNum = go_giftNumsBusiness.GetListData();
            if (giftNum.Rows.Count > 0)
            {
                for (int i = 0; i < giftNum.Rows.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = giftNum.Rows[i]["nums"].ToString();
                    item.Value = giftNum.Rows[i]["nums"].ToString();
                    this.rdoScope.Items.Add(item);
                    this.rdoScope.SelectedValue = giftNum.Rows[i]["nums"].ToString();
                }
            }
            else
            {
                base.Response.Write("<script>alert('请先配置积分场次');</script>");

            }

            if (giftId > 0)
            {

                load();
            }
        }

        protected void load()
        {
            go_giftsEntity gifts = go_giftsBusiness.LoadEntity(giftId);
            title.Value = gifts.Title;
            stock.Value = gifts.Stock.ToString();
            hidChange.Value = giftId.ToString();
            litScope.Text = gifts.Scope.ToString();
            litGameType.Text =Globals.getGiftGameType(gifts.Giftsuse.ToString());
            litGiftSum.Text = gifts.sumCode.TrimEnd(',').TrimStart(',').Split(',').Length.ToString();
            litWinSum.Text = gifts.winCode.TrimEnd(',').TrimStart(',').Split(',').Length.ToString();
            litPrizeNumber.Text = gifts.PrizeNumber.ToString();
            litProbability.Text = gifts.Probability.ToString();
            rdoGame.SelectedValue = gifts.Giftsuse.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_giftsEntity giftsProduc = new go_giftsEntity();



            //添加模式
            if (giftId <= 0)
            {
                string sum = "";
                int codeCounts = int.Parse(inpGuaProduct.Value);
                giftsProduc = new go_giftsEntity()
                {
                    Productid = Convert.ToInt32(ddlProducts.SelectedValue),
                    Title = title.Value,
                    Scope = int.Parse(rdoScope.SelectedValue),
                    Probability = int.Parse(probability.Value),
                    Stock = stock.Value,
                    Addtime = DateTime.Now,
                    PrizeNumber = PrizeNumber.Value,
                    Giftsuse = rdoGame.SelectedValue,
                    codeCount = codeCounts
                };

                
                //根据所填的商品总数 生成自然数
                for (int i = 0; i < codeCounts; i++)
                {
                    sum += i + ",";
                }
                giftsProduc.sumCode = ","+sum;
                //得到中奖码
                int[] sumIndexWin = GetRandomArray(int.Parse(inpGuaPrize.Value), 0, int.Parse(inpGuaPrize.Value));
                string winCodes = "";
                for (int a = 0; a < sumIndexWin.Length; a++)
                {
                    winCodes += sumIndexWin[a] + ",";
                }

                giftsProduc.winCode = "," + winCodes;

                if (go_giftsBusiness.SaveEntity(giftsProduc, true))
                {
                    base.addAdminLog(operationAction.Add, null, "添加了积分商品" + title.Value);
                }

            }
            else//编辑模式
            {
                giftsProduc = go_giftsBusiness.LoadEntity(giftId);
                giftsProduc.Title = title.Value;
                giftsProduc.Stock = stock.Value;
                if (go_giftsBusiness.SaveEntity(giftsProduc, false))
                {
                    base.addAdminLog(operationAction.Update, null, "修改了积分商品" + title.Value);
                }


            }

            go_configsEntity ec = go_configsBusiness.LoadEntity(3);
            ec.Ctime = DateTime.Now;
            go_configsBusiness.SaveEntity(ec, false);
            //新增or修改成功后跳转到列表页面
            Response.Redirect("gifts.aspx");
        }

        // Number随机数个数
        // minNum随机数下限
        // maxNum随机数上限
        public int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        /// <summary>
        /// 绑定商品品牌下拉框
        /// </summary>
        private void BindBrand()
        {
            ddBrand.Items.Clear();
            if (ddlCategory.SelectedValue != "0")
            {
                DataTable productRows = go_brandBusiness.GetListData(" cateid = " + ddlCategory.SelectedValue, "id,name");
                ddBrand.DataSource = productRows;
                ddBrand.DataTextField = "name";
                ddBrand.DataValueField = "id";
                ddBrand.DataBind();
            }
            ddBrand.Items.Insert(0, new ListItem("请选择品牌", "0"));
        }

        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        private void BindProducts()
        {
            ddlProducts.Items.Clear();
            if (ddlCategory.SelectedValue != "0")
            {
                string where = " categoryId = " + ddlCategory.SelectedValue;
                if (ddBrand.SelectedValue != "0")
                {
                    where += string.Format(" And brandid={0}", ddBrand.SelectedValue);
                }
                DataTable productRows = go_productsBusiness.GetListData(where, "productId,title");
                ddlProducts.DataSource = productRows;
                ddlProducts.DataTextField = "title";
                ddlProducts.DataValueField = "productId";
                ddlProducts.DataBind();
            }
            ddlProducts.Items.Insert(0, new ListItem("请选择商品", "0"));
        }

        //商品分类下拉框事件
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBrand();
            BindProducts();
        }

        //品牌下拉框事件
        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProducts();
        }

        /// <summary>
        /// 商品下拉框选中事件
        /// </summary>
        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            go_productsEntity product = go_productsBusiness.LoadEntity(Convert.ToInt32(ddlProducts.SelectedValue));
            title.Value = product.Title;
        }

    }
}