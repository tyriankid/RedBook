using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 百度推广-实体类
	/// </summary>
	[Serializable]
	public  class go_baidu_cfgEntity {

		#region 字段名
		public static string FieldCfgid = "Cfgid";
		public static string FieldCfgoriginalid = "Cfgoriginalid";
		public static string FieldIsdelete = "Isdelete";
		public static string FieldCfgtype = "Cfgtype";
		public static string FieldCodequantitys = "Codequantitys";
        public static string FieldBuytimes = "Buytimes";
		#endregion

		#region 属性
		private int  _cfgid;
		public int  Cfgid
		{
			get{ return _cfgid;}
			set{ _cfgid=value;}
		}
		private int  _cfgoriginalid;
		public int  Cfgoriginalid
		{
			get{ return _cfgoriginalid;}
			set{ _cfgoriginalid=value;}
		}
		private int  _isdelete;
		public int  Isdelete
		{
			get{ return _isdelete;}
			set{ _isdelete=value;}
		}
		private int  _cfgtype;
		public int  Cfgtype
		{
			get{ return _cfgtype;}
			set{ _cfgtype=value;}
		}
		private string  _codequantitys;
		public string  Codequantitys
		{
			get{ return _codequantitys;}
			set{ _codequantitys=value;}
		}
        private string _buytimes;
        public string Buytimes
        {
            get { return _buytimes; }
            set { _buytimes = value; }
        }
		#endregion

		#region 构造函数
		public go_baidu_cfgEntity(){}

		public go_baidu_cfgEntity(DataRow dr)
		{
			if (dr[FieldCfgid] != DBNull.Value)
			{
			_cfgid = (int )dr[FieldCfgid];
			}
			if (dr[FieldCfgoriginalid] != DBNull.Value)
			{
			_cfgoriginalid = (int )dr[FieldCfgoriginalid];
			}
			if (dr[FieldIsdelete] != DBNull.Value)
			{
			_isdelete = (int )dr[FieldIsdelete];
			}
			if (dr[FieldCfgtype] != DBNull.Value)
			{
			_cfgtype = (int )dr[FieldCfgtype];
			}
			if (dr[FieldCodequantitys] != DBNull.Value)
			{
			_codequantitys = (string )dr[FieldCodequantitys];
			}
            if (dr[FieldBuytimes] != DBNull.Value)
            {
                _buytimes = (string)dr[FieldBuytimes];
            }
		}
		#endregion

	}
}
