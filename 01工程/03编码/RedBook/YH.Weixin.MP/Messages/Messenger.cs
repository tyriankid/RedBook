using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP.Api;
using YH.Weixin.MP.Domain;
using YH.Weixin.MP.QrCode;

namespace YH.Weixin.MP.Messages
{
    public class Messenger
    {
        /// <summary>
        /// 组团失败提醒
        /// </summary>
        public static void SendTuanFail(string openID, string productname, string orderdate, string ordermoney)
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            TemplateMessage templateMessage = new TemplateMessage();
            templateMessage.Url = Globals.WX_Domain + "WxTuan/tuan.mylist.aspx";//单击URL
            templateMessage.TemplateId = Globals.GetMasterSettings(true).WX_Template_03;// "s4EZ4t2PCGozcDZa9kHhOs4TqUMf4_9wiQLSrnlRIM4";//消息模板ID
            templateMessage.Touser = openID;//用户OPENID

            TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
                        new TemplateMessage.MessagePart{Name = "first",Value = "很抱歉，你参与的团购已经过期未达到满团人数，团购失败"},
                        new TemplateMessage.MessagePart{Name = "keyword1",Color = "#FF0000",Value = productname},
                        new TemplateMessage.MessagePart{Name = "keyword2",Value=orderdate},
                        new TemplateMessage.MessagePart{Name = "keyword3",Value=ordermoney},
                        new TemplateMessage.MessagePart{Name = "keyword4",Color = "#FF0000",Value=ordermoney},
                        new TemplateMessage.MessagePart{Name = "remark",Value = "所有费用会原路返回您的账户，请注意查收。"}};
            templateMessage.Data = messateParts;
            TemplateApi.SendMessage(TokenApi.GetToken_Message(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret), templateMessage);
        }

        /// <summary>
        /// 组团成功开奖通知
        /// </summary>
        public static void SendTuanSuccess(string openID, string keyword1, string keyword2, string remark = "")
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            TemplateMessage templateMessage = new TemplateMessage();
            templateMessage.Url = Globals.WX_Domain + "WxTuan/tuan.mylist.aspx";//单击URL
            templateMessage.TemplateId = Globals.GetMasterSettings(true).WX_Template_01;// "b1_ARggaBzbc5owqmwrZ15QPj9Ksfs0p5i64C6MzXKw";//消息模板ID
            templateMessage.Touser = openID;//用户OPENID

            TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
                        new TemplateMessage.MessagePart{Name = "first",Value = "恭喜您参与的团购中奖了！"},
                        new TemplateMessage.MessagePart{Name = "keyword1",Value = keyword1},
                        new TemplateMessage.MessagePart{Name = "keyword2",Color = "#FF0000",Value=keyword2},
                        new TemplateMessage.MessagePart{Name = "remark",Value = remark}};
            templateMessage.Data = messateParts;
            TemplateApi.SendMessage(TokenApi.GetToken_Message(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret), templateMessage);
        }

        /// <summary>
        /// 一元购中奖通知
        /// </summary>
        public static void SendYuanSuccess(string openID, string keyword1, string keyword2, string remark = "")
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            TemplateMessage templateMessage = new TemplateMessage();
            templateMessage.Url = Globals.WX_Domain + "community.index.aspx";//单击URL
            templateMessage.TemplateId = Globals.GetMasterSettings(true).WX_Template_01;// "b1_ARggaBzbc5owqmwrZ15QPj9Ksfs0p5i64C6MzXKw";//消息模板ID
            templateMessage.Touser = openID;//用户OPENID

            TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
                        new TemplateMessage.MessagePart{Name = "first",Value = "恭喜您参与的爽乐购中奖了！"},
                        new TemplateMessage.MessagePart{Name = "keyword1",Value = keyword1},
                        new TemplateMessage.MessagePart{Name = "keyword2",Color = "#FF0000",Value=keyword2},
                        new TemplateMessage.MessagePart{Name = "remark",Value = remark}};
            templateMessage.Data = messateParts;
            TemplateApi.SendMessage(TokenApi.GetToken_Message(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret), templateMessage);
        }

        /// <summary>
        /// 发货成功后的通知
        /// </summary>
        /// <param name="openID">微信OPENID</param>
        /// <param name="keyword1">商品摘要</param>
        /// <param name="keyword2">发货单号</param>
        /// <param name="keyword3">物流商</param>
        /// <param name="keyword3">跳转页</param>
        public static void SendGoodsSuccess(string openID, string keyword1, string keyword2, string keyword3,string url)
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            TemplateMessage templateMessage = new TemplateMessage();
            templateMessage.Url = Globals.WX_Domain + url;// "user.winningconfirm.aspx?shopid=40&orderid=Y160831090808594813&productid=4";//单击URL
            templateMessage.TemplateId = Globals.GetMasterSettings(true).WX_Template_02;// "INRGe6p5FWJkcxl0KuJnHY33iFfGf_HAYKaxUzFjLNY";//消息模板ID
            templateMessage.Touser = openID;//用户OPENID

            TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
                        new TemplateMessage.MessagePart{Name = "first",Value = "您好，您有一个订单已经发货，请及时查看并收货。"},
                        new TemplateMessage.MessagePart{Name = "keyword1",Value = keyword1},
                        new TemplateMessage.MessagePart{Name = "keyword2",Color = "#FF0000",Value=keyword2},
                        new TemplateMessage.MessagePart{Name = "keyword3",Value = keyword3},
                        new TemplateMessage.MessagePart{Name = "remark",Value = "如有问题请致电4008-567-510或直接微信留言，我们第一时间为你解决！"}};
            templateMessage.Data = messateParts;
            TemplateApi.SendMessage(TokenApi.GetToken_Message(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret), templateMessage);
        }


        /// <summary>
        /// 判断用户有没有关注公众号
        /// </summary>
        /// <returns></returns>
        public static bool WxSubscribe(string openid)
        {
            //开启微信才开始判断
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);

            //获取access_token
            string responseResult = GetResponseResult("https://api.weixin.qq.com/cgi-bin/token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&grant_type=client_credential");
            if (responseResult.Contains("access_token"))
            {
                JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
                string wxUserInfoStr = GetResponseResult("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + obj2["access_token"].ToString() + "&openid=" + openid + "&lang=zh_CN");
                if (wxUserInfoStr.Contains("subscribe"))
                {
                    JObject wxUserInfo = JsonConvert.DeserializeObject(wxUserInfoStr) as JObject;
                    if (Convert.ToInt32(wxUserInfo["subscribe"]) != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static string GetResponseResult(string url)
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



    }
}
