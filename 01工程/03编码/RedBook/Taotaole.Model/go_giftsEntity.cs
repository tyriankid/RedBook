using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 积分商城-实体类
	/// </summary>
	[Serializable]
	public  class go_giftsEntity {

		#region 字段名
		public static string FieldGiftId = "GiftId";
		public static string FieldProductid = "Productid";
		public static string FieldTitle = "Title";
		public static string FieldNeedScore = "NeedScore";
		public static string FieldScope = "Scope";
		public static string FieldProbability = "Probability";
		public static string FieldAddtime = "Addtime";
		public static string FieldStock = "Stock";
		public static string FieldPrizeNumber = "PrizeNumber";
		public static string FieldGiftsuse = "Giftsuse";
        public static string FieldWinCode = "winCode";
        public static string FieldSumCode = "sumCode";
        public static string FieldCodeCount = "codeCount";
		#endregion

		#region 属性
		private int  _giftId;
		public int  GiftId
		{
			get{ return _giftId;}
			set{ _giftId=value;}
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
		private int  _needScore;
		public int  NeedScore
		{
			get{ return _needScore;}
			set{ _needScore=value;}
		}
		private int  _scope;
		public int  Scope
		{
			get{ return _scope;}
			set{ _scope=value;}
		}
		private int  _probability;
		public int  Probability
		{
			get{ return _probability;}
			set{ _probability=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private string  _stock;
		public string  Stock
		{
			get{ return _stock;}
			set{ _stock=value;}
		}
		private string  _prizeNumber;
		public string  PrizeNumber
		{
			get{ return _prizeNumber;}
			set{ _prizeNumber=value;}
		}
		private string  _giftsuse;
		public string  Giftsuse
		{
			get{ return _giftsuse;}
			set{ _giftsuse=value;}
		}
        private string _sumcode;
        public string sumCode
        {
            get { return _sumcode; }
            set { _sumcode = value; }
        }
        private string _wincode;
        public string winCode
        {
            get { return _wincode; }
            set { _wincode = value; }
        }
        private int _codeCount;
        public int codeCount
        {
            get { return _codeCount; }
            set { _codeCount = value; }
        }
		#endregion

		#region 构造函数
		public go_giftsEntity(){}

		public go_giftsEntity(DataRow dr)
		{
			if (dr[FieldGiftId] != DBNull.Value)
			{
			_giftId = (int )dr[FieldGiftId];
			}
			if (dr[FieldProductid] != DBNull.Value)
			{
			_productid = (int )dr[FieldProductid];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldNeedScore] != DBNull.Value)
			{
			_needScore = (int )dr[FieldNeedScore];
			}
			if (dr[FieldScope] != DBNull.Value)
			{
			_scope = (int )dr[FieldScope];
			}
			if (dr[FieldProbability] != DBNull.Value)
			{
			_probability = (int )dr[FieldProbability];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldStock] != DBNull.Value)
			{
			_stock = (string )dr[FieldStock];
			}
			if (dr[FieldPrizeNumber] != DBNull.Value)
			{
			_prizeNumber = (string )dr[FieldPrizeNumber];
			}
			if (dr[FieldGiftsuse] != DBNull.Value)
			{
			_giftsuse = (string )dr[FieldGiftsuse];
			}
            if (dr[FieldSumCode] != DBNull.Value)
            {
                _sumcode = (string)dr[FieldSumCode];
            } 
            if (dr[FieldWinCode] != DBNull.Value)
            {
                _wincode = (string)dr[FieldWinCode];
            }
            if (dr[FieldCodeCount] != DBNull.Value)
            {
                _codeCount = (int)dr[FieldCodeCount];
            }

		}
		#endregion

	}
}
