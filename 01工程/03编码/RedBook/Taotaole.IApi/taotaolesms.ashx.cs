using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaolesms 的摘要说明
    /// </summary>
    public class taotaolesms : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "sendcode": //发送短信验证码
                    sendcode(context);
                    break;
                case "validatecode": //验证短信验证码
                    validatecode(context);
                    break;
            }

        }

        /// <summary>
        /// 发送短信验证码
        /// 参数：mobile 用户手机号
        /// 返回状态 state 0代表发送验证码成功 2代表发送验证码失败 3代表发送验证码频繁 4错误的手机号 5手机号在数据库中找不到
        /// </summary>
        /// <param name="context"></param>
        private void sendcode(HttpContext context)
        {
            //验证手机号
            string mobile = context.Request["mobile"];
            if (string.IsNullOrEmpty(mobile) || !PageValidate.IsPhone(mobile))
            {
                context.Response.Write("{\"state\":4}");    //错误的手机号
                return;
            }
            bool isnull = false;
            string type = context.Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                isnull = true;
            }

            //获取用户信息
            DataTable dtmember = go_memberBusiness.GetListData("mobile='" + mobile + "'", " uid,Telcode ", "", 1);
            if (dtmember.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":5}");    //手机号不存在
                return;
            }

            //验证发送验证码时间
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(dtmember.Rows[0]["uid"]));
            if (!string.IsNullOrEmpty(memberEntity.Telcode))
            {
                DateTime timeTelcode = Convert.ToDateTime(memberEntity.Telcode.Split('|')[1]);
                if (ShopSms.Get_Time_Difference(timeTelcode) < ShopSms.Time_Difference_Span)
                {
                    context.Response.Write(string.Format("{{\"state\":{0}}}", 3));  //发送验证码频繁
                    return;
                }
            }
            System.Random r = new Random();
            //string checkcodes = r.Next(100000, 999999).ToString() + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string checkcodes = "8888" + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //发送验证码
            memberEntity.Telcode = checkcodes;
            bool b = go_memberBusiness.SaveEntity(memberEntity, false);
            if (!b)
            {
                context.Response.Write("{\"state\":2}");    //发送验证码失败
                return;
            }
            if (isnull)
            {
                ShopSms.APISendSms(mobile, checkcodes.Split('|')[0], ShopSms.SendSmsType.FindPwd);
            }
            else
            {
                ShopSms.APISendSms(mobile, checkcodes.Split('|')[0], ShopSms.SendSmsType.Reg);
            }
            
            context.Response.Write(string.Format("{{\"state\":{0}}}", 0));  //发送验证码成功

        }

        /// <summary>
        /// 验证验证码
        /// 参数 mobile 用户手机号 code验证码
        /// 返回状态 0代表验证成功 2代表验证失败 3代表验证过时，请重新发送验证码 4代表验证错误 5验证码失效，请重新发送验证码
        /// </summary>
        /// <param name="context"></param>
        private void validatecode(HttpContext context)
        {
            //获取参数
            string mobile = context.Request["mobile"];
            string code = context.Request["code"];

            string type = context.Request["type"];
            if (string.IsNullOrEmpty(type))
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 6));  //验证类型错误
                return;
            }
            //获取用户相关信息
            DataTable dt = go_memberBusiness.GetListData("mobile='" + mobile + "'", "*", null, 1);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));  //2代表验证失败
                return;
            }
            if (dt.Rows[0]["telcode"].ToString().Trim() == "")
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 5));  //验证码失效，请重新发送验证码
                return;
            }
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(dt.Rows[0]["uid"]));
            //判断上次发送验证码时间
            if (ShopSms.Get_Time_Difference(Convert.ToDateTime(dt.Rows[0]["Telcode"].ToString().Split('|')[1])) >= ShopSms.Time_Difference_Val)
            {
                //将用户上次发送验证码时间及验证码清空
                memberEntity.Telcode = "";
                if (go_memberBusiness.SaveEntity(memberEntity, false))
                {
                    context.Response.Write(string.Format("{{\"state\":{0}}}", 3));  //3代表验证过时，请重新发送验证码
                    return;
                }
            }
            if (code != dt.Rows[0]["Telcode"].ToString().Split('|')[0])
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 4));  //4代表验证错误
                return;
            }
            //将用户上次发送验证码时间及验证码清空
            memberEntity.Mobilecode = "1";
            //将用户改为手机验证一成功
            memberEntity.Telcode = "";
            if (go_memberBusiness.SaveEntity(memberEntity, false))
            {
                if (type == "1")
                {
                    context.Response.Write(string.Format("{{\"state\":{0}}}",0));  //验证成功
                    return;
                }
                string sign = SecurityHelper.Encrypt(DbServers.APIKey, memberEntity.Uid.ToString());
                //返回用户JSON
                string result = string.Format("{{\"state\":{0},\r\"sign\":\"{1}\"", 0, sign);
                result += "}";
                context.Response.Write(result);  //0代表验证成功
                return;
            }
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}