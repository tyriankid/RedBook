using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 图文回复-实体类
	/// </summary>
	[Serializable]
	public  class go_wxreply_articleEntity {

		#region 字段名
		public static string FieldReplyid = "Replyid";
		public static string FieldArticleid = "Articleid";
		public static string FieldTitle = "Title";
		public static string FieldImgurl = "Imgurl";
		public static string FieldUrl = "Url";
		public static string FieldDescription = "Description";
		public static string FieldContent = "Content";
		#endregion

		#region 属性
		private int  _replyid;
		public int  Replyid
		{
			get{ return _replyid;}
			set{ _replyid=value;}
		}
		private int  _articleid;
		public int  Articleid
		{
			get{ return _articleid;}
			set{ _articleid=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private string  _imgurl;
		public string  Imgurl
		{
			get{ return _imgurl;}
			set{ _imgurl=value;}
		}
		private string  _url;
		public string  Url
		{
			get{ return _url;}
			set{ _url=value;}
		}
		private string  _description;
		public string  Description
		{
			get{ return _description;}
			set{ _description=value;}
		}
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		#endregion

		#region 构造函数
		public go_wxreply_articleEntity(){}

		public go_wxreply_articleEntity(DataRow dr)
		{
			if (dr[FieldReplyid] != DBNull.Value)
			{
			_replyid = (int )dr[FieldReplyid];
			}
			if (dr[FieldArticleid] != DBNull.Value)
			{
			_articleid = (int )dr[FieldArticleid];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldImgurl] != DBNull.Value)
			{
			_imgurl = (string )dr[FieldImgurl];
			}
			if (dr[FieldUrl] != DBNull.Value)
			{
			_url = (string )dr[FieldUrl];
			}
			if (dr[FieldDescription] != DBNull.Value)
			{
			_description = (string )dr[FieldDescription];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
		}
		#endregion

	}
}
