using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace RedBookPlatform.products
{
    public partial class AddScope :Base
    {

       
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string imageurl = "";
            //页面重载 将图片路径重新赋值
            DataTable giftNumInfo = go_giftNumsBusiness.GetListData();
            for(int y=0;y<giftNumInfo.Rows.Count;y++)
            {
                imageurl += giftNumInfo.Rows[y]["imageurl"].ToString()+";";
            }
            string[] imageurls = imageurl.Split(';');
            //删除数据重新添加
            go_giftNumsBusiness.DelListData();
            //获取隐藏域中 区间   Image控件ID
            string scoreNumStr = this.hidInfo.Value;
            string ImageId = this.hidImageId.Value;

            go_giftNumsEntity giftNum = new go_giftNumsEntity();

            string[] scoreNumStrs = scoreNumStr.Split(';');
            string[] ImageIds = ImageId.Split(';');
            
            HttpFileCollection files =Request.Files;
            
            for (int i = 0; i < scoreNumStrs.Length; i++)
            {
                giftNum.GiftNumsId = 1;
                giftNum.Nums =scoreNumStrs.GetValue(i).ToString();
                //图片实体保存到项目内
                if (files[i].ContentLength > 0)
                {
                    string fileName = giftNum.Nums + DateTime.Now.ToString("hhmmss") + Path.GetExtension(files[i].FileName);
                    files[i].SaveAs(Server.MapPath("/Resources/uploads/shopimg") + fileName);
                    giftNum.Imageurl = ("shopimg" + fileName);
                }
                else
                {
                    giftNum.Imageurl = imageurls.GetValue(i).ToString();
                }
                //图片展示ID
                giftNum.ImageID = ImageIds.GetValue(i).ToString();
                go_giftNumsBusiness.SaveEntity(giftNum, true);
            }

            Response.Redirect("AddScope.aspx");
            
               
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DataTable giftNum = go_giftNumsBusiness.GetListData();
                if (giftNum.Rows.Count > 0)
                {
                    for (int i = 0; i < giftNum.Rows.Count; i++)
                    {   
                        this.hidInfo.Value += giftNum.Rows[i]["nums"].ToString()+";";
                        this.hidFile.Value += giftNum.Rows[i]["imageurl"].ToString() + ";";
                        this.hidImageId.Value += giftNum.Rows[i]["imageID"].ToString() + ";";

                    }
                    this.hidInfo.Value=this.hidInfo.Value.TrimEnd(';');
                    this.hidFile.Value = this.hidFile.Value.TrimEnd(';');
                    this.hidImageId.Value = this.hidImageId.Value.TrimEnd(';');
                    
                }
            }
            
        }
    }
}