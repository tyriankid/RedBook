using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 晒单点赞-实体类
	/// </summary>
	[Serializable]
	public  class go_shaidan_zanEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldSd_id = "Sd_id";
		public static string FieldSdhf_userid = "Sdhf_userid";
		public static string FieldSdhf_time = "Sdhf_time";
		public static string FieldStart = "Start";
		public static string FieldCounts = "Counts";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
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
		private DateTime  _sdhf_time;
		public DateTime  Sdhf_time
		{
			get{ return _sdhf_time;}
			set{ _sdhf_time=value;}
		}
		private int  _start;
		public int  Start
		{
			get{ return _start;}
			set{ _start=value;}
		}
		private int  _counts;
		public int  Counts
		{
			get{ return _counts;}
			set{ _counts=value;}
		}
		#endregion

		#region 构造函数
		public go_shaidan_zanEntity(){}

		public go_shaidan_zanEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldSd_id] != DBNull.Value)
			{
			_sd_id = (int )dr[FieldSd_id];
			}
			if (dr[FieldSdhf_userid] != DBNull.Value)
			{
			_sdhf_userid = (int )dr[FieldSdhf_userid];
			}
			if (dr[FieldSdhf_time] != DBNull.Value)
			{
			_sdhf_time = (DateTime )dr[FieldSdhf_time];
			}
			if (dr[FieldStart] != DBNull.Value)
			{
			_start = (int )dr[FieldStart];
			}
			if (dr[FieldCounts] != DBNull.Value)
			{
			_counts = (int )dr[FieldCounts];
			}
		}
		#endregion

	}
}
