using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 晒单-实体类
	/// </summary>
	[Serializable]
	public  class go_shaidanEntity {

		#region 字段名
		public static string FieldSd_id = "Sd_id";
		public static string FieldSd_userid = "Sd_userid";
		public static string FieldSd_orderid = "Sd_orderid";
		public static string FieldSd_qishu = "Sd_qishu";
		public static string FieldSd_ip = "Sd_ip";
		public static string FieldSd_title = "Sd_title";
		public static string FieldSd_thumbs = "Sd_thumbs";
		public static string FieldSd_content = "Sd_content";
		public static string FieldSd_photolist = "Sd_photolist";
		public static string FieldSd_zan = "Sd_zan";
		public static string FieldSd_ping = "Sd_ping";
		public static string FieldSd_time = "Sd_time";
		#endregion

		#region 属性
		private int  _sd_id;
		public int  Sd_id
		{
			get{ return _sd_id;}
			set{ _sd_id=value;}
		}
		private int  _sd_userid;
		public int  Sd_userid
		{
			get{ return _sd_userid;}
			set{ _sd_userid=value;}
		}
		private string  _sd_orderid;
		public string  Sd_orderid
		{
			get{ return _sd_orderid;}
			set{ _sd_orderid=value;}
		}
		private int  _sd_qishu;
		public int  Sd_qishu
		{
			get{ return _sd_qishu;}
			set{ _sd_qishu=value;}
		}
		private string  _sd_ip;
		public string  Sd_ip
		{
			get{ return _sd_ip;}
			set{ _sd_ip=value;}
		}
		private string  _sd_title;
		public string  Sd_title
		{
			get{ return _sd_title;}
			set{ _sd_title=value;}
		}
		private string  _sd_thumbs;
		public string  Sd_thumbs
		{
			get{ return _sd_thumbs;}
			set{ _sd_thumbs=value;}
		}
		private string  _sd_content;
		public string  Sd_content
		{
			get{ return _sd_content;}
			set{ _sd_content=value;}
		}
		private string  _sd_photolist;
		public string  Sd_photolist
		{
			get{ return _sd_photolist;}
			set{ _sd_photolist=value;}
		}
		private int  _sd_zan;
		public int  Sd_zan
		{
			get{ return _sd_zan;}
			set{ _sd_zan=value;}
		}
		private int  _sd_ping;
		public int  Sd_ping
		{
			get{ return _sd_ping;}
			set{ _sd_ping=value;}
		}
		private DateTime  _sd_time;
		public DateTime  Sd_time
		{
			get{ return _sd_time;}
			set{ _sd_time=value;}
		}
		#endregion

		#region 构造函数
		public go_shaidanEntity(){}

		public go_shaidanEntity(DataRow dr)
		{
			if (dr[FieldSd_id] != DBNull.Value)
			{
			_sd_id = (int )dr[FieldSd_id];
			}
			if (dr[FieldSd_userid] != DBNull.Value)
			{
			_sd_userid = (int )dr[FieldSd_userid];
			}
			if (dr[FieldSd_orderid] != DBNull.Value)
			{
			_sd_orderid = (string )dr[FieldSd_orderid];
			}
			if (dr[FieldSd_qishu] != DBNull.Value)
			{
			_sd_qishu = (int )dr[FieldSd_qishu];
			}
			if (dr[FieldSd_ip] != DBNull.Value)
			{
			_sd_ip = (string )dr[FieldSd_ip];
			}
			if (dr[FieldSd_title] != DBNull.Value)
			{
			_sd_title = (string )dr[FieldSd_title];
			}
			if (dr[FieldSd_thumbs] != DBNull.Value)
			{
			_sd_thumbs = (string )dr[FieldSd_thumbs];
			}
			if (dr[FieldSd_content] != DBNull.Value)
			{
			_sd_content = (string )dr[FieldSd_content];
			}
			if (dr[FieldSd_photolist] != DBNull.Value)
			{
			_sd_photolist = (string )dr[FieldSd_photolist];
			}
			if (dr[FieldSd_zan] != DBNull.Value)
			{
			_sd_zan = (int )dr[FieldSd_zan];
			}
			if (dr[FieldSd_ping] != DBNull.Value)
			{
			_sd_ping = (int )dr[FieldSd_ping];
			}
			if (dr[FieldSd_time] != DBNull.Value)
			{
			_sd_time = (DateTime )dr[FieldSd_time];
			}
		}
		#endregion

	}
}
