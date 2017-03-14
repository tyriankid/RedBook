using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Business;

namespace RedBookPlatform.wx
{
    public partial class warning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindWarningCodetable();
            BindWarningAddmoney();
            BindWarningOrders();
        }


        /// <summary>
        /// 可用码过小预警
        /// </summary>
        private void BindWarningCodetable()
        {
            string selectSql = @"
                Declare @warning table(yid int,codetable nvarchar(255),shengyurenshu int,buyquantity int )
                Declare @yid int Declare @shengyurenshu int Declare @codetable varchar(200)
                Declare @sql nvarchar(max) Declare @quantity int=0
                declare auth_cur cursor for
                Select yId,shengyurenshu,codetable From go_yiyuan where shengyurenshu>0
                open auth_cur
                fetch next from auth_cur into @yid,@shengyurenshu,@codetable
                while (@@fetch_status=0)
                  begin
	                Set @sql='Select @quantity=COUNT(*) From '+@codetable+' where codetype=''yuan'' and businessId='+RTRIM(@yid)+' and status=0'
	                exec sp_executesql @sql,N'@quantity int out',@quantity out
	                if(@quantity<>@shengyurenshu)
                    Begin
		                insert into @warning values(@yid,@codetable,@shengyurenshu,@quantity)
                    End
                    fetch next from auth_cur into @yid,@shengyurenshu,@codetable
                  end
                close auth_cur
                deallocate auth_cur
                Select T.*,'('+RTRIM(y.qishu)+')'+y.title as shopname From @warning T Inner Join go_yiyuan Y on T.yid=Y.yId";
            DataSet dsData= CustomsBusiness.GetSql(selectSql);
            RepeaterInfo1.DataSource = dsData.Tables[0];
            RepeaterInfo1.DataBind();
            ViewState["dtWarningCodetable"] = dsData.Tables[0];
        }

        /// <summary>
        /// 充值小于1预警
        /// </summary>
        private void BindWarningAddmoney()
        {
            string strdatetime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string selectSql = @"
            Declare @time datetime='{0}'
            Select * From go_member_addmoney_record Where pay_type='微信支付' and status='已付款' and money<1 and memo<>'tuan' and time>=@time order by time desc";
            selectSql = selectSql.Replace("{0}", strdatetime);
            DataSet dsData = CustomsBusiness.GetSql(selectSql);
            RepeaterInfo2.DataSource = dsData.Tables[0];
            RepeaterInfo2.DataBind();
        }

        /// <summary>
        /// 单次购买大于5的订单
        /// </summary>
        private void BindWarningOrders()
        {
            string strdatetime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string selectSql = @"
            Select * From go_orders where time>='{0}' And quantity>=5 And uid not in(Select uid From go_member where typeid=9)";
            selectSql = selectSql.Replace("{0}", strdatetime);
            DataSet dsData = CustomsBusiness.GetSql(selectSql);
            RepeaterInfo3.DataSource = dsData.Tables[0];
            RepeaterInfo3.DataBind();
        }

        //更新
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["dtWarningCodetable"] == null || (ViewState["dtWarningCodetable"] as DataTable).Rows.Count == 0)
            {
                return;
            }

            DataTable dtData = ViewState["dtWarningCodetable"] as DataTable;
            foreach (DataRow dr in dtData.Rows)
            {
                string updateSql = @"
                    Update {0} set status=0 where codeId in(
                    Select top {1} codeId From {0} Where codetype='yuan' And businessId={2} and status=1 order by NEWID()
                    )";
                updateSql = updateSql.Replace("{0}", dr["codetable"].ToString());
                updateSql = updateSql.Replace("{1}", dr["shengyurenshu"].ToString());
                updateSql = updateSql.Replace("{2}", dr["yid"].ToString());
                CustomsBusiness.ExecuteSql(updateSql);
            }
            
        }

    }
}