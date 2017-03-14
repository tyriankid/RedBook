using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaoleshop 的摘要说明
    /// </summary>
    public class taotaoleshop : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string action = context.Request["action"];
            switch (action)
            {
                case "listinfo":        //获取商品列表
                    listinfo(context);
                    break;
                case "detail":          //获取商品详情
                    detail(context);
                    break;
                case "detailrenovate":          //获取商品详情刷新
                    detailrenovate(context);
                    break;
                case "shopidbyqishu":   //获取一元购商品ID
                    shopidbyqishu(context);
                    break;
                case "buyrecord":       //商品参入记录
                    buyrecord(context);
                    break;
                case "history":          //获取往期揭晓
                    history(context);
                    break;
                case "algorithmwon":    //获取计算详情
                    algorithmwon(context);
                    break;
                case "getshopcategory":    //获取商品分类列表
                    getshopcategory(context);
                    break;
                case "reflistinfo":    //只获取商品剩余人数数据
                    reflistinfo(context);
                    break;
                case "quanProductList":
                    quanProductList(context);//获取直购商品列表
                    break;
                case "quanDetail":
                    quanDetail(context);//获取直购商品详情
                    break;
                case "quanCategoryList"://获取直购页面的分类展示信息
                    quanCategoryList(context);
                    break;
                case "getshopBrand"://获取直购页面的品牌展示信息
                    getshopBrand(context);
                    break;
            }

        }

        private void getshopBrand(HttpContext context)
        {
            string cateId = context.Request["cateId"];
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=getshopBrand&cateId={0}", cateId));
            context.Response.Write(result);
        }
        private void quanCategoryList(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=quanCategoryList&uid={0}", uid));
            context.Response.Write(result);
        }
        private void quanDetail(HttpContext context)
        {
            //获取传参
            string QuanId = context.Request["QuanId"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=quanDetail&QuanId={0}&uid={1}", QuanId, uid));
            context.Response.Write(result);
        }


        private void quanProductList(HttpContext context)
        {
            //获取传参
            string p = context.Request["p"];
            string brandId = context.Request["brandId"];
            string category = context.Request["category"];
            string productType = context.Request["type"];
            string productName = context.Request["productName"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=quanProductList&p={0}&brandId={1}&category={2}&type={3}&productName={4}", p, brandId, category, productType, productName));
            context.Response.Write(result);
        }

        private void listinfo(HttpContext context)
        {
            //获取传参
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "0";
            string order = context.Request["order"];
            if (string.IsNullOrEmpty(order)) order = "0";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            string pagesize = context.Request["pagesize"];
            string secsOpen = context.Request["secsOpen"];
            if (string.IsNullOrEmpty(pagesize) || !PageValidate.IsNumberPlus(pagesize)) pagesize = "1";

            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=listinfo&typeid={0}&order={1}&p={2}&pagesize={3}&secsOpen={4}", typeid, order, p, pagesize, secsOpen));
            context.Response.Write(result);
        }
        private void reflistinfo(HttpContext context)
        {
            //获取传参
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            string reftime = context.Request["reftime"];
            if (string.IsNullOrEmpty(reftime)) reftime = DateTime.Now.ToString();
            string typeid = context.Request["typeid"];
            string order = context.Request["order"];
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=reflistinfo&typeid={0}&order={1}&p={2}&reftime={3}", typeid, order, p, reftime));
            context.Response.Write(result);
        }
        private void detail(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            string productid = context.Request["productid"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "4");
            //sign = SecurityHelper.GetSign(uid);

            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=detail&shopid={0}&uid={1}&productid={2}", shopid, uid, productid));
            context.Response.Write(result);
        }
        private void detailrenovate(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=detailrenovate&shopid={0}&uid={1}", shopid, uid));
            context.Response.Write(result);
        }
        private void shopidbyqishu(HttpContext context)
        {
            //获取传参
            string qishu = context.Request["qishu"];
            string productid = context.Request["productid"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=shopidbyqishu&qishu={0}&uid={1}&productid={2}", qishu, uid, productid));
            context.Response.Write(result);
        }

        private void buyrecord(HttpContext context)
        {
            //获取传参
            string productid = context.Request["ShopID"];
            int p = Convert.ToInt32(context.Request["p"]);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=buyrecord&ShopID={0}&p={1}", productid, p));
            context.Response.Write(result);
        }

        private void history(HttpContext context)
        {
            //获取传参
            string productid = context.Request["productid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=history&productid={0}&p={1}", productid, p));
            context.Response.Write(result);

        }

        private void algorithmwon(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=algorithmwon&ShopID={0}", shopid));
            context.Response.Write(result);

        }

        private void getshopcategory(HttpContext context)
        {
            //获取传参
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshop", string.Format("action=getshopcategory"));
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