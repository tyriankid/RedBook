using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP.Entity;

namespace RedBookPlatform.wx
{
    public partial class addSingleArticle : Base
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
            else
            {
                this.uploadpic.Src = this.hdpic.Value;
            }

            #region 上传图片
            if (!string.IsNullOrEmpty(base.Request.QueryString["iscallback"]) && System.Convert.ToBoolean(base.Request.QueryString["iscallback"]))
            {
                this.UploadImage();
            }
            else
            {
                if (!string.IsNullOrEmpty(base.Request.Form["del"]))
                {
                    string path = base.Request.Form["del"];
                    string str2 = Server.MapPath(Globals.UPLOAD_PATH);
                    try
                    {
                        if (System.IO.File.Exists(str2))
                        {
                            System.IO.File.Delete(str2);
                            base.Response.Write("true");
                        }
                    }
                    catch (System.Exception)
                    {
                        base.Response.Write("false");
                    }
                    base.Response.End();
                }
            }
            #endregion
        }


        protected void load()
        {
            //首先载入实体类
            IList<go_wxreply_articleEntity> articleEntityList = go_wxreply_articleBusiness.GetListEntity(" replyid = " + replyid);
            go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
            go_wxreply_articleEntity articleEntity = new go_wxreply_articleEntity();
            if (articleEntityList.Count > 0) articleEntity = articleEntityList[0];
            //根据编辑的内容和读取的内容新建NewsReplyInfo和NewsMsgInfo
            NewsReplyInfo newsEntity = new NewsReplyInfo
            {
                Replyid = replyEntity.Replyid,
                Keyword = replyEntity.Keyword,
                Matchtype = replyEntity.Matchtype,
                Replytype = replyEntity.Replytype,
                Messagetype = replyEntity.Messagetype,
                Isdisable = replyEntity.Isdisable,
            };
            NewsMsgInfo item = new NewsMsgInfo
            {
                Reply = newsEntity,
                Content = articleEntity.Content,
                Description = articleEntity.Description,
                Imgurl = articleEntity.Imgurl,
                Title = articleEntity.Title,
                Url = articleEntity.Url
            };
            newsEntity.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
            newsEntity.NewsMsg.Add(item);

            txtKeyword.Value = replyEntity.Keyword;
            needsToHideKeyword = replyEntity.Replytype != 1 ? true : false;
            rdoReplytype.Items.FindByValue(replyEntity.Replytype.ToString()).Selected = true;
            rdoMatchtype.Items.FindByValue(replyEntity.Matchtype.ToString()).Selected = true;
            rdoIsdisable.Items.FindByValue(replyEntity.Isdisable.ToString()).Selected = true;

            this.Tbtitle.Text = newsEntity.NewsMsg[0].Title;
            this.LbimgTitle.Text = newsEntity.NewsMsg[0].Title;
            this.Tbdescription.Text = System.Web.HttpUtility.HtmlDecode(newsEntity.NewsMsg[0].Description);
            this.content.Value = newsEntity.NewsMsg[0].Content;
            this.Lbmsgdesc.Text = newsEntity.NewsMsg[0].Description;
            this.TbUrl.Text = newsEntity.NewsMsg[0].Url;
            this.uploadpic.Src = newsEntity.NewsMsg[0].Imgurl;
            this.hdpic.Value = newsEntity.NewsMsg[0].Imgurl;
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

                NewsReplyInfo replyEntity = new NewsReplyInfo
                {
                    Keyword = txtKeyword.Value.Trim(),
                    Matchtype = Convert.ToInt32(rdoMatchtype.SelectedValue),
                    Replytype = Convert.ToInt32(rdoReplytype.SelectedValue),
                    Messagetype = 2,
                    Isdisable = Convert.ToInt32(rdoIsdisable.SelectedValue),
                };


                NewsMsgInfo item = new NewsMsgInfo
                {
                    Reply = replyEntity,
                    Content = this.content.Value,
                    Description = System.Web.HttpUtility.HtmlEncode(this.Tbdescription.Text),
                    Imgurl = this.hdpic.Value,
                    Title = System.Web.HttpUtility.HtmlEncode(this.Tbtitle.Text),
                    Url = this.TbUrl.Text.Trim()
                };
                replyEntity.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
                replyEntity.NewsMsg.Add(item);

                //判断关键字是否重复
                if (replyEntity.Replytype == 1 && go_wxreplyBusiness.HasReplyKeyword(txtKeyword.Value))
                {
                    base.Response.Write("<script>alert('关键字重复！');</script>"); return;
                }


                if (go_wxreplyBusiness.SaveReplyInfo(replyEntity))
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
                //首先载入实体类
                IList<go_wxreply_articleEntity> articleEntityList = go_wxreply_articleBusiness.GetListEntity(" replyid = " + replyid);
                go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
                go_wxreply_articleEntity articleEntity = new go_wxreply_articleEntity();
                if (articleEntityList.Count > 0) articleEntity = articleEntityList[0];
                //根据编辑的内容和读取的内容新建NewsReplyInfo和NewsMsgInfo
                NewsReplyInfo newsEntity = new NewsReplyInfo
                {
                    Replyid = replyid,
                    Keyword = txtKeyword.Value.Trim(),
                    Matchtype = Convert.ToInt32(rdoMatchtype.SelectedValue),
                    Replytype = Convert.ToInt32(rdoReplytype.SelectedValue),
                    Messagetype = 2,
                    Isdisable = Convert.ToInt32(rdoIsdisable.SelectedValue),
                };
                NewsMsgInfo item = new NewsMsgInfo
                {
                    Articleid = articleEntity.Articleid,
                    Reply = newsEntity,
                    Content = this.content.Value,
                    Description = System.Web.HttpUtility.HtmlEncode(this.Tbdescription.Text),
                    Imgurl = this.hdpic.Value,
                    Title = System.Web.HttpUtility.HtmlEncode(this.Tbtitle.Text),
                    Url = this.TbUrl.Text.Trim()
                };
                newsEntity.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
                newsEntity.NewsMsg.Add(item);


                //判断关键字是否重复
                if (newsEntity.Replytype == 1 && go_wxreplyBusiness.HasReplyKeyword(txtKeyword.Value))
                {
                    base.Response.Write("<script>alert('关键字重复！');</script>"); return;
                }

                if (go_wxreplyBusiness.UpdateReplyInfo(newsEntity))
                {
                    base.Response.Write("<script>alert('编辑成功！');location.href='wxreply.aspx'</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('编辑失败！');</script>");
                }
            }

        }

        private void UploadImage()
        {
            try
            {
                System.Web.HttpPostedFile file = base.Request.Files["Filedata"];
                string str = System.DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string str2 = "/Resources/uploads/wxarticle/";
                string str3 = str + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath(str2 + str3));
                base.Response.StatusCode = 200;
                base.Response.Write(str2 + str3);
            }
            catch (System.Exception)
            {
                base.Response.StatusCode = 500;
                base.Response.Write("服务器错误");
                base.Response.End();
            }
            finally
            {
                base.Response.End();
            }
        }

    }
}