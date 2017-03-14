using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_articleEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldCateid = "Cateid";
		public static string FieldAuthor = "Author";
		public static string FieldTitle = "Title";
		public static string FieldTitle_style = "Title_style";
		public static string FieldThumb = "Thumb";
		public static string FieldPicarr = "Picarr";
		public static string FieldKeywords = "Keywords";
		public static string FieldDescription = "Description";
		public static string FieldContents = "Contents";
		public static string FieldHit = "Hit";
		public static string FieldOrdersn = "Ordersn";
		public static string FieldPosttime = "Posttime";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _cateid;
		public string  Cateid
		{
			get{ return _cateid;}
			set{ _cateid=value;}
		}
		private string  _author;
		public string  Author
		{
			get{ return _author;}
			set{ _author=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private string  _title_style;
		public string  Title_style
		{
			get{ return _title_style;}
			set{ _title_style=value;}
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
		private string  _contents;
		public string  Contents
		{
			get{ return _contents;}
			set{ _contents=value;}
		}
		private int  _hit;
		public int  Hit
		{
			get{ return _hit;}
			set{ _hit=value;}
		}
		private int  _ordersn;
		public int  Ordersn
		{
			get{ return _ordersn;}
			set{ _ordersn=value;}
		}
		private DateTime  _posttime;
		public DateTime  Posttime
		{
			get{ return _posttime;}
			set{ _posttime=value;}
		}
		#endregion

		#region 构造函数
		public go_articleEntity(){}

		public go_articleEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldCateid] != DBNull.Value)
			{
			_cateid = (string )dr[FieldCateid];
			}
			if (dr[FieldAuthor] != DBNull.Value)
			{
			_author = (string )dr[FieldAuthor];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldTitle_style] != DBNull.Value)
			{
			_title_style = (string )dr[FieldTitle_style];
			}
			if (dr[FieldThumb] != DBNull.Value)
			{
			_thumb = (string )dr[FieldThumb];
			}
			if (dr[FieldPicarr] != DBNull.Value)
			{
			_picarr = (string )dr[FieldPicarr];
			}
			if (dr[FieldKeywords] != DBNull.Value)
			{
			_keywords = (string )dr[FieldKeywords];
			}
			if (dr[FieldDescription] != DBNull.Value)
			{
			_description = (string )dr[FieldDescription];
			}
			if (dr[FieldContents] != DBNull.Value)
			{
			_contents = (string )dr[FieldContents];
			}
			if (dr[FieldHit] != DBNull.Value)
			{
			_hit = (int )dr[FieldHit];
			}
			if (dr[FieldOrdersn] != DBNull.Value)
			{
			_ordersn = (int )dr[FieldOrdersn];
			}
			if (dr[FieldPosttime] != DBNull.Value)
			{
			_posttime = (DateTime )dr[FieldPosttime];
			}
		}
		#endregion

	}
}
