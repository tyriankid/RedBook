using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 商品分类-实体类
	/// </summary>
	[Serializable]
	public  class go_categoryEntity {

		#region 字段名
		public static string FieldCateid = "Cateid";
		public static string FieldParentid = "Parentid";
		public static string FieldChannel = "Channel";
		public static string FieldModel = "Model";
		public static string FieldName = "Name";
		public static string FieldCatdir = "Catdir";
		public static string FieldPic_url = "Pic_url";
		public static string FieldUrl = "Url";
		public static string FieldInfo = "Info";
		public static string FieldOrders = "Orders";
		#endregion

		#region 属性
		private int  _cateid;
		public int  Cateid
		{
			get{ return _cateid;}
			set{ _cateid=value;}
		}
		private int  _parentid;
		public int  Parentid
		{
			get{ return _parentid;}
			set{ _parentid=value;}
		}
		private decimal  _channel;
		public decimal  Channel
		{
			get{ return _channel;}
			set{ _channel=value;}
		}
		private decimal  _model;
		public decimal  Model
		{
			get{ return _model;}
			set{ _model=value;}
		}
		private string  _name;
		public string  Name
		{
			get{ return _name;}
			set{ _name=value;}
		}
		private string  _catdir;
		public string  Catdir
		{
			get{ return _catdir;}
			set{ _catdir=value;}
		}
		private string  _pic_url;
		public string  Pic_url
		{
			get{ return _pic_url;}
			set{ _pic_url=value;}
		}
		private string  _url;
		public string  Url
		{
			get{ return _url;}
			set{ _url=value;}
		}
		private string  _info;
		public string  Info
		{
			get{ return _info;}
			set{ _info=value;}
		}
		private decimal  _orders;
		public decimal  Orders
		{
			get{ return _orders;}
			set{ _orders=value;}
		}
		#endregion

		#region 构造函数
		public go_categoryEntity(){}

		public go_categoryEntity(DataRow dr)
		{
			if (dr[FieldCateid] != DBNull.Value)
			{
			_cateid = (int )dr[FieldCateid];
			}
			if (dr[FieldParentid] != DBNull.Value)
			{
			_parentid = (int )dr[FieldParentid];
			}
			if (dr[FieldChannel] != DBNull.Value)
			{
			_channel = (decimal )dr[FieldChannel];
			}
			if (dr[FieldModel] != DBNull.Value)
			{
			_model = (decimal )dr[FieldModel];
			}
			if (dr[FieldName] != DBNull.Value)
			{
			_name = (string )dr[FieldName];
			}
			if (dr[FieldCatdir] != DBNull.Value)
			{
			_catdir = (string )dr[FieldCatdir];
			}
			if (dr[FieldPic_url] != DBNull.Value)
			{
			_pic_url = (string )dr[FieldPic_url];
			}
			if (dr[FieldUrl] != DBNull.Value)
			{
			_url = (string )dr[FieldUrl];
			}
			if (dr[FieldInfo] != DBNull.Value)
			{
			_info = (string )dr[FieldInfo];
			}
			if (dr[FieldOrders] != DBNull.Value)
			{
			_orders = (decimal )dr[FieldOrders];
			}
		}
		#endregion

	}
}
