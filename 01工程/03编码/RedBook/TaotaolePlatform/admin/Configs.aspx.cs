using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RedBookPlatform.admin
{
    public partial class Configs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            btnAddApp_Click(null, null);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            btnSub_Click(null,null);
        } 
        private bool SaveConfig(string connection1, string connection2)
        {
            bool result;
            //try
            //{
                Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
                System.Web.Configuration.MachineKeySection section = (System.Web.Configuration.MachineKeySection)configuration.GetSection("system.web/machineKey");
                section.ValidationKey = this.CreateKey(20);
                section.DecryptionKey = this.CreateKey(24);
                section.Validation = System.Web.Configuration.MachineKeyValidation.SHA1;
                section.Decryption = "3DES";
                configuration.ConnectionStrings.ConnectionStrings["ConnectString"].ConnectionString = connection1;
                configuration.ConnectionStrings.ConnectionStrings["ConnectString_Branch"].ConnectionString = connection2;
                configuration.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                configuration.Save();
                result = true;
            //}
            //catch (System.Exception exception)
            //{
            //    result = false;
            //}
            return result;
        }
        private string CreateKey(int len)
        {
            byte[] data = new byte[len];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(data);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(string.Format("{0:X2}", data[i]));
            }
            return builder.ToString();
        }

        //使用 DPAPI 加密 appSettings 
        protected void btnAddApp_Click(object sender, EventArgs e)
        {
            //Request.ApplicationPath 
            //获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径。 
            //当前应用程序的虚拟路径。 
            //开启 Request.ApplicationPath 应用程序所在的 web.config 文件 
            Configuration config = WebConfigurationManager.
                  OpenWebConfiguration(Request.ApplicationPath);
            //获取 web.config 中的 appSettings 区块 
            ConfigurationSection configSection =
                config.GetSection("appSettings");
            //如果这个区块还没有被加密 
            if (!configSection.SectionInformation.IsProtected)
            {
                //进行加密操作 
                configSection.SectionInformation.
                    ProtectSection("DataProtectionConfigurationProvider");
                //将加密的结果保存回 web.config 文件中 
                config.Save();
                //lblMsg.Text = "AppSettings 使用 DPAPI 加密成功!!!";
            }
            else
            {
                //lblMsg.Text = "AppSettings " +"已经被 DPAPI 加密了，此次加密操作被取消!!!";
            }
        }

        //使用 DPAPI 加密 connectionStrings 
        protected void btnAddCon_Click(object sender, EventArgs e)
        {
            //Request.ApplicationPath 
            //获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径。 
            //当前应用程序的虚拟路径。 
            //开启 Request.ApplicationPath 应用程序所在的 web.config 文件 
            Configuration config = WebConfigurationManager.
                  OpenWebConfiguration(Request.ApplicationPath);
            //获取 web.config 中的 appSettings 区块 
            ConfigurationSection configSection =
                config.GetSection("connectionStrings");
            //如果这个区块还没有被加密 
            if (!configSection.SectionInformation.IsProtected)
            {
                //进行加密操作 
                configSection.SectionInformation.
                    ProtectSection("DataProtectionConfigurationProvider");
                //将加密的结果保存回 web.config 文件中 
                config.Save();
                //lblMsg.Text = "ConnectionStrings 使用 DPAPI 加密成功!!!";
            }
            else
            {
                //lblMsg.Text = "ConnectionStrings " + "已经被 DPAPI 加密了，此次加密操作被取消!!!";
            }
        }

        //进行解密 
        protected void btnSub_Click(object sender, EventArgs e)
        {
            Configuration config = WebConfigurationManager.
                OpenWebConfiguration(Request.ApplicationPath);
            ConfigurationSection configAppSection =
                config.GetSection("appSettings");
            if (configAppSection.SectionInformation.IsProtected)
            {
                //在解密时，并不需要区分是 DPAPI 加密的还是 RSA 加密的 
                //其均会自行解密 
                configAppSection.SectionInformation.UnprotectSection();
                config.Save();
                //lblMsg.Text = "解密成功!!!";
            }
            else
            {
                //lblMsg.Text = "该区块暂时还没有被加密，所以无需解密!!!";
            }

            ConfigurationSection configConSection =
                config.GetSection("connectionStrings");

            if (configConSection.SectionInformation.IsProtected)
            {
                configConSection.SectionInformation.UnprotectSection();
                config.Save();
                //lblMsg.Text = "解密成功!!!";
            }
            else
            {
                //lblMsg.Text = "该区块暂时还没有被加密，所以无需解密!!!";
            }
        }

        


    }
}