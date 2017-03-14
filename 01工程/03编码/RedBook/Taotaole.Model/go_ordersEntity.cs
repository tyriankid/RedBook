using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 订单-实体类
	/// </summary>
	[Serializable]
	public  class go_ordersEntity {

		#region 字段名
		public static string FieldOrderId = "OrderId";
		public static string FieldUid = "Uid";
		public static string FieldOrdertype = "Ordertype";
		public static string FieldBusinessId = "BusinessId";
		public static string FieldQuantity = "Quantity";
		public static string FieldGoucode = "Goucode";
		public static string FieldMoney = "Money";
		public static string FieldIswon = "Iswon";
		public static string FieldPay_type = "Pay_type";
		public static string FieldIp = "Ip";
		public static string FieldStatus = "Status";
		public static string FieldTime = "Time";
		public static string FieldExpress_company = "Express_company";
		public static string FieldExpress_code = "Express_code";
		public static string FieldSendtime = "Sendtime";
		public static string FieldCard_use_type = "Card_use_type";
		public static string FieldOrder_address_info = "Order_address_info";
		public static string FieldPhone = "Phone";
		#endregion

		#region 属性
		private string  _orderId;
		public string  OrderId
		{
			get{ return _orderId;}
			set{ _orderId=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _ordertype;
		public string  Ordertype
		{
			get{ return _ordertype;}
			set{ _ordertype=value;}
		}
		private int  _businessId;
		public int  BusinessId
		{
			get{ return _businessId;}
			set{ _businessId=value;}
		}
		private int  _quantity;
		public int  Quantity
		{
			get{ return _quantity;}
			set{ _quantity=value;}
		}
		private string  _goucode;
		public string  Goucode
		{
			get{ return _goucode;}
			set{ _goucode=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private int  _iswon;
		public int  Iswon
		{
			get{ return _iswon;}
			set{ _iswon=value;}
		}
		private string  _pay_type;
		public string  Pay_type
		{
			get{ return _pay_type;}
			set{ _pay_type=value;}
		}
		private string  _ip;
		public string  Ip
		{
			get{ return _ip;}
			set{ _ip=value;}
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
		private string  _express_company;
		public string  Express_company
		{
			get{ return _express_company;}
			set{ _express_company=value;}
		}
		private string  _express_code;
		public string  Express_code
		{
			get{ return _express_code;}
			set{ _express_code=value;}
		}
		private DateTime  _sendtime;
		public DateTime  Sendtime
		{
			get{ return _sendtime;}
			set{ _sendtime=value;}
		}
		private int  _card_use_type;
		public int  Card_use_type
		{
			get{ return _card_use_type;}
			set{ _card_use_type=value;}
		}
		private string  _order_address_info;
		public string  Order_address_info
		{
			get{ return _order_address_info;}
			set{ _order_address_info=value;}
		}
		private string  _phone;
		public string  Phone
		{
			get{ return _phone;}
			set{ _phone=value;}
		}
		#endregion

		#region 构造函数
		public go_ordersEntity(){}

		public go_ordersEntity(DataRow dr)
		{
			if (dr[FieldOrderId] != DBNull.Value)
			{
			_orderId = (string )dr[FieldOrderId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldOrdertype] != DBNull.Value)
			{
			_ordertype = (string )dr[FieldOrdertype];
			}
			if (dr[FieldBusinessId] != DBNull.Value)
			{
			_businessId = (int )dr[FieldBusinessId];
			}
			if (dr[FieldQuantity] != DBNull.Value)
			{
			_quantity = (int )dr[FieldQuantity];
			}
			if (dr[FieldGoucode] != DBNull.Value)
			{
			_goucode = (string )dr[FieldGoucode];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldIswon] != DBNull.Value)
			{
			_iswon = (int )dr[FieldIswon];
			}
			if (dr[FieldPay_type] != DBNull.Value)
			{
			_pay_type = (string )dr[FieldPay_type];
			}
			if (dr[FieldIp] != DBNull.Value)
			{
			_ip = (string )dr[FieldIp];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (string )dr[FieldStatus];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldExpress_company] != DBNull.Value)
			{
			_express_company = (string )dr[FieldExpress_company];
			}
			if (dr[FieldExpress_code] != DBNull.Value)
			{
			_express_code = (string )dr[FieldExpress_code];
			}
			if (dr[FieldSendtime] != DBNull.Value)
			{
			_sendtime = (DateTime )dr[FieldSendtime];
			}
			if (dr[FieldCard_use_type] != DBNull.Value)
			{
			_card_use_type = (int )dr[FieldCard_use_type];
			}
			if (dr[FieldOrder_address_info] != DBNull.Value)
			{
			_order_address_info = (string )dr[FieldOrder_address_info];
			}
			if (dr[FieldPhone] != DBNull.Value)
			{
			_phone = (string )dr[FieldPhone];
			}
		}
		#endregion

	}
}
