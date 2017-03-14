using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_giftNumsEntity {

		#region 字段名
		public static string FieldGiftNumsId = "GiftNumsId";
		public static string FieldNums = "Nums";
		public static string FieldImageurl = "Imageurl";
		public static string FieldImageID = "ImageID";
		#endregion

		#region 属性
		private int  _giftNumsId;
		public int  GiftNumsId
		{
			get{ return _giftNumsId;}
			set{ _giftNumsId=value;}
		}
		private string  _nums;
		public string  Nums
		{
			get{ return _nums;}
			set{ _nums=value;}
		}
		private string  _imageurl;
		public string  Imageurl
		{
			get{ return _imageurl;}
			set{ _imageurl=value;}
		}
		private string  _imageID;
		public string  ImageID
		{
			get{ return _imageID;}
			set{ _imageID=value;}
		}
		#endregion

		#region 构造函数
		public go_giftNumsEntity(){}

		public go_giftNumsEntity(DataRow dr)
		{
			if (dr[FieldGiftNumsId] != DBNull.Value)
			{
			_giftNumsId = (int )dr[FieldGiftNumsId];
			}
			if (dr[FieldNums] != DBNull.Value)
			{
			_nums = (string )dr[FieldNums];
			}
			if (dr[FieldImageurl] != DBNull.Value)
			{
			_imageurl = (string )dr[FieldImageurl];
			}
			if (dr[FieldImageID] != DBNull.Value)
			{
			_imageID = (string )dr[FieldImageID];
			}
		}
		#endregion

	}
}
