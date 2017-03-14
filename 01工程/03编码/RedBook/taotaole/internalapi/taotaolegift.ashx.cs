using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// 积分商城相关
    /// </summary>
    public class taotaolegift : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "getScoreNums": //获取积分场区间
                    getScoreNums(context);
                    break;
                case "getGiftList": //获取待开奖商品
                    getGiftList(context);
                    break;
                case "goGiftDraw": //跑马灯,大转盘开奖
                    goGiftDraw(context);
                    break;
                case "goGiftGua": //跑马灯,大转盘开奖
                    goGiftGua(context);
                    break;
                case "getScoreDetail"://积分使用详情
                    getScoreDetail(context);
                    break;
                case "giftWinDetail"://奖品详情
                    giftWinDetail(context);
                    break;
                case "giftGameIsOpen"://游戏是否开启
                    giftGameIsOpen(context);
                    break;
            }
        }
        /// <summary>
        /// 游戏是否开启
        /// </summary>
        private void giftGameIsOpen(HttpContext context)
        {
            string Score = context.Request["Score"];
            string gameType = context.Request["gameType"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=giftGameIsOpen&Score={0}&gameType={1}",Score, gameType));
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取积分详情
        /// </summary>
        private void getScoreDetail(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=getScoreDetail&sign={0}&uid={1}",sign,uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 中奖列表
        /// </summary>
        /// <param name="context"></param>
        private void giftWinDetail(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=giftWinDetail&sign={0}&uid={1}",sign,uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取积分场区间
        /// </summary>
        private void getScoreNums(HttpContext context)
        {
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=getScoreNums"));
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取相应区间商品
        /// </summary>
        private void getGiftList(HttpContext context)
        {
            string maxScore = context.Request["maxScore"];
            string gameType = context.Request["gameType"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=getGiftList&sign={0}&uid={1}&maxScore={2}&gameType={3}", sign, uid, maxScore, gameType));
            context.Response.Write(result);
        }

        /// <summary>
        /// 跑马灯,大转盘抽奖
        /// </summary>
        private void goGiftDraw(HttpContext context)
        {
            string maxScore = context.Request["maxScore"];
            string gameType = context.Request["gameType"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=goGiftDraw&sign={0}&uid={1}&maxScore={2}&gameType={3}", sign, uid, maxScore, gameType));
            context.Response.Write(result);
        }

        /// <summary>
        /// 刮刮乐抽奖
        /// </summary>
        private void goGiftGua(HttpContext context)
        {
            string giftid = context.Request["giftId"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrgift", string.Format("action=goGiftGua&sign={0}&uid={1}&giftId={2}", sign, uid, giftid));
            context.Response.Write(result);
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