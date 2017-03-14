using Newtonsoft.Json;
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
    public partial class addMultiArticle : Base

    {

        public int replyid = 0;
        public bool needsToHideKeyword = false;//用于前端载入时隐藏关键字区域
        public bool isMismatchExist = false;//用于前端判断是否已经存在无匹配回复
        public bool isSubscribeReplyExist = false;//用于前端判断是否已经存在关注时回复
        public int replytype = 1;//默认回复类型:1关键字回复
        public int matchtype = 1;//默认匹配类型:1精确匹配
        public int isdisable = 0;//默认是否失效:0:否
        public string keyword = "";//关键字
        public int sboxCount = 0;//多文本的数量,用于载入时前端进行循环加载


        protected string articleJson = "{\"aa\":\"aa\"}";


        protected void Page_Load(object sender, EventArgs e)
        {
            replyid = Convert.ToInt32(Request.QueryString["replyid"]);
            if (!this.IsPostBack)
            {
                isMismatchExist = go_wxreplyBusiness.isMismatchReplyExist(replyid);
                isSubscribeReplyExist = go_wxreplyBusiness.isSubscribeReplyExist(replyid);
            }
            //添加和编辑的无刷新方法
            string cmd = base.Request.QueryString["cmd"];
            if (!string.IsNullOrEmpty(cmd) && !string.IsNullOrEmpty(base.Request.Form["MultiArticle"]))
            {
                switch (cmd)
                {
                    #region 新增
                    case "add":
                        string str = base.Request.Form["MultiArticle"];
                        System.Collections.Generic.List<ArticleList> list = JsonConvert.DeserializeObject<System.Collections.Generic.List<ArticleList>>(str);
                        if (list != null && list.Count > 0)
                        {
                            NewsReplyInfo reply = new NewsReplyInfo
                            {
                                Messagetype = 3,//多图文
                                Isdisable = Convert.ToInt32(base.Request.Form["Isdisable"]),
                                Matchtype = Convert.ToInt32(base.Request.Form["Matchtype"]),
                                Replytype = Convert.ToInt32(base.Request.Form["Replytype"]),
                            };
                            reply.Keyword = reply.Replytype == 1 ? base.Request.Form["Keyword"] : "";//如果是关键字匹配,才会把关键字存入,否则存空
                            //判断关键字是否重复
                            if (reply.Replytype == 1 && !string.IsNullOrWhiteSpace(reply.Keyword) && go_wxreplyBusiness.HasReplyKeyword(reply.Keyword))
                            {
                                base.Response.Write("key");
                                base.Response.End();
                                return;
                            }

                            System.Collections.Generic.List<NewsMsgInfo> list2 = new System.Collections.Generic.List<NewsMsgInfo>();
                            foreach (ArticleList list3 in list)
                            {
                                if (list3.Status != "del")
                                {
                                    NewsMsgInfo item = list3;
                                    if (item != null)
                                    {
                                        item.Reply = reply;
                                        list2.Add(item);
                                    }
                                }
                            }
                            reply.NewsMsg = list2;
                            if (go_wxreplyBusiness.SaveReplyInfo(reply))
                            {
                                base.Response.Write("true");
                                base.Response.End();
                            }
                        }
                        break;
                        #endregion
                    #region 编辑
                    case "edit":
                        replyid = Convert.ToInt32(Request.QueryString["replyid"]);
                        //首先载入实体类
                        IList<go_wxreply_articleEntity> articleEntityList = go_wxreply_articleBusiness.GetListEntity(" replyid = " + replyid);
                        go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
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
                        newsEntity.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
                        foreach (go_wxreply_articleEntity article in articleEntityList)
                        {
                            NewsMsgInfo item = new NewsMsgInfo
                            {
                                Reply = newsEntity,
                                Content = article.Content,
                                Description = article.Description,
                                Imgurl = article.Imgurl,
                                Title = article.Title,
                                Url = article.Url
                            };
                            newsEntity.NewsMsg.Add(item);
                        }



                        string strEdit = base.Request.Form["MultiArticle"];
                        System.Collections.Generic.List<ArticleList> listEdit = JsonConvert.DeserializeObject<System.Collections.Generic.List<ArticleList>>(strEdit);
                        if (listEdit != null && listEdit.Count > 0)
                        {
                            
                            newsEntity.Isdisable = Convert.ToInt32(base.Request.Form["Isdisable"]);
                            newsEntity.Matchtype = Convert.ToInt32(base.Request.Form["Matchtype"]);
                            newsEntity.Replytype = Convert.ToInt32(base.Request.Form["Replytype"]);
                            newsEntity.Keyword = newsEntity.Replytype == 1 ? base.Request.Form["Keyword"] : "";//如果是关键字匹配,才会把关键字存入,否则存空
                            //判断关键字是否重复
                            if (newsEntity.Replytype == 1 && !string.IsNullOrWhiteSpace(newsEntity.Keyword) && go_wxreplyBusiness.HasReplyKeyword(newsEntity.Keyword))
                            {
                                base.Response.Write("key");
                                base.Response.End();
                                return;
                            }

                            System.Collections.Generic.List<NewsMsgInfo> listArticle = new System.Collections.Generic.List<NewsMsgInfo>();
                            foreach (ArticleList article in listEdit)
                            {
                                if (article.Status != "del")
                                {
                                    NewsMsgInfo item = article;
                                    if (item != null)
                                    {
                                        item.Reply = newsEntity;
                                        listArticle.Add(item);
                                    }
                                }
                            }
                            newsEntity.NewsMsg = listArticle;
                            if (go_wxreplyBusiness.UpdateReplyInfo(newsEntity))
                            {
                                base.Response.Write("true");
                                base.Response.End();
                            }
                        }
                        break;
                    #endregion
                }
            }
            else//编辑模式的载入方法
            {
                #region 编辑模式载入
                if (replyid == 0) return;
                //首先载入实体类
                IList<go_wxreply_articleEntity> articleEntityList = go_wxreply_articleBusiness.GetListEntity(" replyid = " + replyid);
                go_wxreplyEntity replyEntity = go_wxreplyBusiness.LoadEntity(replyid);
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
                newsEntity.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
                foreach (go_wxreply_articleEntity article in articleEntityList)
                {
                    NewsMsgInfo item = new NewsMsgInfo
                    {
                        Reply = newsEntity,
                        Content = article.Content,
                        Description = article.Description,
                        Imgurl = article.Imgurl,
                        Title = article.Title,
                        Url = article.Url
                    };
                    newsEntity.NewsMsg.Add(item);
                }

                List<ArticleList> articleList = new List<ArticleList>();
                if (newsEntity.NewsMsg != null && newsEntity.NewsMsg.Count > 0)
                {
                    int num = 1; sboxCount = newsEntity.NewsMsg.Count;
                    foreach (NewsMsgInfo infoArticle in newsEntity.NewsMsg)
                    {
                        ArticleList list5 = new ArticleList
                        {
                            Imgurl = infoArticle.Imgurl,
                            Title = infoArticle.Title,
                            Url = infoArticle.Url,
                            Description = infoArticle.Description,
                            Content = infoArticle.Content
                        };
                        list5.BoxId = num++.ToString();
                        list5.Status = "";
                        articleList.Add(list5);
                    }
                    this.articleJson = JsonConvert.SerializeObject(articleList);
                }

                //获取单选框的值
                this.replytype = newsEntity.Replytype;
                this.matchtype = newsEntity.Matchtype;
                this.isdisable = newsEntity.Isdisable;
                this.keyword = newsEntity.Keyword;
                #endregion
            }
        }



    }
}