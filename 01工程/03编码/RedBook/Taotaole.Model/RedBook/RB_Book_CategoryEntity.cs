using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_Book_CategoryEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldCategoryName = "CategoryName";
		public static string FieldDescription = "Description";
		public static string FieldAddTime = "AddTime";
		public static string FieldSortBaseNum = "SortBaseNum";
		public static string FieldCategoryIcon = "CategoryIcon";
		#endregion

		#region 属性
		private Guid  _id;
		public Guid  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _categoryName;
		public string  CategoryName
		{
			get{ return _categoryName;}
			set{ _categoryName=value;}
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
		private int  _sortBaseNum;
		public int  SortBaseNum
		{
			get{ return _sortBaseNum;}
			set{ _sortBaseNum=value;}
		}
		private string  _categoryIcon;
		public string  CategoryIcon
		{
			get{ return _categoryIcon;}
			set{ _categoryIcon=value;}
		}
		#endregion

		#region 构造函数
		public RB_Book_CategoryEntity(){}

		public RB_Book_CategoryEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldCategoryName] != DBNull.Value)
			{
			_categoryName = (string )dr[FieldCategoryName];
			}
			if (dr[FieldDescription] != DBNull.Value)
			{
			_description = (string )dr[FieldDescription];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
			if (dr[FieldSortBaseNum] != DBNull.Value)
			{
			_sortBaseNum = (int )dr[FieldSortBaseNum];
			}
			if (dr[FieldCategoryIcon] != DBNull.Value)
			{
			_categoryIcon = (string )dr[FieldCategoryIcon];
			}
		}
		#endregion

	}
}
