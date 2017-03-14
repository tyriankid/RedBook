using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 用户充值记录-实体类
	/// </summary>
	[Serializable]
	public  class go_member_addmoney_recordEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUid = "Uid";
		public static string FieldCode = "Code";
		public static string FieldMoney = "Money";
		public static string FieldPay_type = "Pay_type";
		public static string FieldStatus = "Status";
		public static string FieldTime = "Time";
		public static string FieldScore = "Score";
		public static string FieldBuy_type = "Buy_type";
		public static string FieldTuanid = "Tuanid";
		public static string FieldTuanlistid = "Tuanlistid";
        public static string FieldMemo = "Memo";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _code;
		public string  Code
		{
			get{ return _code;}
			set{ _code=value;}
		}
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _pay_type;
		public string  Pay_type
		{
			get{ return _pay_type;}
			set{ _pay_type=value;}
		}
		private string  _status;
		public string  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private DateTime  _time;
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private decimal  _score;
		public decimal  Score
		{
			get{ return _score;}
			set{ _score=value;}
		}
		private decimal  _buy_type;
		public decimal  Buy_type
		{
			get{ return _buy_type;}
			set{ _buy_type=value;}
		}
		private int  _tuanid;
		public int  Tuanid
		{
			get{ return _tuanid;}
			set{ _tuanid=value;}
		}
		private int  _tuanlistid;
		public int  Tuanlistid
		{
			get{ return _tuanlistid;}
			set{ _tuanlistid=value;}
		}
		#endregion

		#region 构造函数
		public go_member_addmoney_recordEntity(){}

		public go_member_addmoney_recordEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldCode] != DBNull.Value)
			{
			_code = (string )dr[FieldCode];
			}
            if (dr[FieldMemo] != DBNull.Value)
            {
                _code = (string)dr[FieldMemo];
            }
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldPay_type] != DBNull.Value)
			{
			_pay_type = (string )dr[FieldPay_type];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (string )dr[FieldStatus];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldScore] != DBNull.Value)
			{
			_score = (decimal )dr[FieldScore];
			}
			if (dr[FieldBuy_type] != DBNull.Value)
			{
			_buy_type = (decimal )dr[FieldBuy_type];
			}
			if (dr[FieldTuanid] != DBNull.Value)
			{
			_tuanid = (int )dr[FieldTuanid];
			}
			if (dr[FieldTuanlistid] != DBNull.Value)
			{
			_tuanlistid = (int )dr[FieldTuanlistid];
			}
		}
		#endregion

	}
}
