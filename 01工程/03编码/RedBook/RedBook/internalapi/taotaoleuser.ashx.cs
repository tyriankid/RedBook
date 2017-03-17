using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.MP.Messages;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaoleuser 的摘要说明
    /// </summary>
    public class taotaoleuser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "login":               //登录
                    login(context);
                    break;
                case "checkmobile":         //注册时检查手机号是否被注册
                    checkmobile(context);
                    break;
                case "registeruser":        //用户注册
                    registeruser(context);
                    break;
                case "updatepass":          //更改密码
                    updatepass(context);
                    break;
                case "visituser":           //查看某用户
                    visituser(context);
                    break;
                case "useraddresslist":          //获取收货地址列表
                    useraddresslist(context);
                    break;
                case "deleteuseraddress":          //删除收货地址
                    deleteuseraddress(context);
                    break;
                case "defauleuseraddress":          //设置默认收货地址
                    defauleuseraddress(context);
                    break;
                case "adduseraddress":          //添加收货地址
                    adduseraddress(context);
                    break;
                case "addressidlist":          //根据地址ID查询地址信息
                    addressidlist(context);
                    break;

                case "usergameaddresslist":          //获取游戏收货地址列表
                    usergameaddresslist(context);
                    break;
                case "deleteusergameaddress":          //删除游戏收货地址
                    deleteusergameaddress(context);
                    break;
                case "defauleusergameaddress":          //设置默认收货地址(游戏)
                    defauleusergameaddress(context);
                    break;
                case "addusergameaddress":          //添加游戏收货地址
                    addusergameaddress(context);
                    break;
                case "gameaddressidlist":          //根据游戏地址ID查询游戏地址信息
                    gameaddressidlist(context);
                    break;
                case "updateuserinfo":      //更改用户信息 ，不包括头像
                    updateuserinfo(context);
                    break;
                case "updateuseripcity":    //保存当前用户COOKIE，并更新城市服务商
                    updateuseripcity(context);
                    break;
                case "getsubscribe":        //获取当前登录用户
                    GetSubscribe(context);
                    break;
                case "countingo"://今日统计信息
                    countingo(context);
                    break;
                case "activityMember":               // 点击图文送红包
                    activityMember(context);
                    break;
            }
        }

        /// <summary>
        /// 点击图文送红包
        /// </summary>
        /// <param name="context"></param>
        private void activityMember(HttpContext context)
        {
            WxLogger("开始执行接口");
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            WxLogger("已经验证了签名：" + sign);
            string uid = Globals.GetCurrentMemberUserIdSign();
            WxLogger("从Cookie中得到用户ID：" + uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=activityMember&sign={0}&uid={1}", sign, uid));
            WxLogger("从接口中获取的返回值内容：" + result);
            context.Response.Write(result);
        }


        bool isWxLogger = true;
        /// <summary>
        /// 微信日志
        /// </summary>
        /// <param name="log"></param>
        void WxLogger(string log)
        {
            if (!isWxLogger) return;
            string logFile = HttpContext.Current.Request.MapPath("~/wx_login.txt");
            File.AppendAllText(logFile, string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log));
        }

        /// <summary>
        /// 用户登录。
        /// 参数1：sign @访问签名
        /// 参数2：username @用户手机号/邮箱
        /// 参数3：password @用户密码。
        /// 返回值 state: 0验证通过 1帐号不存在 2帐号或密码错误 3未通过验证。
        /// </summary>
        private void login(HttpContext context)
        {

            string mobile = context.Request["mobile"];
            string password64 = context.Request["password"];

            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=login&mobile={0}&password={1}", mobile, password64));
            go_memberEntity memberEntity = JsonConvert.DeserializeObject<go_memberEntity>(result);
            //设置cookie
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("TTL_Member")
            {
                Value = SecurityHelper.Encrypt("uid", memberEntity.Uid.ToString()),
                Expires = System.DateTime.Now.AddDays(7.0),
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            System.Web.HttpCookie cookie_sign = new System.Web.HttpCookie("sign")
            {
                //Value = SecurityHelper.Encrypt("sign", memberEntity.cookiesign),
                //Expires = System.DateTime.Now.AddDays(7.0),
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie_sign);
            context.Response.Write(result);

        }
        /// <summary>
        /// 检查手机号是否已经被注册
        /// </summary>
        /// <param name="context"></param>
        private void checkmobile(HttpContext context)
        {
            string mobile = context.Request["mobile"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=checkmobile&mobile={0}", mobile));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户注册
        /// 参数：mobile 手机号  password 密码（加密过后的）
        /// 返回状态 state 0代表注册成功  2代表注册失败
        /// </summary>
        /// <param name="context"></param>
        private void registeruser(HttpContext context)
        {
            string mobile = context.Request["mobile"];
            string password64 = context.Request["password"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=registeruser&mobile={0}&password={1}", mobile,password64));
            context.Response.Write(result);
        }

        /// <summary>
        /// 发送短信验证码
        /// 参数：mobile 用户手机号
        /// 返回状态 state 0代表发送验证码成功 2代表发送验证码失败 3代表发送验证码频繁
        /// </summary>
        /// <param name="context"></param>
        private void sendcode(HttpContext context)
        {
            string mobile = context.Request["mobile"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=sendcode&mobile={0}", mobile));
            context.Response.Write(result);
        }

        /// <summary>
        /// 更新密码
        /// 传参 pass：原密码 或者 code：短信验证码 passnew：新密码
        /// 返回状态 state  0代更新成功 //1代表签名验证错误 //2代更新失败  //3代表验证过时，请重新发送验证码 //4代表验证错误
        /// </summary>
        /// <param name="context"></param>
        private void updatepass(HttpContext context)
        {
            string pass = context.Request["pass"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string code = context.Request["code"];
            string passnew = context.Request["passnew"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=updatepass&passnew={0}&pass={1}&code={2}&sign={3}&uid={4}", passnew, pass, code, sign, uid));
            context.Response.Write(result);
        }
        private void visituser(HttpContext context)
        {
            //签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string otherid = context.Request["otherid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=visituser&sign={0}&uid={1}&otherid={2}", sign, uid, otherid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 验证验证码
        /// 参数 mobile 用户手机号 code验证码
        /// 返回状态 0代表验证成功 2代表验证失败 3代表验证过时，请重新发送验证码 4代表验证错误
        /// </summary>
        /// <param name="context"></param>
        private void validatecode(HttpContext context)
        {
            //获取参数
            string mobile = context.Request["mobile"];
            string code = context.Request["code"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=validatecode&mobile={0}&code={1}", mobile, code));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户地址列表
        /// </summary>
        /// <param name="context"></param>
        private void useraddresslist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=useraddresslist&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        private void deleteuseraddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string addressid = context.Request["addressid"];
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=deleteuseraddress&sign={0}&addressid={1}&uid={2}", sign, addressid, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 设为默认地址
        /// </summary>
        private void defauleuseraddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string addressid = context.Request["addressid"];

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=defauleuseraddress&sign={0}&addressid={1}&uid={2}", sign, addressid,uid));
            context.Response.Write(result);
        }
        //添加用户地址
        private void adduseraddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string sheng = context.Request["sheng"];
            string shi = context.Request["shi"];
            string xian = context.Request["xian"];
            string jiedao = context.Request["jiedao"];
            string youbian = context.Request["youbian"];
            string shouhuoren = context.Request["shouhuoren"];
            string mobile = context.Request["mobile"];
            string tell = context.Request["tell"];
            string qq = context.Request["qq"];
            string addressid = context.Request["addressid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=adduseraddress&sign={0}&sheng={1}&shi={2}&xian={3}&jiedao={4}&youbian={5}&shouhuoren={6}&mobile={7}&tell={8}&qq={9}&uid={10}&addressid={11}", sign, sheng, shi, xian, jiedao, youbian, shouhuoren, mobile, tell, qq, uid, addressid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 根据地址ID查询地址信息
        /// </summary>
        /// <param name="context"></param>
        private void addressidlist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string addressid = context.Request["addressid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=addressidlist&sign={0}&addressid={1}&uid={2}", sign, addressid, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取游戏地址列表
        /// 参数：sign=用户签名
        /// 返回状态 state 2未知的签名参数 3你还没有地址，去添加收货地址吧
        /// </summary>
        /// <param name="context"></param>
        private void usergameaddresslist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=usergameaddresslist&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 删除游戏地址
        /// 参数 sign=用户签名 gameaddressid=游戏地址ID
        /// 返回状态 state 2未知的签名参数 3你不能删除别人的地址 0删除成功
        /// </summary>
        private void deleteusergameaddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string gameaddressid = context.Request["gameaddressid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=deleteusergameaddress&sign={0}&uid={1}&gameaddressid={2}", sign, uid, gameaddressid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 设置默认收货地址(游戏)
        /// 参数 sign=用户签名 gameaddressid=游戏地址ID
        /// 返回状态 state 0设为默认成功 2未知的签名参数 3这不是你的地址 4设为默认值失败
        /// </summary>
        /// <param name="context"></param>
        private void defauleusergameaddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string gameaddressid = context.Request["gameaddressid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=defauleusergameaddress&sign={0}&uid={1}&gameaddressid={2}", sign, uid, gameaddressid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 添加游戏收货地址
        /// 参数：sign=用户签名 gameaddressid=游戏地址ID（更新地址必须此参数，添加不需要） gamename=游戏名称 fuwuqi=游戏服务器 daqu=大区 gamenum=游戏账号 shouhuoren=收货人 mobile=手机号
        /// 返回状态 state 0添加成功/更新成功 2未知的签名参数
        /// </summary>
        private void addusergameaddress(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string gameaddressid = context.Request["gameaddressid"];
            string gamename = context.Request["gamename"];
            string fuwuqi = context.Request["fuwuqi"];
            string daqu = context.Request["daqu"];
            string gamenum = context.Request["gamenum"];
            string shouhuoren = context.Request["shouhuoren"];
            string mobile = context.Request["mobile"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=addusergameaddress&sign={0}&uid={1}&gameaddressid={2}&gamename={3}&fuwuqi={4}&daqu={5}&gamenum={6}&shouhuoren={7}&mobile={8}", sign, uid, gameaddressid, gamename, fuwuqi, daqu, gamenum, shouhuoren, mobile));
            context.Response.Write(result);
        }

        /// <summary>
        /// 根据游戏地址ID查询游戏地址信息
        /// 参数 sign=用户签名 gameaddressid=游戏地址ID
        /// 返回状态 state 0获取成功 2未知的签名参数 3游戏地址ID错误
        /// </summary>
        /// <param name="context"></param>
        private void gameaddressidlist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string gameaddressid = context.Request["gameaddressid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=gameaddressidlist&sign={0}&uid={1}&gameaddressid={2}", sign, uid, gameaddressid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 更改用户信息  不包括用户头像
        /// </summary>
        private void updateuserinfo(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string username = context.Request["username"];
            string qianming = context.Request["qianming"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=updateuserinfo&sign={0}&uid={1}&username={2}&qianming={3}", sign, uid, username, qianming));
            context.Response.Write(result);
        }

        /// <summary>
        /// 更改用户IP和城市服务商信息  
        /// </summary>
        private void updateuseripcity(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string cityname = context.Request["cityname"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=updateuseripcity&sign={0}&uid={1}&cityname={2}", sign, uid, cityname));
            context.Response.Write(result);
        }

        /// <summary>
        /// 今日统计信息
        /// </summary>
        /// <param name="context"></param>
        private void countingo(HttpContext context)
        {
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerruser", string.Format("action=countingo"));
            context.Response.Write(result);
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        private string GetIP(HttpContext context)
        {
            string result = String.Empty;
            result = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; //使用代理服务器
            if (null == result || result == String.Empty)
            {
                result = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = context.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>
        /// 获取当前登录用户是否已关注微信公众号
        /// </summary>
        public void GetSubscribe(HttpContext context)
        { 
            go_memberEntity entity=go_memberBusiness.LoadEntity(Globals.GetCurrentMemberUserId());
            if(entity==null || string.IsNullOrEmpty(entity.Wxid))
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 0));
                return;
            }
            int isSubscribe = Messenger.WxSubscribe(entity.Wxid) ? 1 : 0;
            context.Response.Write(string.Format("{{\"state\":{0}}}", isSubscribe));
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