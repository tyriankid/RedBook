using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Taotaole.Bll;
using Taotaole.Model;
using YH.Utility;
using System.Data;
using Taotaole.Common;
using Newtonsoft.Json.Linq;
using Taotaole.Business;

namespace Taotaole.IApi
{
    /// <summary>
    /// 用户相关接口V1.0 
    /// by add jinhb_20160729
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
                case "appreguser":          //app注册
                    appreguser(context);
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
                case "updateuseripcity"://更改用户ip和城市服务商
                    updateuseripcity(context);
                    break;
                case "countingo"://今日统计信息
                    countingo(context);
                    break;
                case "sign":          //app注册
                    string sign = context.Request["sign"];
                    string uid = SecurityHelper.GetSign(sign);
                    context.Response.Write(uid);
                    break;
                case "activityMember":               // 点击图文送红包
                    activityMember(context);
                    break;
                #region
                /*
                    string str1 = DateTime.Now.ToString("yyyyMMddHHmmssss");//充值参数数组，顺序为：充值时间、充值号码、充值金额(个数)、使用类型
                    string str2 = "18162666151";
                    string str3 = "100";
                    string str4 = yollyinterface.GenerateYollyID( yollyinterface.usetype.Phone);
                    string str5 = "10";
                    string strs = yollyinterface.APIyolly(new object[] { str1, str2, str3, str4, "2" }, yollyinterface.usetype.Phone);
                    Globals.DebugLogger(strs);
                    context.Response.Write(strs);
                    context.Response.End();
                    string uid = context.Request["uid"];
                    if (!string.IsNullOrEmpty(uid))
                    {
                        string hid = str2.Replace(str2.Substring(3, 4), "****");
                        string sign = SecurityHelper.Encrypt(DbServers.APIKey, uid);
                        context.Response.Write("{\"state\":" + sign + "}" + hid + "");    //未知的签名参数
                        return;
                    }
                    */

                    /*
                    para += string.Format("YOLLYID={0}", HttpContext.Current.Server.UrlEncode(YOLLYID));
                    para += string.Format("&YOLLYTIME={0}", HttpContext.Current.Server.UrlEncode((post[0]).ToString()));//发起时间
                    para += string.Format("&YOLLYURL={0}", HttpContext.Current.Server.UrlEncode(""));//发起时间
                    para += string.Format("&YOLLYFLOW={0}", HttpContext.Current.Server.UrlEncode((post[3]).ToString()));//流水号
                    para += string.Format("&MOBILE={0}", HttpContext.Current.Server.UrlEncode((post[1]).ToString()));//充值号码
                    para += string.Format("&MONEY={0}", HttpContext.Current.Server.UrlEncode((post[2]).ToString()));//充值金额  65548187000000120160823184853134770789511D39316FEC4984ABE97ED554D749EBE86
                    para += string.Format("&TYPE={0}", HttpContext.Current.Server.UrlEncode(""));        
                    */


                    //string Pserialid = yollyinterface.GenerateYollyID(yollyinterface.usetype.Phone);
                    //Globals.DebugLogger(Pserialid);
                    //        DateTime Pnow = DateTime.Now;
                    //        object[] Ppost = new object[] { Pnow.ToString("yyyyMMddHHmmssss"), "13349946975", 1000, Pserialid, "" };
                    //        string Pret = yollyinterface.APIyolly(Ppost, yollyinterface.usetype.Phone);
                    //        context.Response.Write(Pret);
                    //        Globals.DebugLogger(Pret);


                /*
                        string Aserialid = yollyinterface.GenerateYollyID(yollyinterface.usetype.Alipay);
                        Globals.DebugLogger(Aserialid);
                        DateTime Anow = DateTime.Now;
                        object[] Apost = new object[] { Anow.ToString("yyyyMMddHHmmssss"), "jinihuabin1219@163.com", 1, Aserialid, 2 };//, sdt.Rows[0]["money"].ToString()
                        string Aret = yollyinterface.APIyolly(Apost, yollyinterface.usetype.Alipay);
                        Globals.DebugLogger(Aret);
                */
                #endregion

            }
        }
        /// <summary>
        /// 点击图文送红包
        /// </summary>
        /// <param name="context"></param>
        private void activityMember(HttpContext context)
        {
         
            //验证签名
             string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
           // string uid = "1";
            if (string.IsNullOrEmpty(uid))
            {
              
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            DataTable dtactivity = go_activityMemberBusiness.GetListData(" uid=" + uid + "");
            if (dtactivity.Rows.Count > 0)
            {

                context.Response.Write("{\"state\":3}");    //用户红包无资格领取
                return;
            }
            go_activityMemberEntity activityEntity = new go_activityMemberEntity
            {
                Uid = int.Parse(uid),
                IsReceive = "是"
            };
            //保存用户领取记录
            if (go_activityMemberBusiness.SaveEntity(activityEntity, true))
            {
              
                go_memberEntity memberEntity = go_memberBusiness.LoadEntity(int.Parse(uid));
                memberEntity.Uid = int.Parse(uid);
                memberEntity.Money = memberEntity.Money + 200;
                memberEntity.Score = memberEntity.Score + 200;
                //修改用户金币积分余额
                if (go_memberBusiness.SaveEntity(memberEntity, false))
                {   
                    //插入金币充值记录
                    string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.add);
                    go_member_addmoney_recordEntity addMoneyEntity = new go_member_addmoney_recordEntity
                    {
                        Uid = int.Parse(uid),
                        Code = orderid,
                        Money = 200,
                        Pay_type = "活动赠送",
                        Status = "已付款",
                        Time = DateTime.Now,
                        Memo = "add"
                    };
                    go_member_addmoney_recordBusiness.SaveEntity(addMoneyEntity, true);
                    //插入积分获取详情
                    go_scoreDetailEntity score = new go_scoreDetailEntity
                    {
                        Uid = int.Parse(uid),
                        Usetime = DateTime.Now,
                        Money = 200,
                        Use_range = "活动赠送",
                        Detail = "活动赠送",
                        OrderId = orderid,
                        Type = "1",
                    };
                    go_scoreDetailBusiness.SaveEntity(score, true);

                    context.Response.Write("{\"state\":4}");    //用户红包已领取成功
                    return;
                }
                else
                {
                   
                    context.Response.Write("{\"state\":5}");    //活动异常
                    return;
                }
            }
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
            //验证格式
            string mobile = context.Request["mobile"];
            string password64 = context.Request["password"];
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password64))
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));
                return;
            }
            string password = DataFormat.Base64Decode(password64);//解密后的明文密码
            string passwordMD5 = SecurityHelper.GetMd5To32(password);//md5加密后的密码

            //数据校验
            IList<go_memberEntity> listadminEntity = go_memberBusiness.GetListEntity(" mobile='" + mobile + "' and mobilecode='1' and password='" + passwordMD5 + "'", "*", null, 0);
            if (listadminEntity.Count == 0)
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 3));
                return;
            }
            //更新会员登录信息、签名等
            listadminEntity[0].User_ip = NetworkHelper.GetBuyIP();
            go_memberBusiness.UpdateLogin(listadminEntity[0]);
            string sign = SecurityHelper.Encrypt(DbServers.APIKey, listadminEntity[0].Uid.ToString());


            //返回用户JSON
            string result = string.Format("{{\"state\":{0},\r\"sign\":\"{1}\",", 0, sign);
            result += JsonConvert.SerializeObject(listadminEntity[0], Newtonsoft.Json.Formatting.Indented).TrimStart('{').TrimEnd('}');//JsonConvert.SerializeObject(dt)
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 检查手机号是否已经被注册
        /// 参数：mobile 用户手机号
        /// 返回状态 state  0代表可以注册  2代表手机号已被别人注册
        /// </summary>
        /// <param name="context"></param>
        private void checkmobile(HttpContext context)
        {
            string type = context.Request["type"];
            int mobilecode = -1;
            if (!string.IsNullOrEmpty(type))
            {
                mobilecode = 1;
            }
            string mobile = context.Request["mobile"];
            //根据手机号查询用户实体类
            IList<go_memberEntity> listadminEntity = go_memberBusiness.GetListEntity("mobile='" + mobile + "' and mobilecode='" + mobilecode + "'", "mobile", null, 1);
            if (listadminEntity.Count == 0)
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 0));  //0代表可以注册
                return;
            }
            else
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));  //2代表已被注册
                return;
            }
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
            string password = DataFormat.Base64Decode(password64);//解密后的明文密码
            string passwordMD5 = SecurityHelper.GetMd5To32(password);//md5加密后的密码

            //将手机中间四位变成****
            string hid = mobile.Replace(mobile.Substring(3, 4), "****");
            go_memberEntity memberEntity = new go_memberEntity
            {
                Groupid = 1,
                Username = "用户：" + hid,
                Email = "",
                Mobile = mobile,
                Password = passwordMD5,
                Money = 0,
                Jingyan = 0,
                Score = 0,
                Img = "photo/member.jpg",
                Qianming = " ",
                Mobilecode = "-1",
                Approuse = 0,
                Time = DateTime.Now,
                User_ip = NetworkHelper.GetBuyIP(),
                Band = "",
                Headimg = "",
                Wxid = "",
                Addgroup = "",
                Emailcode = "-1",
                Passcode = "",
                Reg_key = "",
                Telcode = ""
            };
            if (go_memberBusiness.SaveEntity(memberEntity, true))
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 0));  //0代表注册成功
                return;
            }
            else
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));  //2代表注册失败
                return;
            }
        }

        /// <summary>
        /// 更新密码
        /// 传参 pass：原密码 或者 code：短信验证码 passnew：新密码
        /// 返回状态 state  0代更新成功 //1代表签名验证错误 //2代更新失败  //3代表验证过时，请重新发送验证码 //4代表验证错误
        /// </summary>
        /// <param name="context"></param>
        private void updatepass(HttpContext context)
        {
            //验证签名
            string mobile = context.Request["mobile"];
            if (string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"state\":2}");    //手机号错误
                return;
            }
            //获取传参原密码
            string password64 = context.Request["newpass"];
            if (string.IsNullOrEmpty(password64))
            {
                context.Response.Write("{\"state\":3}");    //未知的签名参数
                return;
            }
            string password = DataFormat.Base64Decode(password64);//解密后的明文密码
            string passwordMD5 = SecurityHelper.GetMd5To32(password);//md5加密后的密码
            DataTable dt = go_memberBusiness.GetListData("mobile='" + mobile + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(dt.Rows[0]["uid"]), DbServers.DbServerName.LatestDB);
            memberEntity.Password = passwordMD5;
            if (go_memberBusiness.SaveEntity(memberEntity, false, DbServers.DbServerName.LatestDB))
            {
                string sign = SecurityHelper.Encrypt(DbServers.APIKey, memberEntity.Uid.ToString());
                //返回用户JSON
                string result = string.Format("{{\"state\":{0},\r\"sign\":\"{1}\"", 0, sign);
                result += "}";
                context.Response.Write(result);  //0代表验证成功
                return;
            }

        }




        /// <summary>
        /// 查看某用户
        /// 参数：uid 用户ID
        /// 返回关于uid的实体类
        /// </summary>
        /// <param name="context"></param>
        private void visituser(HttpContext context)
        {
            bool isMy = true;
            string uid = "0";
            string otherid = context.Request["otherid"];
            if (!string.IsNullOrEmpty(otherid) && PageValidate.IsNumberPlus(otherid))
            {
                isMy = false;
                uid = otherid;
            }
            if (isMy)
            {
                //Globals.DebugLogger("visituserA-" + context.Request["sign"]);
                //Globals.DebugLogger("visituserB-" + context.Request["uid"]);
                uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
                if (string.IsNullOrEmpty(uid))
                {
                    context.Response.Write("{\"state\":2}");    //未知的签名参数
                    return;
                }
            }
            DataTable dt = go_memberBusiness.GetListData("uid='" + uid + "'", "uid,username,email,mobile,img,qianming,headimg,luckyb,money,score,groupid", null, 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));  //2代表用户ID不存在
                return;
            }
            dt.Columns.Add("touxiang", typeof(string));
            ShopOrders.SetTouxiang(dt.Rows[0]);

            //输出JSON
            if (isMy)
            {
                //输出个人完整信息
                string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"username\":\"{2}\",\r\"email\":\"{3}\",\r\"mobile\":\"{4}\",\r\"qianming\":\"{5}\",\r\"touxiang\":\"{6}\",\r\"luckyb\":\"{7}\",\r\"groupid\":\"{8}\",\r\"money\":\"{9}\",\r\"uid\":\"{10}\",\r\"score\":\"{11}\""
                    , 0, "", dt.Rows[0]["username"], dt.Rows[0]["email"], dt.Rows[0]["mobile"], dt.Rows[0]["qianming"], dt.Rows[0]["touxiang"], dt.Rows[0]["luckyb"], dt.Rows[0]["groupid"], dt.Rows[0]["money"], dt.Rows[0]["uid"], dt.Rows[0]["score"]);
                result += "}";
                context.Response.Write(result);
            }
            else
            {
                //查看他人部分信息

                string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"username\":\"{2}\",\r\"email\":\"{3}\",\r\"mobile\":\"{4}\",\r\"qianming\":\"{5}\",\r\"touxiang\":\"{6}\",\r\"luckyb\":\"{7}\",\r\"money\":\"{8}\",\r\"uid\":\"{9}\",\r\"score\":\"{10}\""
                    , 0, "", dt.Rows[0]["username"], dt.Rows[0]["email"], dt.Rows[0]["mobile"], dt.Rows[0]["qianming"], dt.Rows[0]["touxiang"], dt.Rows[0]["luckyb"], dt.Rows[0]["money"], dt.Rows[0]["score"]);
                result += "}";
                context.Response.Write(result);
            }

        }

        /// <summary>
        /// app注册
        /// </summary>
        /// <param name="context"></param>
        private void appreguser(HttpContext context)
        {
            string info = context.Request["info"];
            if (string.IsNullOrEmpty(info))
            {
                context.Response.Write(string.Format("{{\"state\":{0}}}", 2));  //授权登录失败
                return;
            }

            //获取授权信息
            JObject obj2 = JsonConvert.DeserializeObject(info) as JObject;
            string unionId = (obj2["unionid"] == null) ? "" : obj2["unionid"].ToString(); //微信用户UNIONID
            string where = string.IsNullOrEmpty(unionId) ? string.Format("wxid='{0}'", obj2["openid"].ToString()) : string.Format("unionid='{0}'", unionId);
            IList<go_memberEntity> listmemberEntity = go_memberBusiness.GetListEntity(where);
            if (listmemberEntity.Count == 0)
            {
                #region reg
                go_memberEntity memberEntity = new go_memberEntity
                {
                    Groupid = 1,
                    Username = obj2["nickname"].ToString(),
                    Email = " ",
                    Mobile = "",
                    Password = SecurityHelper.GetMd5To32(new Random().Next(100000, 999999).ToString()),
                    Money = 0,
                    Jingyan = 0,
                    Score = 0,
                    Img = "photo/member.jpg",
                    Qianming = " ",
                    Mobilecode = "-1",
                    Approuse = 0,
                    Time = DateTime.Now,
                    User_ip = NetworkHelper.GetBuyIP(),
                    Band = "",
                    Headimg = obj2["headimgurl"].ToString(),
                    //Wxid = obj2["openid"].ToString(),
                    Appwxid = obj2["openid"].ToString(),
                    Addgroup = "",
                    Emailcode = "-1",
                    Passcode = "",
                    Reg_key = "",
                    Telcode = "",
                    Typeid = 2,//注册来源 0默认微信关注  1 app手机登录  2 app_微信授权登录
                    Unionid = (obj2["unionid"] == null) ? "" : obj2["unionid"].ToString()
                };
                go_memberBusiness.SaveEntity(memberEntity, true);
                #endregion
                listmemberEntity.Add(memberEntity);
            }
            else
            {
                go_memberEntity entity = listmemberEntity[0];
                entity.Approuse = 1;
                go_memberBusiness.SaveEntity(entity, false);
            }
            string sign = SecurityHelper.Encrypt(DbServers.APIKey, listmemberEntity[0].Uid.ToString());

            //返回用户JSON
            string result = string.Format("{{\"state\":{0},\r\"sign\":\"{1}\",", 0, sign);
            result += JsonConvert.SerializeObject(listmemberEntity[0], Newtonsoft.Json.Formatting.Indented).TrimStart('{').TrimEnd('}');//JsonConvert.SerializeObject(dt)
            result += "}";
            WxLogger(result);
            context.Response.Write(result);

        }

        /// <summary>
        /// 用户地址列表
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void useraddresslist(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //获取用户地址信息
            DataTable dt = CustomsBusiness.GetListData("go_member_dizhi d join go_member m on d.uid=m.uid", "d.uid='" + uid + "'", "m.username,d.sheng,d.id,d.shi,d.xian,d.tell,d.jiedao,d.youbian,d.shouhuoren,d.mobile,d.qq,d.isdefault", "isdefault desc", 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":3}");    //你还没有地址，去添加收货地址吧
                return;
            }
            string result = string.Format("{{\"state\":{0},\"default\":\"{1}\",\r\"data\":", 0, dt.Rows[0]["isdefault"]);
            result += JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented).TrimStart('{').TrimEnd('}');//JsonConvert.SerializeObject(dt)
            result += "}";
            WxLogger(result);
            context.Response.Write(result);
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        private void deleteuseraddress(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string id = context.Request["addressid"];
            go_member_dizhiEntity dizhiEntity = go_member_dizhiBusiness.LoadEntity(Convert.ToInt32(id));
            if (dizhiEntity.Uid.ToString() != uid)
            {
                context.Response.Write("{\"state\":3}");    //你不能删除别人的地址
                return;
            }
            go_member_dizhiBusiness.Del(Convert.ToInt32(id));
            context.Response.Write("{\"state\":0}");    //删除成功
            return;
        }

        /// <summary>
        /// 设为默认地址
        /// </summary>
        private void defauleuseraddress(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string id = context.Request["addressid"];
            go_member_dizhiEntity dizhiEntity = go_member_dizhiBusiness.LoadEntity(Convert.ToInt32(id));
            if (dizhiEntity.Uid.ToString() != uid)
            {
                context.Response.Write("{\"state\":3}");    //这不是你的地址
                return;
            }
            //查询用户的所有地址信息
            DataTable dt = go_member_dizhiBusiness.GetListData(" uid='" + uid + "'", "*", null, 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":5}");    //这不是你的地址
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == id)
                {
                    dt.Rows[i]["isdefault"] = "Y";
                }
                else
                {
                    dt.Rows[i]["isdefault"] = "N";
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            string[] strsql = { "select * from go_member_dizhi" };
            //整表提交数据，并保存
            if (!CustomsBusiness.CommitDataSet(ds, strsql, DbServers.DbServerName.LatestDB))
            {
                context.Response.Write("{\"state\":4}");    //设为默认值失败
                return;
            }
            context.Response.Write("{\"state\":0}");    //设为默认值成功
            return;
        }

        /// <summary>
        /// 添加用户地址
        /// </summary>
        /// <param name="context"></param>
        private void adduseraddress(HttpContext context)
        {
            bool isAdd = true;
            string addressid = context.Request["addressid"];
            if (string.IsNullOrEmpty(addressid))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string sheng = context.Request["sheng"];
            string shi = context.Request["shi"];
            string xian = context.Request["xian"];
            string jiedao = context.Request["jiedao"];
            string shouhuoren = context.Request["shouhuoren"];
            string mobile = context.Request["mobile"];
            go_member_dizhiEntity dizhiEntity = (isAdd) ? new go_member_dizhiEntity() : go_member_dizhiBusiness.LoadEntity(Convert.ToInt32(addressid));
            isAdd = (dizhiEntity != null && dizhiEntity.Id != 0) ? false : true;
            dizhiEntity.Uid = Convert.ToInt32(uid);
            dizhiEntity.Sheng = sheng;
            dizhiEntity.Shi = shi;
            dizhiEntity.Xian = xian;
            dizhiEntity.Jiedao = jiedao;
            dizhiEntity.Youbian = 0;
            dizhiEntity.Shouhuoren = shouhuoren;
            dizhiEntity.Mobile = mobile;
            dizhiEntity.Tell = "";
            dizhiEntity.Qq = "";
            if (isAdd)
                dizhiEntity.isDefault = "N";
            dizhiEntity.Shifoufahuo = 0;
            dizhiEntity.Time = DateTime.Now;
            bool issave = go_member_dizhiBusiness.SaveEntity(dizhiEntity, isAdd);
            Globals.DebugLogger("issave:" + issave.ToString());
            if (isAdd)
            {
                context.Response.Write("{\"state\":0}");    //添加失败
                return;
            }
            else
            {
                context.Response.Write("{\"state\":0}");    //更新成功
                Globals.DebugLogger("更新成功");
                return;
            }

        }

        /// <summary>
        /// 根据地址ID查询地址信息
        /// </summary>
        /// <param name="context"></param>
        private void addressidlist(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string addressid = context.Request["addressid"];
            if (string.IsNullOrEmpty(addressid))
            {
                context.Response.Write("{\"state\":3}");    //地址ID错误
                return;
            }
            go_member_dizhiEntity dizhiEntity = go_member_dizhiBusiness.LoadEntity(Convert.ToInt32(addressid));
            if (dizhiEntity.Uid == Convert.ToInt32(uid))
            {
                //输出JSON
                string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":"
                    , 0, Globals.G_UPLOAD_PATH);
                result += JsonConvert.SerializeObject(dizhiEntity, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                context.Response.Write(result);
            }
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
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //获取用户地址信息
            DataTable dt = CustomsBusiness.GetListData("go_member_dizhi_game g join go_member m on g.uid=m.uid ", "m.uid='" + uid + "'", "m.username,gamename,g.id,g.gamearea,g.gameserver,g.gameusercode,g.shouhuoren,g.mobile,g.isdefault,g.uid", "g.isdefault desc", 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":3}");    //你还没有地址，去添加收货地址吧
                return;
            }
            string result = string.Format("{{\"state\":{0},\"default\":\"{1}\",\r\"data\":", 0, dt.Rows[0]["isdefault"]);
            result += JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented).TrimStart('{').TrimEnd('}');//JsonConvert.SerializeObject(dt)
            result += "}";
            WxLogger(result);
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
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string id = context.Request["gameaddressid"];
            go_member_dizhi_gameEntity gameEntity = go_member_dizhi_gameBusiness.LoadEntity(Convert.ToInt32(id));
            if (gameEntity.Uid.ToString() != uid)
            {
                context.Response.Write("{\"state\":3}");    //你不能删除别人的地址
                return;
            }
            go_member_dizhi_gameBusiness.Del(Convert.ToInt32(id));
            context.Response.Write("{\"state\":0}");    //删除成功
            return;
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
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string id = context.Request["gameaddressid"];
            go_member_dizhi_gameEntity gameEntity = go_member_dizhi_gameBusiness.LoadEntity(Convert.ToInt32(id));
            if (gameEntity.Uid.ToString() != uid)
            {
                context.Response.Write("{\"state\":3}");    //这不是你的地址
                return;
            }
            //查询用户的所有地址信息
            DataTable dt = go_member_dizhi_gameBusiness.GetListData(" uid='" + uid + "'", "*", null, 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":5}");    //这不是你的地址
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == id)
                {
                    dt.Rows[i]["isdefault"] = "Y";
                }
                else
                {
                    dt.Rows[i]["isdefault"] = "N";
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            string[] strsql = { "select * from go_member_dizhi_game" };
            //整表提交数据，并保存
            if (!CustomsBusiness.CommitDataSet(ds, strsql, DbServers.DbServerName.LatestDB))
            {
                context.Response.Write("{\"state\":4}");    //设为默认值失败
                return;
            }
            context.Response.Write("{\"state\":0}");    //设为默认值成功
            return;
        }

        /// <summary>
        /// 添加游戏收货地址
        /// 参数：sign=用户签名 gameaddressid=游戏地址ID（更新地址必须此参数，添加不需要） gamename=游戏名称 fuwuqi=游戏服务器 daqu=大区 gamenum=游戏账号 shouhuoren=收货人 mobile=手机号
        /// 返回状态 state 0添加成功/更新成功 2未知的签名参数
        /// </summary>
        private void addusergameaddress(HttpContext context)
        {
            bool isAdd = true;
            string gameaddressid = context.Request["gameaddressid"];
            if (string.IsNullOrEmpty(gameaddressid))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string gamename = context.Request["gamename"];
            string fuwuqi = context.Request["fuwuqi"];
            string daqu = context.Request["daqu"];
            string gamenum = context.Request["gamenum"];
            string shouhuoren = context.Request["shouhuoren"];
            string mobile = context.Request["mobile"];
            go_member_dizhi_gameEntity gameEntity = (isAdd) ? new go_member_dizhi_gameEntity() : go_member_dizhi_gameBusiness.LoadEntity(Convert.ToInt32(gameaddressid));
            isAdd = (gameEntity != null && gameEntity.Id != 0) ? false : true;
            gameEntity.Uid = Convert.ToInt32(uid);
            gameEntity.Gamename = gamename;
            gameEntity.Gamearea = fuwuqi;
            gameEntity.Gameserver = daqu;
            gameEntity.Gameusercode = gamenum;
            gameEntity.Shouhuoren = shouhuoren;
            gameEntity.Mobile = mobile;
            if (isAdd)
                gameEntity.Isdefault = "N";
            gameEntity.Time = DateTime.Now;

            go_member_dizhi_gameBusiness.SaveEntity(gameEntity, isAdd);
            if (isAdd)
            {
                context.Response.Write("{\"state\":0}");    //添加成功
                return;
            }
            else
            {
                context.Response.Write("{\"state\":0}");    //更新成功
                Globals.DebugLogger("更新成功");
                return;
            }
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
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string gameaddressid = context.Request["gameaddressid"];
            if (string.IsNullOrEmpty(gameaddressid))
            {
                context.Response.Write("{\"state\":3}");    //游戏地址ID错误
                return;
            }
            go_member_dizhi_gameEntity gameEntity = go_member_dizhi_gameBusiness.LoadEntity(Convert.ToInt32(gameaddressid));
            if (gameEntity.Uid == Convert.ToInt32(uid))
            {
                //输出JSON
                string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":["
                    , 0, Globals.G_UPLOAD_PATH);
                result += JsonConvert.SerializeObject(gameEntity, Newtonsoft.Json.Formatting.Indented);
                result += "]}";

                context.Response.Write(result);
            }
        }

        /// <summary>
        /// 更改用户信息  不包括用户头像
        /// </summary>
        private void updateuserinfo(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
            string username = context.Request["username"];
            if (!string.IsNullOrEmpty(username))
            {
                memberEntity.Username = username;
                memberEntity.Groupid = memberEntity.Groupid + 1;
            }
            string qianming = context.Request["qianming"];
            if (!string.IsNullOrEmpty(qianming) || qianming == "")
            {
                memberEntity.Qianming = qianming;
            }
            if (go_memberBusiness.SaveEntity(memberEntity, false))
            {
                context.Response.Write("{\"state\":0}");    //更改成功
                return;
            }
        }

        /// <summary>
        /// 更改城市服务商信息  
        /// </summary>
        private void updateuseripcity(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //已存在城市服务商
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
            if (memberEntity.Servicecityid != 0)
            {
                context.Response.Write("{\"state\":0}");
                return;
            }

            //更新所属城市服务商
            string cityname = context.Request["cityname"];
            if (!string.IsNullOrEmpty(cityname))
            {
                object ouid = go_channel_userBusiness.GetScalar(string.Format("cityid = '{0}' and parentid = 0", cityname), "uid");
                if (ouid != null)
                {
                    memberEntity.Servicecityid = int.Parse(ouid.ToString());
                    go_memberBusiness.SaveEntity(memberEntity, false);
                }
            }

            context.Response.Write("{\"state\":0}");
        }
        /// <summary>
        /// 今日统计信息
        /// </summary>
        /// <param name="context"></param>
        private void countingo(HttpContext context)
        {
            DateTime DT = DateTime.Now;
            DT.AddHours(-(DT.Hour));
            DT.AddMilliseconds(-(DT.Millisecond));
            DT.AddMinutes(-(DT.Minute));
            DT.AddSeconds(-(DT.Second));
            //获取今天时间
            string newtime = DT.Date.ToString();
            DataTable member = go_memberBusiness.GetListData("time>'" + newtime + "'", "COUNT(*)as count ", null, 0, DbServers.DbServerName.LatestDB);//新增会员数量
            DataTable orders = go_ordersBusiness.GetListData("time>'" + newtime + "'", "COUNT(*)as count ", null, 0, DbServers.DbServerName.LatestDB);//新增订单数量
            DataTable addmoney = go_member_addmoney_recordBusiness.GetListData("time>'" + newtime + "' and status='已付款'", "SUM(money) as money", DbServers.DbServerName.LatestDB);//今日充值金额
            DataTable xiaofei = go_member_accountBusiness.GetListData("time>'" + newtime + "'", "SUM(money) as money", null, 0, DbServers.DbServerName.LatestDB);//今日消费金额
            DataTable kaijiang = go_yiyuanBusiness.GetListData("time >'" + newtime + "' and shengyurenshu=0", "COUNT(*) as count", DbServers.DbServerName.LatestDB);//今日开奖次数
            DataTable allmember = go_memberBusiness.GetListData(null, "COUNT(*)as count ", null, 0, DbServers.DbServerName.LatestDB);//会员总数量
            DataTable alladdmoney = go_member_addmoney_recordBusiness.GetListData("status='已支付'", "SUM(money) as money", DbServers.DbServerName.LatestDB);//今日充值金额
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"newmember\":\"{1}\",\r\"neworders\":\"{2}\",\r\"newaddmoney\":\"{3}\",\r\"newconsumemoney\":\"{4}\",\r\"newprizenum\":\"{5}\",\r\"newallmember\":\"{6}\",\r\"time\":\"{7}\""
                , 0, member.Rows[0]["count"], orders.Rows[0]["count"], Convert.ToDecimal(addmoney.Rows[0]["money"]).ToString("0.00"), Convert.ToDecimal(xiaofei.Rows[0]["money"]).ToString("0.00"), kaijiang.Rows[0]["count"], allmember.Rows[0]["count"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            result += "}";

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


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}