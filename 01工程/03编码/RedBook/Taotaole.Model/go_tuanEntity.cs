using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 团购商品-实体类
	/// </summary>
	[Serializable]
	public  class go_tuanEntity {

		#region 字段名
		public static string FieldTId = "TId";
		public static string FieldProductid = "Productid";
		public static string FieldTitle = "Title";
		public static string FieldPer_price = "Per_price";
		public static string FieldTotal_num = "Total_num";
		public static string FieldMax_sell = "Max_sell";
		public static string FieldLose_time = "Lose_time";
		public static string FieldStart_time = "Start_time";
		public static string FieldEnd_time = "End_time";
		public static string FieldDeadline = "Deadline";
		public static string FieldSort = "Sort";
		public static string FieldIs_delete = "Is_delete";
		public static string FieldReachtuan_num = "Reachtuan_num";
		public static string FieldPrize_num = "Prize_num";
		public static string FieldSell_num = "Sell_num";
		public static string FieldTime = "Time";
		public static string FieldStatus = "Status";
		#endregion

		#region 属性
		private int  _tId;
		public int  TId
		{
			get{ return _tId;}
			set{ _tId=value;}
		}
		private int  _productid;
		public int  Productid
		{
			get{ return _productid;}
			set{ _productid=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private decimal  _per_price;
		public decimal  Per_price
		{
			get{ return _per_price;}
			set{ _per_price=value;}
		}
		private int  _total_num;
		public int  Total_num
		{
			get{ return _total_num;}
			set{ _total_num=value;}
		}
		private int  _max_sell;
		public int  Max_sell
		{
			get{ return _max_sell;}
			set{ _max_sell=value;}
		}
		private DateTime  _lose_time;
		public DateTime  Lose_time
		{
			get{ return _lose_time;}
			set{ _lose_time=value;}
		}
		private DateTime  _start_time;
		public DateTime  Start_time
		{
			get{ return _start_time;}
			set{ _start_time=value;}
		}
		private DateTime  _end_time;
		public DateTime  End_time
		{
			get{ return _end_time;}
			set{ _end_time=value;}
		}
		private DateTime  _deadline;
		public DateTime  Deadline
		{
			get{ return _deadline;}
			set{ _deadline=value;}
		}
		private int  _sort;
		public int  Sort
		{
			get{ return _sort;}
			set{ _sort=value;}
		}
		private int  _is_delete;
		public int  Is_delete
		{
			get{ return _is_delete;}
			set{ _is_delete=value;}
		}
		private int  _reachtuan_num;
		public int  Reachtuan_num
		{
			get{ return _reachtuan_num;}
			set{ _reachtuan_num=value;}
		}
		private int  _prize_num;
		public int  Prize_num
		{
			get{ return _prize_num;}
			set{ _prize_num=value;}
		}
		private int  _sell_num;
		public int  Sell_num
		{
			get{ return _sell_num;}
			set{ _sell_num=value;}
		}
		private DateTime  _time;
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private int  _status;
		public int  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		#endregion

		#region 构造函数
		public go_tuanEntity(){}

		public go_tuanEntity(DataRow dr)
		{
			if (dr[FieldTId] != DBNull.Value)
			{
			_tId = (int )dr[FieldTId];
			}
			if (dr[FieldProductid] != DBNull.Value)
			{
			_productid = (int )dr[FieldProductid];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldPer_price] != DBNull.Value)
			{
			_per_price = (decimal )dr[FieldPer_price];
			}
			if (dr[FieldTotal_num] != DBNull.Value)
			{
			_total_num = (int )dr[FieldTotal_num];
			}
			if (dr[FieldMax_sell] != DBNull.Value)
			{
			_max_sell = (int )dr[FieldMax_sell];
			}
			if (dr[FieldLose_time] != DBNull.Value)
			{
			_lose_time = (DateTime )dr[FieldLose_time];
			}
			if (dr[FieldStart_time] != DBNull.Value)
			{
			_start_time = (DateTime )dr[FieldStart_time];
			}
			if (dr[FieldEnd_time] != DBNull.Value)
			{
			_end_time = (DateTime )dr[FieldEnd_time];
			}
			if (dr[FieldDeadline] != DBNull.Value)
			{
			_deadline = (DateTime )dr[FieldDeadline];
			}
			if (dr[FieldSort] != DBNull.Value)
			{
			_sort = (int )dr[FieldSort];
			}
			if (dr[FieldIs_delete] != DBNull.Value)
			{
			_is_delete = (int )dr[FieldIs_delete];
			}
			if (dr[FieldReachtuan_num] != DBNull.Value)
			{
			_reachtuan_num = (int )dr[FieldReachtuan_num];
			}
			if (dr[FieldPrize_num] != DBNull.Value)
			{
			_prize_num = (int )dr[FieldPrize_num];
			}
			if (dr[FieldSell_num] != DBNull.Value)
			{
			_sell_num = (int )dr[FieldSell_num];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
		}
		#endregion

	}
}
