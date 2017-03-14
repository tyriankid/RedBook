using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 红包-实体类
	/// </summary>
	[Serializable]
	public  class go_code_configEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldTitle = "Title";
		public static string FieldR_type = "R_type";
		public static string FieldStarttime = "Starttime";
		public static string FieldEndtime = "Endtime";
		public static string FieldSentraisetime = "Sentraisetime";
		public static string FieldWhichweek = "Whichweek";
		public static string FieldRaisetime = "Raisetime";
		public static string FieldTimespans2 = "Timespans2";
		public static string FieldTimespans = "Timespans";
		public static string FieldCount = "Count";
		public static string FieldRestcount = "Restcount";
		public static string FieldSentcount = "Sentcount";
		public static string FieldAmount = "Amount";
		public static string FieldDiscount = "Discount";
		public static string FieldRebate = "Rebate";
		public static string FieldRebatemax = "Rebatemax";
		public static string FieldD_type = "D_type";
		public static string FieldUse_range = "Use_range";
		public static string FieldState = "State";
		public static string FieldAddtime = "Addtime";
		public static string FieldRemark = "Remark";
		public static string FieldSenttime = "Senttime";
		public static string FieldOvertime = "Overtime";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private decimal  _r_type;
		public decimal  R_type
		{
			get{ return _r_type;}
			set{ _r_type=value;}
		}
		private DateTime  _starttime;
		public DateTime  Starttime
		{
			get{ return _starttime;}
			set{ _starttime=value;}
		}
		private DateTime  _endtime;
		public DateTime  Endtime
		{
			get{ return _endtime;}
			set{ _endtime=value;}
		}
		private DateTime  _sentraisetime;
		public DateTime  Sentraisetime
		{
			get{ return _sentraisetime;}
			set{ _sentraisetime=value;}
		}
		private int  _whichweek;
		public int  Whichweek
		{
			get{ return _whichweek;}
			set{ _whichweek=value;}
		}
		private DateTime  _raisetime;
		public DateTime  Raisetime
		{
			get{ return _raisetime;}
			set{ _raisetime=value;}
		}
		private DateTime  _timespans2;
		public DateTime  Timespans2
		{
			get{ return _timespans2;}
			set{ _timespans2=value;}
		}
		private string  _timespans;
		public string  Timespans
		{
			get{ return _timespans;}
			set{ _timespans=value;}
		}
		private int  _count;
		public int  Count
		{
			get{ return _count;}
			set{ _count=value;}
		}
		private int  _restcount;
		public int  Restcount
		{
			get{ return _restcount;}
			set{ _restcount=value;}
		}
		private int  _sentcount;
		public int  Sentcount
		{
			get{ return _sentcount;}
			set{ _sentcount=value;}
		}
		private int  _amount;
		public int  Amount
		{
			get{ return _amount;}
			set{ _amount=value;}
		}
		private int  _discount;
		public int  Discount
		{
			get{ return _discount;}
			set{ _discount=value;}
		}
		private int  _rebate;
		public int  Rebate
		{
			get{ return _rebate;}
			set{ _rebate=value;}
		}
		private int  _rebatemax;
		public int  Rebatemax
		{
			get{ return _rebatemax;}
			set{ _rebatemax=value;}
		}
		private decimal  _d_type;
		public decimal  D_type
		{
			get{ return _d_type;}
			set{ _d_type=value;}
		}
		private string  _use_range;
		public string  Use_range
		{
			get{ return _use_range;}
			set{ _use_range=value;}
		}
		private int  _state;
		public int  State
		{
			get{ return _state;}
			set{ _state=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private string  _remark;
		public string  Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
		}
		private DateTime  _senttime;
		public DateTime  Senttime
		{
			get{ return _senttime;}
			set{ _senttime=value;}
		}
		private DateTime  _overtime;
		public DateTime  Overtime
		{
			get{ return _overtime;}
			set{ _overtime=value;}
		}
		#endregion

		#region 构造函数
		public go_code_configEntity(){}

		public go_code_configEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldR_type] != DBNull.Value)
			{
			_r_type = (decimal )dr[FieldR_type];
			}
			if (dr[FieldStarttime] != DBNull.Value)
			{
			_starttime = (DateTime )dr[FieldStarttime];
			}
			if (dr[FieldEndtime] != DBNull.Value)
			{
			_endtime = (DateTime )dr[FieldEndtime];
			}
			if (dr[FieldSentraisetime] != DBNull.Value)
			{
			_sentraisetime = (DateTime )dr[FieldSentraisetime];
			}
			if (dr[FieldWhichweek] != DBNull.Value)
			{
			_whichweek = (int )dr[FieldWhichweek];
			}
			if (dr[FieldRaisetime] != DBNull.Value)
			{
			_raisetime = (DateTime )dr[FieldRaisetime];
			}
			if (dr[FieldTimespans2] != DBNull.Value)
			{
			_timespans2 = (DateTime )dr[FieldTimespans2];
			}
			if (dr[FieldTimespans] != DBNull.Value)
			{
			_timespans = (string )dr[FieldTimespans];
			}
			if (dr[FieldCount] != DBNull.Value)
			{
			_count = (int )dr[FieldCount];
			}
			if (dr[FieldRestcount] != DBNull.Value)
			{
			_restcount = (int )dr[FieldRestcount];
			}
			if (dr[FieldSentcount] != DBNull.Value)
			{
			_sentcount = (int )dr[FieldSentcount];
			}
			if (dr[FieldAmount] != DBNull.Value)
			{
			_amount = (int )dr[FieldAmount];
			}
			if (dr[FieldDiscount] != DBNull.Value)
			{
			_discount = (int )dr[FieldDiscount];
			}
			if (dr[FieldRebate] != DBNull.Value)
			{
			_rebate = (int )dr[FieldRebate];
			}
			if (dr[FieldRebatemax] != DBNull.Value)
			{
			_rebatemax = (int )dr[FieldRebatemax];
			}
			if (dr[FieldD_type] != DBNull.Value)
			{
			_d_type = (decimal )dr[FieldD_type];
			}
			if (dr[FieldUse_range] != DBNull.Value)
			{
			_use_range = (string )dr[FieldUse_range];
			}
			if (dr[FieldState] != DBNull.Value)
			{
			_state = (int )dr[FieldState];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldRemark] != DBNull.Value)
			{
			_remark = (string )dr[FieldRemark];
			}
			if (dr[FieldSenttime] != DBNull.Value)
			{
			_senttime = (DateTime )dr[FieldSenttime];
			}
			if (dr[FieldOvertime] != DBNull.Value)
			{
			_overtime = (DateTime )dr[FieldOvertime];
			}
		}
		#endregion

	}
}
