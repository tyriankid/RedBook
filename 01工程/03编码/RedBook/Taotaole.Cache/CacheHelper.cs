using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Taotaole.Cache
{
    public class CacheHelper
    {
        private static ArrayList al = new ArrayList();
        public CacheHelper() 
        {
            
        }

        public static bool IsOK(string cacheID)
        {
            bool b = true;
            try
            {
                al.Add("127.0.0.1:11211");
                b = MemberHelper.AddCacheMs(al, "mc", cacheID, "1", 1);
                //Globals.DebugLogger("频繁调用接口【userpaymentsubmit】_" + uid, "API_Log.txt");
            }
            catch {
                //Globals.DebugLogger("缓存添加失败：" + e.Message, "API_Log.txt");
            }
            return true;
            //return b;
        }

        public static void DelCache(string cacheID)
        {
            try
            {
                al.Add("127.0.0.1:11211");
                MemberHelper.DelCache(al, "mc", cacheID);
                //Globals.DebugLogger("缓存移出成功", "API_Log.txt");
            }
            catch {
                //Globals.DebugLogger("缓存移出失败：" + e.Message, "API_Log.txt");
            }
        }


    }
}
