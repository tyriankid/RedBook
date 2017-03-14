using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_channel_recodesEntity {

		#region 字段名
		public static string FieldRid = "Rid";
		public static string FieldUid = "Uid";
		public static string FieldCid = "Cid";
		public static string FieldContent = "Content";
		public static string FieldMoney = "Money";
		public static string FieldTime = "Time";
		public static string FieldRechargemoney = "Rechargemoney";
		public static string FieldState = "State";
		public static string FieldSettlementtime = "Settlementtime";
		public static string FieldOrderid = "Orderid";
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
		private int  _cid;
		public int  Cid
		{
			get{ return _cid;}
			set{ _cid=value;}
		}
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private DateTime  _time;
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private decimal  _rechargemoney;
		public decimal  Rechargemoney
		{
			get{ return _rechargemoney;}
			set{ _rechargemoney=value;}
		}
		private byte  _state;
		public byte  State
		{
			get{ return _state;}
			set{ _state=value;}
		}
		private DateTime  _settlementtime;
		public DateTime  Settlementtime
		{
			get{ return _settlementtime;}
			set{ _settlementtime=value;}
		}
		private string  _orderid;
		public string  Orderid
		{
			get{ return _orderid;}
			set{ _orderid=value;}
		}
		#endregion

		#region 构造函数
		public go_channel_recodesEntity(){}

		public go_channel_recodesEntity(DataRow dr)
		{
			if (dr[FieldRid] != DBNull.Value)
			{
			_rid = (int )dr[FieldRid];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldCid] != DBNull.Value)
			{
			_cid = (int )dr[FieldCid];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldRechargemoney] != DBNull.Value)
			{
			_rechargemoney = (decimal )dr[FieldRechargemoney];
			}
			if (dr[FieldState] != DBNull.Value)
			{
			_state = (byte )dr[FieldState];
			}
			if (dr[FieldSettlementtime] != DBNull.Value)
			{
			_settlementtime = (DateTime )dr[FieldSettlementtime];
			}
			if (dr[FieldOrderid] != DBNull.Value)
			{
			_orderid = (string )dr[FieldOrderid];
			}
		}
		#endregion

	}
}
