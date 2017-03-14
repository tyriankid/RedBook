using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 团购参团记录-实体类
	/// </summary>
	[Serializable]
	public  class go_tuan_listinfoEntity {

		#region 字段名
		public static string FieldTuanlistId = "TuanlistId";
		public static string FieldTuanId = "TuanId";
		public static string FieldAddtime = "Addtime";
		public static string FieldEndtime = "Endtime";
		public static string FieldUid = "Uid";
		public static string FieldTotal_num = "Total_num";
		public static string FieldInvolvement_num = "Involvement_num";
		public static string FieldRemain_num = "Remain_num";
		public static string FieldWinners_id = "Winners_id";
		public static string FieldWinning_code = "Winning_code";
		public static string FieldQ_end_time = "Q_end_time";
		public static string FieldQ_showtime = "Q_showtime";
		public static string FieldAssign_winners = "Assign_winners";
		public static string FieldStatus = "Status";
		public static string FieldCodetable = "Codetable";
		#endregion

		#region 属性
		private int  _tuanlistId;
		public int  TuanlistId
		{
			get{ return _tuanlistId;}
			set{ _tuanlistId=value;}
		}
		private int  _tuanId;
		public int  TuanId
		{
			get{ return _tuanId;}
			set{ _tuanId=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private DateTime  _endtime;
		public DateTime  Endtime
		{
			get{ return _endtime;}
			set{ _endtime=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private int  _total_num;
		public int  Total_num
		{
			get{ return _total_num;}
			set{ _total_num=value;}
		}
		private int  _involvement_num;
		public int  Involvement_num
		{
			get{ return _involvement_num;}
			set{ _involvement_num=value;}
		}
		private int  _remain_num;
		public int  Remain_num
		{
			get{ return _remain_num;}
			set{ _remain_num=value;}
		}
		private int  _winners_id;
		public int  Winners_id
		{
			get{ return _winners_id;}
			set{ _winners_id=value;}
		}
		private string  _winning_code;
		public string  Winning_code
		{
			get{ return _winning_code;}
			set{ _winning_code=value;}
		}
		private DateTime  _q_end_time;
		public DateTime  Q_end_time
		{
			get{ return _q_end_time;}
			set{ _q_end_time=value;}
		}
		private string  _q_showtime;
		public string  Q_showtime
		{
			get{ return _q_showtime;}
			set{ _q_showtime=value;}
		}
		private int  _assign_winners;
		public int  Assign_winners
		{
			get{ return _assign_winners;}
			set{ _assign_winners=value;}
		}
		private int  _status;
		public int  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private string  _codetable;
		public string  Codetable
		{
			get{ return _codetable;}
			set{ _codetable=value;}
		}
		#endregion

		#region 构造函数
		public go_tuan_listinfoEntity(){}

		public go_tuan_listinfoEntity(DataRow dr)
		{
			if (dr[FieldTuanlistId] != DBNull.Value)
			{
			_tuanlistId = (int )dr[FieldTuanlistId];
			}
			if (dr[FieldTuanId] != DBNull.Value)
			{
			_tuanId = (int )dr[FieldTuanId];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldEndtime] != DBNull.Value)
			{
			_endtime = (DateTime )dr[FieldEndtime];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldTotal_num] != DBNull.Value)
			{
			_total_num = (int )dr[FieldTotal_num];
			}
			if (dr[FieldInvolvement_num] != DBNull.Value)
			{
			_involvement_num = (int )dr[FieldInvolvement_num];
			}
			if (dr[FieldRemain_num] != DBNull.Value)
			{
			_remain_num = (int )dr[FieldRemain_num];
			}
			if (dr[FieldWinners_id] != DBNull.Value)
			{
			_winners_id = (int )dr[FieldWinners_id];
			}
			if (dr[FieldWinning_code] != DBNull.Value)
			{
			_winning_code = (string )dr[FieldWinning_code];
			}
			if (dr[FieldQ_end_time] != DBNull.Value)
			{
			_q_end_time = (DateTime )dr[FieldQ_end_time];
			}
			if (dr[FieldQ_showtime] != DBNull.Value)
			{
			_q_showtime = (string )dr[FieldQ_showtime];
			}
			if (dr[FieldAssign_winners] != DBNull.Value)
			{
			_assign_winners = (int )dr[FieldAssign_winners];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
			if (dr[FieldCodetable] != DBNull.Value)
			{
			_codetable = (string )dr[FieldCodetable];
			}
		}
		#endregion

	}
}
