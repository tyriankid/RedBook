using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 数据库计数-实体类
	/// </summary>
	[Serializable]
	public  class go_configsEntity {

		#region 字段名
		public static string FieldCid = "Cid";
		public static string FieldCtype = "Ctype";
		public static string FieldCcount = "Ccount";
		public static string FieldCtime = "Ctime";
		public static string FieldCcode = "Ccode";
		#endregion

		#region 属性
		private int  _cid;
		public int  Cid
		{
			get{ return _cid;}
			set{ _cid=value;}
		}
		private string  _ctype;
		public string  Ctype
		{
			get{ return _ctype;}
			set{ _ctype=value;}
		}
		private int  _ccount;
		public int  Ccount
		{
			get{ return _ccount;}
			set{ _ccount=value;}
		}
		private DateTime  _ctime;
		public DateTime  Ctime
		{
			get{ return _ctime;}
			set{ _ctime=value;}
		}
		private string  _ccode;
		public string  Ccode
		{
			get{ return _ccode;}
			set{ _ccode=value;}
		}
		#endregion

		#region 构造函数
		public go_configsEntity(){}

		public go_configsEntity(DataRow dr)
		{
			if (dr[FieldCid] != DBNull.Value)
			{
			_cid = (int )dr[FieldCid];
			}
			if (dr[FieldCtype] != DBNull.Value)
			{
			_ctype = (string )dr[FieldCtype];
			}
			if (dr[FieldCcount] != DBNull.Value)
			{
			_ccount = (int )dr[FieldCcount];
			}
			if (dr[FieldCtime] != DBNull.Value)
			{
			_ctime = (DateTime )dr[FieldCtime];
			}
			if (dr[FieldCcode] != DBNull.Value)
			{
			_ccode = (string )dr[FieldCcode];
			}
		}
		#endregion

	}
}
