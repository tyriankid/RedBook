using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.MP.Domain;
using YH.Weixin.MP.Entity;
using YH.Weixin.MP.Handler;
using YH.Weixin.MP.QrCode;
using YH.Weixin.MP.Request;
using YH.Weixin.MP.Request.Event;
using YH.Weixin.MP.Responses;
using YH.Weixin.MP.Util;
using System.Web.UI;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace YH.Weixin.MP.Messages
{
    public class CustomMsgHandler : RequestHandler
    {

        public CustomMsgHandler(System.IO.Stream inputStream) : base(inputStream) { }

        public CustomMsgHandler(string xml) : base(xml) { }


        public override AbstractResponse DefaultResponse(AbstractRequest requestMessage)
        {

            //获取没有匹配到的回复
            IList<go_wxreplyEntity> replyList = go_wxreplyBusiness.GetListEntity(" isdisable = 0 and replytype = 3");
            go_wxreplyEntity mismatchReply = new go_wxreplyEntity();
            if (replyList.Count > 0)
            {
                mismatchReply = replyList[0];
            }
            //ReplyHelper.GetMismatchReply();
            AbstractResponse result;
            if (mismatchReply == null || this.IsOpenManyService())
            {
                result = this.GotoManyCustomerService(requestMessage);
            }
            else
            {
                AbstractResponse response = this.GetResponse(mismatchReply, requestMessage.FromUserName);
                if (response == null)
                {
                    result = this.GotoManyCustomerService(requestMessage);
                }
                else
                {
                    response.ToUserName = requestMessage.FromUserName;
                    response.FromUserName = requestMessage.ToUserName;
                    result = response;
                }
            }
            return result;
        }


        //private AbstractResponse GetKeyResponse(string key, AbstractRequest request)
        //{
        //    System.Collections.Generic.IList<ReplyInfo> replies = ReplyHelper.GetReplies(ReplyType.Topic);
        //    AbstractResponse result;
        //    if (replies != null && replies.Count > 0)
        //    {
        //        foreach (ReplyInfo info in replies)
        //        {
        //            if (info.Keys == key)
        //            {
        //                TopicInfo topic = VShopHelper.Gettopic(info.ActivityId);
        //                if (topic != null)
        //                {
        //                    NewsResponse response = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article item = new Article
        //                    {
        //                        Description = topic.Title,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, topic.IconUrl),
        //                        Title = topic.Title,
        //                        Url = string.Format("http://{0}/vshop/Topics.aspx?TopicId={1}", System.Web.HttpContext.Current.Request.Url.Host, topic.TopicId)
        //                    };
        //                    response.Articles.Add(item);
        //                    result = response;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list2 = ReplyHelper.GetReplies(ReplyType.Vote);
        //    if (list2 != null && list2.Count > 0)
        //    {
        //        foreach (ReplyInfo info2 in list2)
        //        {
        //            if (info2.Keys == key)
        //            {
        //                VoteInfo voteById = StoreHelper.GetVoteById((long)info2.ActivityId);
        //                if (voteById != null && voteById.IsBackup)
        //                {
        //                    NewsResponse response2 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article2 = new Article
        //                    {
        //                        Description = voteById.VoteName,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, voteById.ImageUrl),
        //                        Title = voteById.VoteName,
        //                        Url = string.Format("http://{0}/vshop/Vote.aspx?voteId={1}", System.Web.HttpContext.Current.Request.Url.Host, voteById.VoteId)
        //                    };
        //                    response2.Articles.Add(article2);
        //                    result = response2;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list3 = ReplyHelper.GetReplies(ReplyType.Wheel);
        //    if (list3 != null && list3.Count > 0)
        //    {
        //        foreach (ReplyInfo info3 in list3)
        //        {
        //            if (info3.Keys == key)
        //            {
        //                LotteryActivityInfo lotteryActivityInfo = VShopHelper.GetLotteryActivityInfo(info3.ActivityId);
        //                if (lotteryActivityInfo != null)
        //                {
        //                    NewsResponse response3 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article3 = new Article
        //                    {
        //                        Description = lotteryActivityInfo.ActivityDesc,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, lotteryActivityInfo.ActivityPic),
        //                        Title = lotteryActivityInfo.ActivityName,
        //                        Url = string.Format("http://{0}/vshop/BigWheel.aspx?activityId={1}", System.Web.HttpContext.Current.Request.Url.Host, lotteryActivityInfo.ActivityId)
        //                    };
        //                    response3.Articles.Add(article3);
        //                    result = response3;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list4 = ReplyHelper.GetReplies(ReplyType.Scratch);
        //    if (list4 != null && list4.Count > 0)
        //    {
        //        foreach (ReplyInfo info4 in list4)
        //        {
        //            if (info4.Keys == key)
        //            {
        //                LotteryActivityInfo info5 = VShopHelper.GetLotteryActivityInfo(info4.ActivityId);
        //                if (info5 != null)
        //                {
        //                    NewsResponse response4 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article4 = new Article
        //                    {
        //                        Description = info5.ActivityDesc,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, info5.ActivityPic),
        //                        Title = info5.ActivityName,
        //                        Url = string.Format("http://{0}/vshop/Scratch.aspx?activityId={1}", System.Web.HttpContext.Current.Request.Url.Host, info5.ActivityId)
        //                    };
        //                    response4.Articles.Add(article4);
        //                    result = response4;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list5 = ReplyHelper.GetReplies(ReplyType.SmashEgg);
        //    if (list5 != null && list5.Count > 0)
        //    {
        //        foreach (ReplyInfo info6 in list5)
        //        {
        //            if (info6.Keys == key)
        //            {
        //                LotteryActivityInfo info7 = VShopHelper.GetLotteryActivityInfo(info6.ActivityId);
        //                if (info7 != null)
        //                {
        //                    NewsResponse response5 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article5 = new Article
        //                    {
        //                        Description = info7.ActivityDesc,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, info7.ActivityPic),
        //                        Title = info7.ActivityName,
        //                        Url = string.Format("http://{0}/vshop/SmashEgg.aspx?activityId={1}", System.Web.HttpContext.Current.Request.Url.Host, info7.ActivityId)
        //                    };
        //                    response5.Articles.Add(article5);
        //                    result = response5;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list6 = ReplyHelper.GetReplies(ReplyType.SignUp);
        //    if (list6 != null && list6.Count > 0)
        //    {
        //        foreach (ReplyInfo info8 in list6)
        //        {
        //            if (info8.Keys == key)
        //            {
        //                ActivityInfo activity = VShopHelper.GetActivity(info8.ActivityId);
        //                if (activity != null)
        //                {
        //                    NewsResponse response6 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article6 = new Article
        //                    {
        //                        Description = activity.Description,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, activity.PicUrl),
        //                        Title = activity.Name,
        //                        Url = string.Format("http://{0}/vshop/Activity.aspx?id={1}", System.Web.HttpContext.Current.Request.Url.Host, activity.ActivityId)
        //                    };
        //                    response6.Articles.Add(article6);
        //                    result = response6;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    System.Collections.Generic.IList<ReplyInfo> list7 = ReplyHelper.GetReplies(ReplyType.Ticket);
        //    if (list7 != null && list7.Count > 0)
        //    {
        //        foreach (ReplyInfo info9 in list7)
        //        {
        //            if (info9.Keys == key)
        //            {
        //                LotteryTicketInfo lotteryTicket = VShopHelper.GetLotteryTicket(info9.ActivityId);
        //                if (lotteryTicket != null)
        //                {
        //                    NewsResponse response7 = new NewsResponse
        //                    {
        //                        CreateTime = System.DateTime.Now,
        //                        FromUserName = request.ToUserName,
        //                        ToUserName = request.FromUserName,
        //                        Articles = new System.Collections.Generic.List<Article>()
        //                    };
        //                    Article article7 = new Article
        //                    {
        //                        Description = lotteryTicket.ActivityDesc,
        //                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, lotteryTicket.ActivityPic),
        //                        Title = lotteryTicket.ActivityName,
        //                        Url = string.Format("http://{0}/vshop/SignUp.aspx?id={1}", System.Web.HttpContext.Current.Request.Url.Host, lotteryTicket.ActivityId)
        //                    };
        //                    response7.Articles.Add(article7);
        //                    result = response7;
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    result = null;
        //    return result;
        //}

        /// <summary>
        /// 根据回复类型生成回复的内容
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public AbstractResponse GetResponse(go_wxreplyEntity reply, string openId)
        {
            AbstractResponse result;
            //1:文本 2:单图文 3:多图文
            if (reply.Messagetype == 1)
            {
                TextResponse response = new TextResponse
                {
                    CreateTime = System.DateTime.Now,
                    Content = reply.Content
                };
                if (reply.Keyword == "tyrian")
                {
                    response.Content = "aaa";//待测试
                }
                result = response;
            }
            else//单图文和多图文内容处理, 多图文包含在循环内
            {
                NewsResponse response2 = new NewsResponse
                {
                    CreateTime = System.DateTime.Now,
                    Articles = new System.Collections.Generic.List<Article>()
                };
                //获取该reply的article(图文内容)
                IList<go_wxreply_articleEntity> articleList = go_wxreply_articleBusiness.GetListEntity(" replyid = " + reply.Replyid);
                foreach (go_wxreply_articleEntity info2 in articleList)
                {
                    Article item = new Article
                    {
                        Description = info2.Description,
                        PicUrl = string.Format("http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Host, info2.Imgurl),
                        Title = info2.Title,
                        Url = info2.Url /*string.IsNullOrEmpty(info2.Url) ? string.Format("http://{0}/Vshop/ImageTextDetails.aspx?messageId={1}", System.Web.HttpContext.Current.Request.Url.Host, info2.Articleid) : */
                    };
                    response2.Articles.Add(item);
                }
                result = response2;
            }
            return result;
        }

        public AbstractResponse GotoManyCustomerService(AbstractRequest requestMessage)
        {
            AbstractResponse result;
            if (!this.IsOpenManyService())
            {
                result = null;
            }
            else
            {
                result = new AbstractResponse
                {
                    FromUserName = requestMessage.ToUserName,
                    ToUserName = requestMessage.FromUserName,
                    MsgType = ResponseMsgType.transfer_customer_service
                };
            }
            return result;
        }

        public bool IsOpenManyService()
        {
            return Globals.GetMasterSettings(false).IsOpenManyService;
        }

        //public override AbstractResponse OnEvent_ClickRequest(ClickEventRequest clickEventRequest)
        //{
        //    MenuInfo menu = VShopHelper.GetMenu(System.Convert.ToInt32(clickEventRequest.EventKey));
        //    AbstractResponse result;
        //    if (menu == null)
        //    {
        //        result = null;
        //    }
        //    else
        //    {
        //        ReplyInfo reply = ReplyHelper.GetReply(menu.ReplyId);
        //        if (reply == null)
        //        {
        //            result = null;
        //        }
        //        else
        //        {
        //            AbstractResponse keyResponse = this.GetKeyResponse(reply.Keys, clickEventRequest);
        //            if (keyResponse != null)
        //            {
        //                result = keyResponse;
        //            }
        //            else
        //            {
        //                AbstractResponse response = this.GetResponse(reply, clickEventRequest.FromUserName);
        //                if (response == null)
        //                {
        //                    this.GotoManyCustomerService(clickEventRequest);
        //                }
        //                response.ToUserName = clickEventRequest.FromUserName;
        //                response.FromUserName = clickEventRequest.ToUserName;
        //                result = response;
        //            }
        //        }
        //    }
        //    return result;
        //}

        //关注事件
        public override AbstractResponse OnEvent_SubscribeRequest(SubscribeEventRequest subscribeEventRequest)
        {
            //清除Cookies
            System.Web.HttpCookie cookies = new System.Web.HttpCookie(Globals.TaotaoleMemberKey)
            {
                Value = "-1",
                Expires = System.DateTime.Now.AddHours(-1)
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookies);


            //关注注册
            ScanVisitDistributor(subscribeEventRequest, subscribeEventRequest.EventKey);

            //关注回复
            //ReplyInfo subscribeReply = ReplyHelper.GetSubscribeReply();
            //关注时回复
            IList<go_wxreplyEntity> replyList = go_wxreplyBusiness.GetListEntity(" isdisable = 0 and replytype=2");
            go_wxreplyEntity subscribeReply = new go_wxreplyEntity();
            if (replyList.Count > 0)
            {
                subscribeReply = replyList[0];
            }
            AbstractResponse result;
            if (subscribeReply == null)
            {
                result = null;
            }
            else
            {
                subscribeReply.Keyword = "登录";
                AbstractResponse response = this.GetResponse(subscribeReply, subscribeEventRequest.FromUserName);
                if (response == null)
                {
                    this.GotoManyCustomerService(subscribeEventRequest);
                }
                response.ToUserName = subscribeEventRequest.FromUserName;
                response.FromUserName = subscribeEventRequest.ToUserName;
                result = response;
            }
            return result;
        }
        /// <summary>
        /// 文字消息回复
        /// </summary>
        public override AbstractResponse OnTextRequest(TextRequest textRequest)
        {
            //获取特殊匹配
            //AbstractResponse keyResponse = this.GetKeyResponse(textRequest.Content, textRequest);
            AbstractResponse result;
            //如果特殊匹配存在值,返回特殊匹配的规则,否则用一般内容返回
            //if (keyResponse != null)
            //{
            //    result = keyResponse; 
            //    return result;
            //}


            //获取回复内容
            IList<go_wxreplyEntity> replies = go_wxreplyBusiness.GetListEntity(" isdisable = 0");
            //如果reply表为空,或者开通了多客服服务,则跳转到多客服回复模块内
            if (replies == null || (replies.Count == 0 && this.IsOpenManyService()))
            {
                this.GotoManyCustomerService(textRequest);
            }
            foreach (go_wxreplyEntity info in replies)
            {
                //精确匹配
                if (info.Matchtype == 1 && info.Keyword == textRequest.Content)
                {
                    AbstractResponse response = this.GetResponse(info, textRequest.FromUserName);
                    response.ToUserName = textRequest.FromUserName;
                    response.FromUserName = textRequest.ToUserName;
                    result = response;
                    return result;
                }
                //模糊匹配
                if (info.Matchtype == 2 && info.Keyword.Contains(textRequest.Content))
                {
                    AbstractResponse response2 = this.GetResponse(info, textRequest.FromUserName);
                    response2.ToUserName = textRequest.FromUserName;
                    response2.FromUserName = textRequest.ToUserName;
                    result = response2;
                    return result;
                }
            }
            //无匹配
            result = this.DefaultResponse(textRequest);

            return result;
        }

        /// <summary>
        /// 增加扫码事件处理,add by jhb,20151213
        /// </summary>
        /// <param name="scanEventRequest"></param>
        /// <returns></returns>
        public override AbstractResponse OnEvent_ScanRequest(ScanEventRequest scanEventRequest)
        {
            ScanVisitDistributor(scanEventRequest, scanEventRequest.EventKey);
            return null;
        }
        public override AbstractResponse OnEvent_ViewRequest(ViewEventRequest viewEventRequest)
        {
            ScanVisitDistributor(viewEventRequest, viewEventRequest.EventKey);
            return null;
        }


        /// <summary>
        /// 关注访问处理  ()
        /// </summary>
        private void ScanVisitDistributor(EventRequest eventRequest, string strEventKey)
        {
            string[] arrayEventKey = strEventKey.Split('_');
            string openid = eventRequest.FromUserName;
            string channelid = arrayEventKey[arrayEventKey.Length - 1];
            if (!string.IsNullOrEmpty(strEventKey) && strEventKey.Split('_').Length > 1)
            {

                switch (arrayEventKey[arrayEventKey.Length - 2].ToLower())
                {
                    case "distributor":
                        string VisitDistributorID = HiCache.Get(string.Format("DataCache-VisitDistributor-{0}", arrayEventKey[arrayEventKey.Length - 1])) as string;
                        if (string.IsNullOrEmpty(VisitDistributorID))
                        {
                            HiCache.Remove(string.Format("DataCache-VisitDistributor-{0}", eventRequest.FromUserName));
                            HiCache.Insert(string.Format("DataCache-VisitDistributor-{0}", eventRequest.FromUserName), arrayEventKey[arrayEventKey.Length - 1], 360
                                , System.Web.Caching.CacheItemPriority.Normal);
                        }
                        break;
                    case "channel":
                        WriteLog(channelid);
                        wxReister(openid, channelid);
                        break;
                    default:
                        WriteLog(channelid);
                        wxReister(openid, "");
                        break;
                }
            }
            else
            {
                wxReister(openid, "");
            }
        }

        private string GetResponseResult(string url)
        {
            using (HttpWebResponse response = (HttpWebResponse)WebRequest.Create(url).GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        /// <summary>
        /// 微信注册
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="uid"></param>
        private void wxReister(string openid, string uid)
        {
            SiteSettings masterSettings = Globals.GetMasterSettings();
            //从微信拉取用户信息
            string tokenurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            string responseResult = this.GetResponseResult(string.Format(tokenurl, masterSettings.WeixinAppId, masterSettings.WeixinAppSecret));
            JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
            IList<go_memberEntity> listmemberEntity = go_memberBusiness.GetListEntity(string.Format("wxid='{0}'", openid));

            if (listmemberEntity.Count == 0)
            {
                //返回微信用户信息
                int channel_type = 0;//推广人类型(0渠道商 1城市服务商)
                string userInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
                string wxUserInfoStr = this.GetResponseResult(string.Format(userInfoUrl, obj2["access_token"].ToString(), openid));
                #region 微信注册用户
                JObject wxUserInfo = JsonConvert.DeserializeObject(wxUserInfoStr) as JObject;
                string unionId = (obj2["unionid"] == null) ? "" : obj2["unionid"].ToString(); //微信用户UNIONID
                string where = string.IsNullOrEmpty(unionId) ? string.Format("wxid='{0}'", openid) : string.Format("unionid='{0}'", unionId);
                listmemberEntity = go_memberBusiness.GetListEntity(where);
                if (listmemberEntity.Count == 0)
                {
                    go_memberEntity member = new go_memberEntity();
                    member.Username = Globals.UrlDecode(wxUserInfo["nickname"].ToString());
                    member.Wxid = openid; //用户微信ID
                    member.Typeid = 0;//微信注册
                    member.Time = DateTime.Now;
                    member.Email = "";
                    member.Password = SecurityHelper.GetMd5To32(Globals.GetGenerateId());
                    member.Headimg = wxUserInfo["headimgurl"] == null ? "" : wxUserInfo["headimgurl"].ToString();//用户头像
                    member.Unionid = wxUserInfo["unionid"] ==null ? "" : wxUserInfo["unionid"].ToString();
                    member.Servicechannelid = 0;
                    //member.Money = 200000;
                    if (!string.IsNullOrEmpty(uid))
                    {
                        DataTable dtchannel = go_channel_userBusiness.GetListData(string.Format("uid ='{0}'", uid));
                        if (dtchannel.Rows.Count > 0)
                            member.Servicecityid = int.Parse(dtchannel.Rows[0]["parentid"].ToString());
                        member.Servicechannelid = int.Parse(uid);
                    }
                    else
                    {
                        //更改城市服务商
                        string city = (wxUserInfo["city"] == null ? "" : wxUserInfo["city"].ToString().Replace("市", ""));
                        if (city != "")
                        {
                            object oServicecityid = go_channel_userBusiness.GetScalar(string.Format("parentid=0 And cityid='{0}'", city));
                            if (oServicecityid != null) member.Servicecityid = int.Parse(oServicecityid.ToString());
                            uid = oServicecityid.ToString();
                            channel_type = 1;
                        }
                        /*DataTable dtUser = go_channel_userBusiness.GetListData("parentid=0", "cityid,uid");
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            DataRow[] rows = dtUser.Select(string.Format("cityid='{0}'", city));
                            if (rows.Length > 0) member.Servicecityid = Convert.ToInt32(rows[0]["uid"]);
                        }*/
                    }
                    if (go_memberBusiness.SaveEntity(member, true))
                    {
                        go_channel_userBusiness.AddUsercount((string.IsNullOrEmpty(uid) ? 0 : int.Parse(uid)), channel_type);//更改对应渠道商推广用户数
                        HttpCookie cookie = new HttpCookie(Globals.TaotaoleMemberKey);
                        string Uid = go_memberBusiness.GetScalar(string.Format(" wxid = '{0}'", openid), "uid").ToString();
                        cookie.Value = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, Uid);
                        cookie.Expires = DateTime.Now.AddYears(10);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                #endregion
                }
            }
        }



        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_login.txt"));

            writer.WriteLine(System.DateTime.Now);

            writer.WriteLine(log);

            writer.Flush();

            writer.Close();

        }
    }
}
