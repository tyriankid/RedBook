using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.wx
{
    public partial class addReplyOnKey : Base
    {
        public int replyid = 0;
        public bool needsToHideKeyword = false;//用于前端载入时隐藏关键字区域
        public bool isMismatchExist = false;//用于前端判断是否已经存在无匹配回复
        public bool isSubscribeReplyExist = false;//用于前端判断是否已经存在关注时回复

        protected void Page_Load(object sender, EventArgs e)
        {
            replyid = Convert.ToInt32(Request.QueryString["replyid"]);
            if (!this.IsPostBack)
            {
                isMismatchExist = go_wxreplyBusiness.isMismatchReplyExist(replyid);//如果存在的id等于自己,则无需屏蔽
                isSubscribeReplyExist = go_wxreplyBusiness.isSubscribeReplyExist(replyid);//如果存在的id等于自己,则无需屏蔽
                rdoIsdisable.SelectedIndex = 0; 
                rdoMatchtype.SelectedIndex = 0; 
                rdoReplytype.SelectedIndex = 0;

                if (replyid > 0) load();//编辑
            }
        }

        protected void load()
        {
            go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
            txtContent.Text = replyEntity.Content;
            txtKeyword.Value = replyEntity.Keyword;
            needsToHideKeyword = replyEntity.Replytype != 1  ? true:false;
            rdoReplytype.Items.FindByValue(replyEntity.Replytype.ToString()).Selected = true;
            rdoMatchtype.Items.FindByValue(replyEntity.Matchtype.ToString()).Selected = true;
            rdoIsdisable.Items.FindByValue(replyEntity.Isdisable.ToString()).Selected = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //如果是关键字回复并且没有填写关键字,给出提示
            if (string.IsNullOrWhiteSpace(txtKeyword.Value) && rdoReplytype.SelectedValue == "1")
            {
                base.Response.Write("<script>alert('请填写关键字！');</script>"); return;
            }



            if (replyid <= 0)//添加
            {

                go_wxreplyEntity replyEntity = new go_wxreplyEntity()
                {
                    Keyword = txtKeyword.Value.Trim(),
                    Matchtype = Convert.ToInt32(rdoMatchtype.SelectedValue),
                    Replytype = Convert.ToInt32(rdoReplytype.SelectedValue),
                    Messagetype = 1,
                    Isdisable = Convert.ToInt32(rdoIsdisable.SelectedValue),
                    Content = txtContent.Text.Trim(),
                };

                //判断关键字是否重复
                if (replyEntity.Replytype == 1 && go_wxreplyBusiness.HasReplyKeyword(txtKeyword.Value))
                {
                    base.Response.Write("<script>alert('关键字重复！');</script>"); return;
                }

                if (go_wxreplyBusiness.SaveEntity(replyEntity, true))
                {
                    base.Response.Write("<script>alert('添加成功！');location.href='wxreply.aspx'</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('添加失败！');</script>");
                }
            }
            else//编辑
            {
                go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
                replyEntity.Keyword = txtKeyword.Value.Trim();
                replyEntity.Matchtype = Convert.ToInt32(rdoMatchtype.SelectedValue);
                replyEntity.Replytype = Convert.ToInt32(rdoReplytype.SelectedValue);
                replyEntity.Isdisable = Convert.ToInt32(rdoIsdisable.SelectedValue);
                replyEntity.Content = txtContent.Text.Trim();

                //判断关键字是否重复
                if (replyEntity.Replytype == 1 && go_wxreplyBusiness.HasReplyKeyword(txtKeyword.Value))
                {
                    base.Response.Write("<script>alert('关键字重复！');</script>"); return;
                }

                if (go_wxreplyBusiness.SaveEntity(replyEntity, false))
                {
                    base.Response.Write("<script>alert('编辑成功！');location.href='wxreply.aspx'</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('编辑失败！');</script>");
                }
            }


        }
    }
}