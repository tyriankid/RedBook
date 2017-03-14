using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YH.Utility;
using Taotaole.Common;
using Taotaole.Model;
using System.IO;
using YH.Weixin.MP.QrCode;


namespace RedBookPlatform.channel
{
    public partial class spreadLink : Base
    {
        string Uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string shareLink = "1元就能买IPhone 6哦，快去看看吧！ http://wx.ewaywin.com/index.aspx?agentid=";
                Uid=this.CurrLoginChannelUser.Uid.ToString();
                string encryptUid = SecurityHelper.Encrypt(Globals.TaotaolePlatformKey,Uid);
                this.txtLink.Value = shareLink + encryptUid;
            }
        }

        /// <summary>
        /// 下载推广码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDown_Click(object sender, EventArgs e)
        {
            Uid = this.CurrLoginChannelUser.Uid.ToString();
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            string savepath = System.Web.HttpContext.Current.Server.MapPath("~/Resources/TicketImage") + "\\" + string.Format("channel_{0}", Uid) + ".jpg";
            if (!File.Exists(savepath))
            {
                YH.Weixin.MP.Api.TicketAPI.GetTicketImage(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, string.Format("channel_{0}", Uid), false);
            }
            string qrCodeBackImgUrl = "/Resources/TicketImage/" + string.Format("channel_{0}", Uid) + ".jpg";
            DownFile(this, savepath);


        }

        public void DownFile(System.Web.UI.Page page, string path)
        {
            try
            {
                System.IO.FileInfo myFile = new System.IO.FileInfo(path);
                page.Response.Clear();
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(myFile.Name));
                page.Response.AddHeader("Content-Length", myFile.Length.ToString());
                page.Response.ContentType = "application/octet-stream";
                page.Response.TransmitFile(myFile.FullName);
                page.Response.End();
            }
            catch
            {
               
            }
        }
    }
}