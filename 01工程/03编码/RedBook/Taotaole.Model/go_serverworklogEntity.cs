using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 服务操作日志-实体类
	/// </summary>
	[Serializable]
	public  class go_serverworklogEntity {

		#region 字段名
		public static string FieldLogid = "Logid";
		public static string FieldTablename = "Tablename";
		public static string FieldDataid = "Dataid";
		public static string FieldFuncode = "Funcode";
		public static string FieldAddtime = "Addtime";
		public static string FieldManstate = "Manstate";
		public static string FieldMantime = "Mantime";
		#endregion

		#region 属性
		private Guid  _logid;
		public Guid  Logid
		{
			get{ return _logid;}
			set{ _logid=value;}
		}
		private string  _tablename;
		public string  Tablename
		{
			get{ return _tablename;}
			set{ _tablename=value;}
		}
		private int  _dataid;
		public int  Dataid
		{
			get{ return _dataid;}
			set{ _dataid=value;}
		}
		private string  _funcode;
		public string  Funcode
		{
			get{ return _funcode;}
			set{ _funcode=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private byte  _manstate;
		public byte  Manstate
		{
			get{ return _manstate;}
			set{ _manstate=value;}
		}
		private DateTime  _mantime;
		public DateTime  Mantime
		{
			get{ return _mantime;}
			set{ _mantime=value;}
		}
		#endregion

		#region 构造函数
		public go_serverworklogEntity(){}

		public go_serverworklogEntity(DataRow dr)
		{
			if (dr[FieldLogid] != DBNull.Value)
			{
			_logid = (Guid )dr[FieldLogid];
			}
			if (dr[FieldTablename] != DBNull.Value)
			{
			_tablename = (string )dr[FieldTablename];
			}
			if (dr[FieldDataid] != DBNull.Value)
			{
			_dataid = (int )dr[FieldDataid];
			}
			if (dr[FieldFuncode] != DBNull.Value)
			{
			_funcode = (string )dr[FieldFuncode];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldManstate] != DBNull.Value)
			{
			_manstate = (byte )dr[FieldManstate];
			}
			if (dr[FieldMantime] != DBNull.Value)
			{
			_mantime = (DateTime )dr[FieldMantime];
			}
		}
		#endregion

	}
}
