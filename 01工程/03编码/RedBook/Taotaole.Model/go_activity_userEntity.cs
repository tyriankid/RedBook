using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
    public class go_activity_userEntity
    {

        #region 字段名
        public static string FieldId = "Id";
        public static string FieldActivity_id = "Activity_id";
        public static string FieldA_type = "A_type";
        public static string FieldChannel_id = "Channel_id";
        public static string FieldUid = "Uid";
        public static string FieldMobile = "Mobile";
        public static string FieldCount = "Count";
        public static string FieldAlreadycount = "Alreadycount";
        public static string FieldSentcount = "Sentcount";
        public static string FieldAlreadysentcount = "Alreadysentcount";
        public static string FieldStarttime = "Starttime";
        public static string FieldEndtime = "Endtime";
        public static string FieldRaisetime = "Raisetime";
        public static string FieldTimespans = "Timespans";
        public static string FieldUse_range = "Use_range";
        public static string FieldAmount = "Amount";
        public static string FieldAddtime = "Addtime";
        public static string FieldRedpackday = "Redpackday";
        #endregion

        #region 属性
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _activity_id;
        public int Activity_id
        {
            get { return _activity_id; }
            set { _activity_id = value; }
        }
        private int _a_type;
        public int A_type
        {
            get { return _a_type; }
            set { _a_type = value; }
        }
        private int _channel_id;
        public int Channel_id
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        private int _alreadycount;
        public int Alreadycount
        {
            get { return _alreadycount; }
            set { _alreadycount = value; }
        }
        private int _sentcount;
        public int Sentcount
        {
            get { return _sentcount; }
            set { _sentcount = value; }
        }
        private int _alreadysentcount;
        public int Alreadysentcount
        {
            get { return _alreadysentcount; }
            set { _alreadysentcount = value; }
        }
        private DateTime _starttime;
        public DateTime Starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }
        private DateTime _endtime;
        public DateTime Endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }
        private int _raisetime;
        public int Raisetime
        {
            get { return _raisetime; }
            set { _raisetime = value; }
        }
        private string _timespans;
        public string Timespans
        {
            get { return _timespans; }
            set { _timespans = value; }
        }
        private string _use_range;
        public string Use_range
        {
            get { return _use_range; }
            set { _use_range = value; }
        }
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private DateTime _addtime;
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _redpackday;
        public int Redpackday
        {
            get { return _redpackday; }
            set { _redpackday = value; }
        }
        #endregion

        #region 构造函数
        public go_activity_userEntity() { }

        public go_activity_userEntity(DataRow dr)
        {
            if (dr[FieldId] != DBNull.Value)
            {
                _id = (int)dr[FieldId];
            }
            if (dr[FieldActivity_id] != DBNull.Value)
            {
                _activity_id = (int)dr[FieldActivity_id];
            }
            if (dr[FieldA_type] != DBNull.Value)
            {
                _a_type = (int)dr[FieldA_type];
            }
            if (dr[FieldChannel_id] != DBNull.Value)
            {
                _channel_id = (int)dr[FieldChannel_id];
            }
            if (dr[FieldUid] != DBNull.Value)
            {
                _uid = (string)dr[FieldUid];
            }
            if (dr[FieldMobile] != DBNull.Value)
            {
                _mobile = (string)dr[FieldMobile];
            }
            if (dr[FieldCount] != DBNull.Value)
            {
                _count = (int)dr[FieldCount];
            }
            if (dr[FieldAlreadycount] != DBNull.Value)
            {
                _alreadycount = (int)dr[FieldAlreadycount];
            }
            if (dr[FieldSentcount] != DBNull.Value)
            {
                _sentcount = (int)dr[FieldSentcount];
            }
            if (dr[FieldAlreadysentcount] != DBNull.Value)
            {
                _alreadysentcount = (int)dr[FieldAlreadysentcount];
            }
            if (dr[FieldStarttime] != DBNull.Value)
            {
                _starttime = (DateTime)dr[FieldStarttime];
            }
            if (dr[FieldEndtime] != DBNull.Value)
            {
                _endtime = (DateTime)dr[FieldEndtime];
            }
            if (dr[FieldRaisetime] != DBNull.Value)
            {
                _raisetime = (int)dr[FieldRaisetime];
            }
            if (dr[FieldTimespans] != DBNull.Value)
            {
                _timespans = (string)dr[FieldTimespans];
            }
            if (dr[FieldUse_range] != DBNull.Value)
            {
                _use_range = (string)dr[FieldUse_range];
            }
            if (dr[FieldAmount] != DBNull.Value)
            {
                _amount = (int)dr[FieldAmount];
            }
            if (dr[FieldAddtime] != DBNull.Value)
            {
                _addtime = (DateTime)dr[FieldAddtime];
            }
            if (dr[FieldRedpackday] != DBNull.Value)
            {
                _redpackday = (int)dr[FieldRedpackday];
            }
        }
        #endregion

    }
}
