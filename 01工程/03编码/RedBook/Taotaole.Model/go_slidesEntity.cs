using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_slidesEntity {

		#region 字段名
		public static string FieldSlideid = "Slideid";
		public static string FieldSlidelink = "Slidelink";
		public static string FieldSlideimg = "Slideimg";
		public static string FieldSlidetype = "Slidetype";
		public static string FieldSlideorder = "Slideorder";
		public static string FieldSlidelastupdatetime = "Slidelastupdatetime";
		public static string FieldSlidestate = "Slidestate";
		public static string FieldActivityid = "Activityid";
		#endregion

		#region 属性
		private int  _slideid;
		public int  Slideid
		{
			get{ return _slideid;}
			set{ _slideid=value;}
		}
		private string  _slidelink;
		public string  Slidelink
		{
			get{ return _slidelink;}
			set{ _slidelink=value;}
		}
		private string  _slideimg;
		public string  Slideimg
		{
			get{ return _slideimg;}
			set{ _slideimg=value;}
		}
		private int  _slidetype;
		public int  Slidetype
		{
			get{ return _slidetype;}
			set{ _slidetype=value;}
		}
		private int  _slideorder;
		public int  Slideorder
		{
			get{ return _slideorder;}
			set{ _slideorder=value;}
		}
		private DateTime  _slidelastupdatetime;
		public DateTime  Slidelastupdatetime
		{
			get{ return _slidelastupdatetime;}
			set{ _slidelastupdatetime=value;}
		}
		private int  _slidestate;
		public int  Slidestate
		{
			get{ return _slidestate;}
			set{ _slidestate=value;}
		}
		private int  _activityid;
		public int  Activityid
		{
			get{ return _activityid;}
			set{ _activityid=value;}
		}
		#endregion

		#region 构造函数
		public go_slidesEntity(){}

		public go_slidesEntity(DataRow dr)
		{
			if (dr[FieldSlideid] != DBNull.Value)
			{
			_slideid = (int )dr[FieldSlideid];
			}
			if (dr[FieldSlidelink] != DBNull.Value)
			{
			_slidelink = (string )dr[FieldSlidelink];
			}
			if (dr[FieldSlideimg] != DBNull.Value)
			{
			_slideimg = (string )dr[FieldSlideimg];
			}
			if (dr[FieldSlidetype] != DBNull.Value)
			{
			_slidetype = (int )dr[FieldSlidetype];
			}
			if (dr[FieldSlideorder] != DBNull.Value)
			{
			_slideorder = (int )dr[FieldSlideorder];
			}
			if (dr[FieldSlidelastupdatetime] != DBNull.Value)
			{
			_slidelastupdatetime = (DateTime )dr[FieldSlidelastupdatetime];
			}
			if (dr[FieldSlidestate] != DBNull.Value)
			{
			_slidestate = (int )dr[FieldSlidestate];
			}
			if (dr[FieldActivityid] != DBNull.Value)
			{
			_activityid = (int )dr[FieldActivityid];
			}
		}
		#endregion

	}
}
