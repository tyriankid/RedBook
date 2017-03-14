using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_BrokerageSetEntity {

		#region 字段名
		public static string FieldBrokerageID = "BrokerageID";
		public static string FieldScoreSet = "ScoreSet";
		public static string FieldGiftSet = "GiftSet";
		public static string FieldAgentSet = "AgentSet";
		public static string FieldObligateSet = "ObligateSet";
		#endregion

		#region 属性
		private int  _brokerageID;
		public int  BrokerageID
		{
			get{ return _brokerageID;}
			set{ _brokerageID=value;}
		}
		private int  _scoreSet;
		public int  ScoreSet
		{
			get{ return _scoreSet;}
			set{ _scoreSet=value;}
		}
		private int  _giftSet;
		public int  GiftSet
		{
			get{ return _giftSet;}
			set{ _giftSet=value;}
		}
		private int  _agentSet;
		public int  AgentSet
		{
			get{ return _agentSet;}
			set{ _agentSet=value;}
		}
		private int  _obligateSet;
		public int  ObligateSet
		{
			get{ return _obligateSet;}
			set{ _obligateSet=value;}
		}
		#endregion

		#region 构造函数
		public go_BrokerageSetEntity(){}

		public go_BrokerageSetEntity(DataRow dr)
		{
			if (dr[FieldBrokerageID] != DBNull.Value)
			{
			_brokerageID = (int )dr[FieldBrokerageID];
			}
			if (dr[FieldScoreSet] != DBNull.Value)
			{
			_scoreSet = (int )dr[FieldScoreSet];
			}
			if (dr[FieldGiftSet] != DBNull.Value)
			{
			_giftSet = (int )dr[FieldGiftSet];
			}
			if (dr[FieldAgentSet] != DBNull.Value)
			{
			_agentSet = (int )dr[FieldAgentSet];
			}
			if (dr[FieldObligateSet] != DBNull.Value)
			{
			_obligateSet = (int )dr[FieldObligateSet];
			}
		}
		#endregion

	}
}
