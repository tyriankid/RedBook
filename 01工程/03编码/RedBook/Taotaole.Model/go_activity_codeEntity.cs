using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
    public class go_activity_codeEntity
    {

        #region 字段名
        public static string FieldId = "Id";
        public static string FieldActivity_id = "Activity_id";
        public static string FieldCode_config_id = "Code_config_id";
        public static string FieldCodetitle = "Codetitle";
        public static string FieldChannel_id = "Channel_id";
        public static string FieldUid = "Uid";
        public static string FieldMobile = "Mobile";
        public static string FieldOrder_id = "Order_id";
        public static string FieldStarttime = "Starttime";
        public static string FieldEndtime = "Endtime";
        public static string FieldRaisetime = "Raisetime";
        public static string FieldTimespans = "Timespans";
        public static string FieldUse_range = "Use_range";
        public static string FieldUsetime = "Usetime";
        public static string FieldDescript = "Descript";
        public static string FieldD_type = "D_type";
        public static string FieldAmount = "Amount";
        public static string FieldDiscount = "Discount";
        public static string FieldState = "State";
        public static string FieldFrom = "From";
        public static string FieldAddtime = "Addtime";
        public static string FieldOvertime = "Overtime";
        public static string FieldSenttime = "Senttime";
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
        private int _code_config_id;
        public int Code_config_id
        {
            get { return _code_config_id; }
            set { _code_config_id = value; }
        }
        private string _codetitle;
        public string Codetitle
        {
            get { return _codetitle; }
            set { _codetitle = value; }
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
        private string _order_id;
        public string Order_id
        {
            get { return _order_id; }
            set { _order_id = value; }
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
        private DateTime _usetime;
        public DateTime Usetime
        {
            get { return _usetime; }
            set { _usetime = value; }
        }
        private string _descript;
        public string Descript
        {
            get { return _descript; }
            set { _descript = value; }
        }
        private int _d_type;
        public int D_type
        {
            get { return _d_type; }
            set { _d_type = value; }
        }
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private int _discount;
        public int Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        private int _from;
        public int From
        {
            get { return _from; }
            set { _from = value; }
        }
        private DateTime _addtime;
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private DateTime _overtime;
        public DateTime Overtime
        {
            get { return _overtime; }
            set { _overtime = value; }
        }
        private DateTime _senttime;
        public DateTime Senttime
        {
            get { return _senttime; }
            set { _senttime = value; }
        }
        #endregion

        #region 构造函数
        public go_activity_codeEntity() { }

        public go_activity_codeEntity(DataRow dr)
        {
            if (dr[FieldId] != DBNull.Value)
            {
                _id = (int)dr[FieldId];
            }
            if (dr[FieldActivity_id] != DBNull.Value)
            {
                _activity_id = (int)dr[FieldActivity_id];
            }
            if (dr[FieldCode_config_id] != DBNull.Value)
            {
                _code_config_id = (int)dr[FieldCode_config_id];
            }
            if (dr[FieldCodetitle] != DBNull.Value)
            {
                _codetitle = (string)dr[FieldCodetitle];
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
            if (dr[FieldOrder_id] != DBNull.Value)
            {
                _order_id = (string)dr[FieldOrder_id];
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
            if (dr[FieldUsetime] != DBNull.Value)
            {
                _usetime = (DateTime)dr[FieldUsetime];
            }
            if (dr[FieldDescript] != DBNull.Value)
            {
                _descript = (string)dr[FieldDescript];
            }
            if (dr[FieldD_type] != DBNull.Value)
            {
                _d_type = (int)dr[FieldD_type];
            }
            if (dr[FieldAmount] != DBNull.Value)
            {
                _amount = (int)dr[FieldAmount];
            }
            if (dr[FieldDiscount] != DBNull.Value)
            {
                _discount = (int)dr[FieldDiscount];
            }
            if (dr[FieldState] != DBNull.Value)
            {
                _state = (int)dr[FieldState];
            }
            if (dr[FieldFrom] != DBNull.Value)
            {
                _from = (int)dr[FieldFrom];
            }
            if (dr[FieldAddtime] != DBNull.Value)
            {
                _addtime = (DateTime)dr[FieldAddtime];
            }
            if (dr[FieldOvertime] != DBNull.Value)
            {
                _overtime = (DateTime)dr[FieldOvertime];
            }
            if (dr[FieldSenttime] != DBNull.Value)
            {
                _senttime = (DateTime)dr[FieldSenttime];
            }
        }
        #endregion

    }
}
