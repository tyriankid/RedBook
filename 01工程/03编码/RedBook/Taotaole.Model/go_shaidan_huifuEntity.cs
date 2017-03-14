using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 晒单回复-实体类
	/// </summary>
	[Serializable]
	public  class go_shaidan_huifuEntity {

		#region 字段名
		public static string FieldSdhf_id = "Sdhf_id";
		public static string FieldSd_id = "Sd_id";
		public static string FieldSdhf_userid = "Sdhf_userid";
		public static string FieldSdhf_content = "Sdhf_content";
		public static string FieldSdhf_time = "Sdhf_time";
		#endregion

		#region 属性
		private int  _sdhf_id;
		public int  Sdhf_id
		{
			get{ return _sdhf_id;}
			set{ _sdhf_id=value;}
		}
		private int  _sd_id;
		public int  Sd_id
		{
			get{ return _sd_id;}
			set{ _sd_id=value;}
		}
		private int  _sdhf_userid;
		public int  Sdhf_userid
		{
			get{ return _sdhf_userid;}
			set{ _sdhf_userid=value;}
		}
		private string  _sdhf_content;
		public string  Sdhf_content
		{
			get{ return _sdhf_content;}
			set{ _sdhf_content=value;}
		}
		private DateTime  _sdhf_time;
		public DateTime  Sdhf_time
		{
			get{ return _sdhf_time;}
			set{ _sdhf_time=value;}
		}
		#endregion

		#region 构造函数
		public go_shaidan_huifuEntity(){}

		public go_shaidan_huifuEntity(DataRow dr)
		{
			if (dr[FieldSdhf_id] != DBNull.Value)
			{
			_sdhf_id = (int )dr[FieldSdhf_id];
			}
			if (dr[FieldSd_id] != DBNull.Value)
			{
			_sd_id = (int )dr[FieldSd_id];
			}
			if (dr[FieldSdhf_userid] != DBNull.Value)
			{
			_sdhf_userid = (int )dr[FieldSdhf_userid];
			}
			if (dr[FieldSdhf_content] != DBNull.Value)
			{
			_sdhf_content = (string )dr[FieldSdhf_content];
			}
			if (dr[FieldSdhf_time] != DBNull.Value)
			{
			_sdhf_time = (DateTime )dr[FieldSdhf_time];
			}
		}
		#endregion

	}
}
