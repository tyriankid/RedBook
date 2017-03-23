using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_BookEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldMainTitle = "MainTitle";
		public static string FieldSubTitle = "SubTitle";
		public static string FieldBackImgUrl = "BackImgUrl";
		public static string FieldUserId = "UserId";
		public static string FieldUserName = "UserName";
		public static string FieldFabulousCount = "FabulousCount";
		public static string FieldFavoriteCount = "FavoriteCount";
		public static string FieldWatchCount = "WatchCount";
		public static string FieldKeyword = "Keyword";
		public static string FieldDescription = "Description";
		public static string FieldAddTime = "AddTime";
		public static string FieldCategoryId = "CategoryId";
		public static string FieldSortBaseNum = "SortBaseNum";
		public static string FieldStatus = "Status";
		public static string FieldProductIds = "ProductIds";
		public static string FieldMemo = "Memo";
		#endregion

		#region 属性
		private Guid  _id;
		public Guid  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _mainTitle;
		public string  MainTitle
		{
			get{ return _mainTitle;}
			set{ _mainTitle=value;}
		}
		private string  _subTitle;
		public string  SubTitle
		{
			get{ return _subTitle;}
			set{ _subTitle=value;}
		}
		private string  _backImgUrl;
		public string  BackImgUrl
		{
			get{ return _backImgUrl;}
			set{ _backImgUrl=value;}
		}
		private int  _userId;
		public int  UserId
		{
			get{ return _userId;}
			set{ _userId=value;}
		}
		private string  _userName;
		public string  UserName
		{
			get{ return _userName;}
			set{ _userName=value;}
		}
		private int  _fabulousCount;
		public int  FabulousCount
		{
			get{ return _fabulousCount;}
			set{ _fabulousCount=value;}
		}
		private int  _favoriteCount;
		public int  FavoriteCount
		{
			get{ return _favoriteCount;}
			set{ _favoriteCount=value;}
		}
		private int  _watchCount;
		public int  WatchCount
		{
			get{ return _watchCount;}
			set{ _watchCount=value;}
		}
		private string  _keyword;
		public string  Keyword
		{
			get{ return _keyword;}
			set{ _keyword=value;}
		}
		private string  _description;
		public string  Description
		{
			get{ return _description;}
			set{ _description=value;}
		}
		private DateTime  _addTime;
		public DateTime  AddTime
		{
			get{ return _addTime;}
			set{ _addTime=value;}
		}
		private Guid  _categoryId;
		public Guid  CategoryId
		{
			get{ return _categoryId;}
			set{ _categoryId=value;}
		}
		private int  _sortBaseNum;
		public int  SortBaseNum
		{
			get{ return _sortBaseNum;}
			set{ _sortBaseNum=value;}
		}
		private int  _status;
		public int  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private string  _productIds;
		public string  ProductIds
		{
			get{ return _productIds;}
			set{ _productIds=value;}
		}
		private string  _memo;
		public string  Memo
		{
			get{ return _memo;}
			set{ _memo=value;}
		}
		#endregion

		#region 构造函数
		public RB_BookEntity(){}

		public RB_BookEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldMainTitle] != DBNull.Value)
			{
			_mainTitle = (string )dr[FieldMainTitle];
			}
			if (dr[FieldSubTitle] != DBNull.Value)
			{
			_subTitle = (string )dr[FieldSubTitle];
			}
			if (dr[FieldBackImgUrl] != DBNull.Value)
			{
			_backImgUrl = (string )dr[FieldBackImgUrl];
			}
			if (dr[FieldUserId] != DBNull.Value)
			{
			_userId = (int )dr[FieldUserId];
			}
			if (dr[FieldUserName] != DBNull.Value)
			{
			_userName = (string )dr[FieldUserName];
			}
			if (dr[FieldFabulousCount] != DBNull.Value)
			{
			_fabulousCount = (int )dr[FieldFabulousCount];
			}
			if (dr[FieldFavoriteCount] != DBNull.Value)
			{
			_favoriteCount = (int )dr[FieldFavoriteCount];
			}
			if (dr[FieldWatchCount] != DBNull.Value)
			{
			_watchCount = (int )dr[FieldWatchCount];
			}
			if (dr[FieldKeyword] != DBNull.Value)
			{
			_keyword = (string )dr[FieldKeyword];
			}
			if (dr[FieldDescription] != DBNull.Value)
			{
			_description = (string )dr[FieldDescription];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
			if (dr[FieldCategoryId] != DBNull.Value)
			{
			_categoryId = (Guid )dr[FieldCategoryId];
			}
			if (dr[FieldSortBaseNum] != DBNull.Value)
			{
			_sortBaseNum = (int )dr[FieldSortBaseNum];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
			if (dr[FieldProductIds] != DBNull.Value)
			{
			_productIds = (string )dr[FieldProductIds];
			}
			if (dr[FieldMemo] != DBNull.Value)
			{
			_memo = (string )dr[FieldMemo];
			}
		}
		#endregion

	}
}
