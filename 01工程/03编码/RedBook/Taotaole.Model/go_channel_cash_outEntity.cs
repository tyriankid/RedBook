using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 推广提现明细-实体类
	/// </summary>
	[Serializable]
	public  class go_channel_cash_outEntity {

		#region 字段名
		public static string FieldRid = "Rid";
		public static string FieldUid = "Uid";
		public static string FieldUsername = "Username";
		public static string FieldBankname = "Bankname";
		public static string FieldBranch = "Branch";
		public static string FieldMoney = "Money";
		public static string FieldTime = "Time";
		public static string FieldBanknumber = "Banknumber";
		public static string FieldLinkphone = "Linkphone";
		public static string FieldAuditstatus = "Auditstatus";
		public static string FieldProcefees = "Procefees";
		public static string FieldReviewtime = "Reviewtime";
		public static string FieldReason = "Reason";
		#endregion

		#region 属性
		private int  _rid;
		public int  Rid
		{
			get{ return _rid;}
			set{ _rid=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _username;
		public string  Username
		{
			get{ return _username;}
			set{ _username=value;}
		}
		private string  _bankname;
		public string  Bankname
		{
			get{ return _bankname;}
			set{ _bankname=value;}
		}
		private string  _branch;
		public string  Branch
		{
			get{ return _branch;}
			set{ _branch=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _time;
		public string  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private string  _banknumber;
		public string  Banknumber
		{
			get{ return _banknumber;}
			set{ _banknumber=value;}
		}
		private string  _linkphone;
		public string  Linkphone
		{
			get{ return _linkphone;}
			set{ _linkphone=value;}
		}
		private decimal  _auditstatus;
		public decimal  Auditstatus
		{
			get{ return _auditstatus;}
			set{ _auditstatus=value;}
		}
		private decimal  _procefees;
		public decimal  Procefees
		{
			get{ return _procefees;}
			set{ _procefees=value;}
		}
		private DateTime  _reviewtime;
		public DateTime  Reviewtime
		{
			get{ return _reviewtime;}
			set{ _reviewtime=value;}
		}
		private string  _reason;
		public string  Reason
		{
			get{ return _reason;}
			set{ _reason=value;}
		}
		#endregion

		#region 构造函数
		public go_channel_cash_outEntity(){}

		public go_channel_cash_outEntity(DataRow dr)
		{
			if (dr[FieldRid] != DBNull.Value)
			{
			_rid = (int )dr[FieldRid];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldUsername] != DBNull.Value)
			{
			_username = (string )dr[FieldUsername];
			}
			if (dr[FieldBankname] != DBNull.Value)
			{
			_bankname = (string )dr[FieldBankname];
			}
			if (dr[FieldBranch] != DBNull.Value)
			{
			_branch = (string )dr[FieldBranch];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (string )dr[FieldTime];
			}
			if (dr[FieldBanknumber] != DBNull.Value)
			{
			_banknumber = (string )dr[FieldBanknumber];
			}
			if (dr[FieldLinkphone] != DBNull.Value)
			{
			_linkphone = (string )dr[FieldLinkphone];
			}
			if (dr[FieldAuditstatus] != DBNull.Value)
			{
			_auditstatus = (decimal )dr[FieldAuditstatus];
			}
			if (dr[FieldProcefees] != DBNull.Value)
			{
			_procefees = (decimal )dr[FieldProcefees];
			}
			if (dr[FieldReviewtime] != DBNull.Value)
			{
			_reviewtime = (DateTime )dr[FieldReviewtime];
			}
			if (dr[FieldReason] != DBNull.Value)
			{
			_reason = (string )dr[FieldReason];
			}
		}
		#endregion

	}
}
