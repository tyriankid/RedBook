using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 微信自定义回复-实体类
	/// </summary>
	[Serializable]
	public  class go_wxreplyEntity {

		#region 字段名
		public static string FieldReplyid = "Replyid";
		public static string FieldKeyword = "Keyword";
		public static string FieldMatchtype = "Matchtype";
		public static string FieldReplytype = "Replytype";
		public static string FieldMessagetype = "Messagetype";
		public static string FieldIsdisable = "Isdisable";
		public static string FieldContent = "Content";
		#endregion

		#region 属性
		private int  _replyid;
		public int  Replyid
		{
			get{ return _replyid;}
			set{ _replyid=value;}
		}
		private string  _keyword;
		public string  Keyword
		{
			get{ return _keyword;}
			set{ _keyword=value;}
		}
		private int  _matchtype;
		public int  Matchtype
		{
			get{ return _matchtype;}
			set{ _matchtype=value;}
		}
		private int  _replytype;
		public int  Replytype
		{
			get{ return _replytype;}
			set{ _replytype=value;}
		}
		private int  _messagetype;
		public int  Messagetype
		{
			get{ return _messagetype;}
			set{ _messagetype=value;}
		}
		private int  _isdisable;
		public int  Isdisable
		{
			get{ return _isdisable;}
			set{ _isdisable=value;}
		}
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		#endregion

		#region 构造函数
		public go_wxreplyEntity(){}

		public go_wxreplyEntity(DataRow dr)
		{
			if (dr[FieldReplyid] != DBNull.Value)
			{
			_replyid = (int )dr[FieldReplyid];
			}
			if (dr[FieldKeyword] != DBNull.Value)
			{
			_keyword = (string )dr[FieldKeyword];
			}
			if (dr[FieldMatchtype] != DBNull.Value)
			{
			_matchtype = (int )dr[FieldMatchtype];
			}
			if (dr[FieldReplytype] != DBNull.Value)
			{
			_replytype = (int )dr[FieldReplytype];
			}
			if (dr[FieldMessagetype] != DBNull.Value)
			{
			_messagetype = (int )dr[FieldMessagetype];
			}
			if (dr[FieldIsdisable] != DBNull.Value)
			{
			_isdisable = (int )dr[FieldIsdisable];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
		}
		#endregion

	}
}
