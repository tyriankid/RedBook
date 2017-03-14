using System;
using System.Data;
using System.Collections;
using Newtonsoft.Json;

namespace Taotaole.Model {
	/// <summary>
	/// 商品主表-实体类
	/// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
	public  class go_productsEntity {

		#region 字段名
		public static string FieldProductid = "Productid";
		public static string FieldCategoryid = "Categoryid";
		public static string FieldBrandid = "Brandid";
		public static string FieldTitle = "Title";
		public static string FieldTitle2 = "Title2";
		public static string FieldKeywords = "Keywords";
		public static string FieldDescription = "Description";
		public static string FieldMoney = "Money";
		public static string FieldThumb = "Thumb";
		public static string FieldPicarr = "Picarr";
		public static string FieldTime = "Time";
		public static string FieldTypeid = "Typeid";
		public static string FieldRenqi = "Renqi";
		public static string FieldPos = "Pos";
        public static string FieldBaokuan = "Baokuan";
        public static string FieldTejia = "Tejia";
        public static string FieldYouhui = "Youhui";
		public static string FieldStatus = "Status";
        public static string FieldNewhand= "newhand";
        public static string FeildContents = "Contents";
        public static string FeildStock = "Stock";
        public static string FeildNumber = "number";
        public static string FieldOperation = "Operation";
		#endregion

		#region 属性
		private int  _productid;
        [JsonIgnore]
		public int  Productid
		{
			get{ return _productid;}
			set{ _productid=value;}
		}
		private int  _categoryid;
		public int  Categoryid
		{
			get{ return _categoryid;}
			set{ _categoryid=value;}
		}
		private int  _brandid;
		public int  Brandid
		{
			get{ return _brandid;}
			set{ _brandid=value;}
		}
		private string  _title;
        [JsonIgnore]
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private string  _title2;
        [JsonIgnore]
		public string  Title2
		{
			get{ return _title2;}
			set{ _title2=value;}
		}
		private string  _keywords;
		public string  Keywords
		{
			get{ return _keywords;}
			set{ _keywords=value;}
		}
		private string  _description;
		public string  Description
		{
			get{ return _description;}
			set{ _description=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _thumb;
		public string  Thumb
		{
			get{ return _thumb;}
			set{ _thumb=value;}
		}
		private string  _picarr;
		public string  Picarr
		{
			get{ return _picarr;}
			set{ _picarr=value;}
		}
		private DateTime  _time;
        [JsonIgnore]
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private int  _typeid;
		public int  Typeid
		{
			get{ return _typeid;}
			set{ _typeid=value;}
		}
		private int  _renqi;
        public int Renqi
		{
			get{ return _renqi;}
			set{ _renqi=value;}
		}
        private int _pos;
        public int Pos
		{
			get{ return _pos;}
			set{ _pos=value;}
		}
        private int _baokuan;
        public int Baokuan
        {
            get { return _baokuan; }
            set { _baokuan = value; }
        }
        private int _tejia;
        public int Tejia
        {
            get { return _tejia; }
            set { _tejia = value; }
        }
        private int _youhui;
        public int Youhui
        {
            get { return _youhui; }
            set { _youhui = value; }
        }
        private int _newhand;
        public int NewHand
        {
            get { return _newhand; }
            set { _newhand = value; }
        }


        private int _status;
        public int Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
        private string _contents;
        public string Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        private string  _number;
        public string  Number
        {
            get { return _number; }
            set { _number = value; }
        }
        private int _operation;
        public int Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }
		#endregion

		#region 构造函数
		public go_productsEntity(){}

		public go_productsEntity(DataRow dr)
		{
			if (dr[FieldProductid] != DBNull.Value)
			{
			_productid = (int )dr[FieldProductid];
			}
			if (dr[FieldCategoryid] != DBNull.Value)
			{
			_categoryid = (int )dr[FieldCategoryid];
			}
			if (dr[FieldBrandid] != DBNull.Value)
			{
			_brandid = (int )dr[FieldBrandid];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldTitle2] != DBNull.Value)
			{
			_title2 = (string )dr[FieldTitle2];
			}
			if (dr[FieldKeywords] != DBNull.Value)
			{
			_keywords = (string )dr[FieldKeywords];
			}
			if (dr[FieldDescription] != DBNull.Value)
			{
			_description = (string )dr[FieldDescription];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldThumb] != DBNull.Value)
			{
			_thumb = (string )dr[FieldThumb];
			}
			if (dr[FieldPicarr] != DBNull.Value)
			{
			_picarr = (string )dr[FieldPicarr];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldTypeid] != DBNull.Value)
			{
			_typeid = (int )dr[FieldTypeid];
			}
			if (dr[FieldRenqi] != DBNull.Value)
			{
			_renqi = (int )dr[FieldRenqi];
			}
			if (dr[FieldPos] != DBNull.Value)
			{
			_pos = (int )dr[FieldPos];
			}
            if (dr[FieldBaokuan] != DBNull.Value)
            {
                _baokuan = (int)dr[FieldBaokuan];
            }
            if (dr[FieldTejia] != DBNull.Value)
            {
                _tejia = (int)dr[FieldTejia];
            }
            if (dr[FieldYouhui] != DBNull.Value)
            {
                _youhui= (int)dr[FieldYouhui];
            }

            if (dr[FieldNewhand] != DBNull.Value)
            {
                _newhand = (int)dr[FieldNewhand];
            }
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
            if (dr[FeildContents] != DBNull.Value)
            {
            _contents = (string)dr[FeildContents];
            }
            if (dr[FeildStock] != DBNull.Value)
            {
                _stock = (int)dr[FeildStock];
            }
            if (dr[FeildNumber] != DBNull.Value)
            {
                _number = (string)dr[FeildNumber];
            }
            if (dr[FieldOperation] != DBNull.Value)
            {
                _operation = (int)dr[FieldOperation];
            }
		}
		#endregion

	}
}
