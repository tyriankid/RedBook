using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_channel_userEntity {

		#region 字段名
		public static string FieldUid = "Uid";
		public static string FieldUsername = "Username";
		public static string FieldUserpass = "Userpass";
		public static string FieldRealname = "Realname";
		public static string FieldContacts = "Contacts";
		public static string FieldUsermobile = "Usermobile";
		public static string FieldUseremail = "Useremail";
		public static string FieldAddtime = "Addtime";
		public static string FieldParentid = "Parentid";
		public static string FieldTypeid = "Typeid";
		public static string FieldProvinceid = "Provinceid";
		public static string FieldCityid = "Cityid";
		public static string FieldRemark = "Remark";
		public static string FieldLogintime = "Logintime";
		public static string FieldLoginip = "Loginip";
		public static string FieldFrozenstate = "Frozenstate";
		public static string FieldRebateratio = "Rebateratio";
		public static string FieldNowithdrawcash = "Nowithdrawcash";
		public static string FieldWithdrawcash = "Withdrawcash";
		public static string FieldUsercount = "Usercount";
		public static string FieldUsercountmoney = "Usercountmoney";
		public static string FieldSettlementprice = "Settlementprice";
		public static string FieldGatheringaccount = "Gatheringaccount";
		#endregion

		#region 属性
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _username;
		public string  Username
		{
			get{ return _username;}
			set{ _username=value;}
		}
		private string  _userpass;
		public string  Userpass
		{
			get{ return _userpass;}
			set{ _userpass=value;}
		}
		private string  _realname;
		public string  Realname
		{
			get{ return _realname;}
			set{ _realname=value;}
		}
		private string  _contacts;
		public string  Contacts
		{
			get{ return _contacts;}
			set{ _contacts=value;}
		}
		private string  _usermobile;
		public string  Usermobile
		{
			get{ return _usermobile;}
			set{ _usermobile=value;}
		}
		private string  _useremail;
		public string  Useremail
		{
			get{ return _useremail;}
			set{ _useremail=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private int  _parentid;
		public int  Parentid
		{
			get{ return _parentid;}
			set{ _parentid=value;}
		}
		private int  _typeid;
		public int  Typeid
		{
			get{ return _typeid;}
			set{ _typeid=value;}
		}
		private string  _provinceid;
		public string  Provinceid
		{
			get{ return _provinceid;}
			set{ _provinceid=value;}
		}
		private string  _cityid;
		public string  Cityid
		{
			get{ return _cityid;}
			set{ _cityid=value;}
		}
		private string  _remark;
		public string  Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
		}
		private DateTime  _logintime;
		public DateTime  Logintime
		{
			get{ return _logintime;}
			set{ _logintime=value;}
		}
		private string  _loginip;
		public string  Loginip
		{
			get{ return _loginip;}
			set{ _loginip=value;}
		}
		private int  _frozenstate;
		public int  Frozenstate
		{
			get{ return _frozenstate;}
			set{ _frozenstate=value;}
		}
		private decimal  _rebateratio;
		public decimal  Rebateratio
		{
			get{ return _rebateratio;}
			set{ _rebateratio=value;}
		}
		private decimal  _nowithdrawcash;
		public decimal  Nowithdrawcash
		{
			get{ return _nowithdrawcash;}
			set{ _nowithdrawcash=value;}
		}
		private decimal  _withdrawcash;
		public decimal  Withdrawcash
		{
			get{ return _withdrawcash;}
			set{ _withdrawcash=value;}
		}
		private int  _usercount;
		public int  Usercount
		{
			get{ return _usercount;}
			set{ _usercount=value;}
		}
		private decimal  _usercountmoney;
		public decimal  Usercountmoney
		{
			get{ return _usercountmoney;}
			set{ _usercountmoney=value;}
		}
		private decimal  _settlementprice;
		public decimal  Settlementprice
		{
			get{ return _settlementprice;}
			set{ _settlementprice=value;}
		}
		private string  _gatheringaccount;
		public string  Gatheringaccount
		{
			get{ return _gatheringaccount;}
			set{ _gatheringaccount=value;}
		}
		#endregion

		#region 构造函数
		public go_channel_userEntity(){}

		public go_channel_userEntity(DataRow dr)
		{
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldUsername] != DBNull.Value)
			{
			_username = (string )dr[FieldUsername];
			}
			if (dr[FieldUserpass] != DBNull.Value)
			{
			_userpass = (string )dr[FieldUserpass];
			}
			if (dr[FieldRealname] != DBNull.Value)
			{
			_realname = (string )dr[FieldRealname];
			}
			if (dr[FieldContacts] != DBNull.Value)
			{
			_contacts = (string )dr[FieldContacts];
			}
			if (dr[FieldUsermobile] != DBNull.Value)
			{
			_usermobile = (string )dr[FieldUsermobile];
			}
			if (dr[FieldUseremail] != DBNull.Value)
			{
			_useremail = (string )dr[FieldUseremail];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldParentid] != DBNull.Value)
			{
			_parentid = (int )dr[FieldParentid];
			}
			if (dr[FieldTypeid] != DBNull.Value)
			{
                _typeid = (int)dr[FieldTypeid];
			}
			if (dr[FieldProvinceid] != DBNull.Value)
			{
			_provinceid = (string )dr[FieldProvinceid];
			}
			if (dr[FieldCityid] != DBNull.Value)
			{
			_cityid = (string )dr[FieldCityid];
			}
			if (dr[FieldRemark] != DBNull.Value)
			{
			_remark = (string )dr[FieldRemark];
			}
			if (dr[FieldLogintime] != DBNull.Value)
			{
			_logintime = (DateTime )dr[FieldLogintime];
			}
			if (dr[FieldLoginip] != DBNull.Value)
			{
			_loginip = (string )dr[FieldLoginip];
			}
			if (dr[FieldFrozenstate] != DBNull.Value)
			{
			_frozenstate = (int )dr[FieldFrozenstate];
			}
			if (dr[FieldRebateratio] != DBNull.Value)
			{
			_rebateratio = (decimal )dr[FieldRebateratio];
			}
			if (dr[FieldNowithdrawcash] != DBNull.Value)
			{
			_nowithdrawcash = (decimal )dr[FieldNowithdrawcash];
			}
			if (dr[FieldWithdrawcash] != DBNull.Value)
			{
			_withdrawcash = (decimal )dr[FieldWithdrawcash];
			}
			if (dr[FieldUsercount] != DBNull.Value)
			{
			_usercount = (int )dr[FieldUsercount];
			}
			if (dr[FieldUsercountmoney] != DBNull.Value)
			{
			_usercountmoney = (decimal )dr[FieldUsercountmoney];
			}
			if (dr[FieldSettlementprice] != DBNull.Value)
			{
			_settlementprice = (decimal )dr[FieldSettlementprice];
			}
			if (dr[FieldGatheringaccount] != DBNull.Value)
			{
			_gatheringaccount = (string )dr[FieldGatheringaccount];
			}
		}
		#endregion

	}
}
