using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YH.Weixin.MP.Entity;
using YH.Weixin.MP.Enums;
using YH.Weixin.MP.Request;
using YH.Weixin.MP.Request.Event;

namespace YH.Weixin.MP.Util
{
    public static class RequestMessageFactory
    {
        public static AbstractRequest GetRequestEntity(XDocument doc)
        {
            RequestMsgType msgType = MsgTypeHelper.GetMsgType(doc);

            AbstractRequest entity = null;
            // System.IO.File.AppendAllText(@"E:\home\LocalUser\580442\www\humowang\api\type.txt", "msgType:" + msgType + "\r\n");
            switch (msgType)
            {
                case RequestMsgType.Text:
                    {
                        entity = new TextRequest();
                        break;
                    }
                case RequestMsgType.Image:
                    {
                        entity = new ImageRequest();
                        break;
                    }
                case RequestMsgType.Voice:
                    {
                        entity = new VoiceRequest();
                        break;
                    }
                case RequestMsgType.Video:
                    {
                        entity = new VideoRequest();
                        break;
                    }
                case RequestMsgType.Location:
                    {
                        entity = new LocationRequest();
                        break;
                    }
                case RequestMsgType.Link:
                    {
                        entity = new LinkRequest();
                        break;
                    }
                case RequestMsgType.Event:
                    {

                        switch (EventTypeHelper.GetEventType(doc))
                        {
                            case RequestEventType.Subscribe:
                                {
                                    entity = new SubscribeEventRequest();
                                    //goto Label_00C5;
                                    break;
                                }
                            case RequestEventType.UnSubscribe:
                                {
                                    entity = new UnSubscribeEventRequest();
                                    //goto Label_00C5;
                                    break;
                                }
                            case RequestEventType.Scan:
                                {
                                    entity = new ScanEventRequest();
                                    // goto Label_00C5;
                                    break;
                                }
                            case RequestEventType.Location:
                                {
                                    entity = new LocationEventRequest();
                                    // goto Label_00C5;
                                    break;
                                }
                            case RequestEventType.Click:
                                {
                                    entity = new ClickEventRequest();
                                    // goto Label_00C5;
                                    break;
                                }
                            case RequestEventType.VIEW:
                                {
                                    entity = new ViewEventRequest();
                                    break;
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException();
                                    // break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }

            }

            //Label_00C5:
            EntityHelper.FillEntityWithXml<AbstractRequest>(entity, doc);

            return entity;
        }
    }
}
