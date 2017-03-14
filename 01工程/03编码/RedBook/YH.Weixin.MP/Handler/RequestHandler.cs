using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using YH.Weixin.MP.Entity;
using YH.Weixin.MP.Enums;
using YH.Weixin.MP.Messages;
using YH.Weixin.MP.Request;
using YH.Weixin.MP.Request.Event;
using YH.Weixin.MP.Util;

namespace YH.Weixin.MP.Handler
{
    public abstract class RequestHandler
    {
        public RequestHandler(Stream inputStream)
        {
            using (XmlReader reader = XmlReader.Create(inputStream))
            {
                this.RequestDocument = XDocument.Load(reader);
                this.Init(this.RequestDocument);
            }
        }

        public RequestHandler(string xml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                this.RequestDocument = XDocument.Load(reader);
                this.Init(this.RequestDocument);
            }
        }

        public abstract AbstractResponse DefaultResponse(AbstractRequest requestMessage);
        public void Execute()
        {
            if (this.RequestMessage != null)
            {
                WriteLog("start:" + this.RequestMessage.MsgType);
                switch (this.RequestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        {
                            this.ResponseMessage = this.OnTextRequest(this.RequestMessage as TextRequest);
                            break;
                        }
                    case RequestMsgType.Image:
                        {
                            this.ResponseMessage = this.OnImageRequest(this.RequestMessage as ImageRequest);
                            break;
                        }

                    case RequestMsgType.Voice:
                        {
                            this.ResponseMessage = this.OnVoiceRequest(this.RequestMessage as VoiceRequest);
                            break;
                        }

                    case RequestMsgType.Video:
                        {
                            this.ResponseMessage = this.OnVideoRequest(this.RequestMessage as VideoRequest);
                            break;
                        }

                    case RequestMsgType.Location:
                        {
                            this.ResponseMessage = this.OnLocationRequest(this.RequestMessage as LocationRequest);
                            break;
                        }

                    case RequestMsgType.Link:
                        {
                            this.ResponseMessage = this.OnLinkRequest(this.RequestMessage as LinkRequest);
                            break;
                        }

                    case RequestMsgType.Event:
                        {
                            this.ResponseMessage = this.OnEventRequest(this.RequestMessage as EventRequest);
                            break;
                        }
                    default:
                        {
                            throw new WeixinException("未知的MsgType请求类型");
                            //  break;
                        }
                }

            }
        }

        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_login.txt"));
            writer.WriteLine(System.DateTime.Now);
            writer.WriteLine(log);
            writer.Flush();
            writer.Close();
        }

        private void Init(XDocument requestDocument)
        {
            this.RequestDocument = requestDocument;
            this.RequestMessage = RequestMessageFactory.GetRequestEntity(this.RequestDocument);
        }

        public virtual AbstractResponse OnEvent_ClickRequest(ClickEventRequest clickEventRequest)
        {
            return this.DefaultResponse(clickEventRequest);
        }

        public virtual AbstractResponse OnEvent_ViewRequest(ViewEventRequest viewEventRequest)
        {
            return this.DefaultResponse(viewEventRequest);
        }

        public virtual AbstractResponse OnEvent_LocationRequest(LocationEventRequest locationEventRequest)
        {
            return this.DefaultResponse(locationEventRequest);
        }

        public virtual AbstractResponse OnEvent_ScanRequest(ScanEventRequest scanEventRequest)
        {
            return this.DefaultResponse(scanEventRequest);
        }

        public virtual AbstractResponse OnEvent_SubscribeRequest(SubscribeEventRequest subscribeEventRequest)
        {
            return this.DefaultResponse(subscribeEventRequest);
        }

        public virtual AbstractResponse OnEvent_UnSubscribeRequest(UnSubscribeEventRequest unSubscribeEventRequest)
        {
            return this.DefaultResponse(unSubscribeEventRequest);
        }

        public AbstractResponse OnEventRequest(EventRequest eventRequest)
        {

            AbstractResponse response = null;
            WriteLog("start2:" + eventRequest.Event);
            switch (eventRequest.Event)
            {
                case RequestEventType.Subscribe:
                    {
                        response = this.OnEvent_SubscribeRequest(eventRequest as SubscribeEventRequest);
                        break;
                    }
                case RequestEventType.UnSubscribe:
                    {
                        response = this.OnEvent_UnSubscribeRequest(eventRequest as UnSubscribeEventRequest);
                        break;
                    }
                case RequestEventType.Scan:
                    {
                        this.ResponseMessage = this.OnEvent_ScanRequest(eventRequest as ScanEventRequest);
                        break;
                    }
                case RequestEventType.Location:
                    {
                        response = this.OnEvent_LocationRequest(eventRequest as LocationEventRequest);
                        break;
                    }
                case RequestEventType.Click:
                    {
                        response = this.OnEvent_ClickRequest(eventRequest as ClickEventRequest);
                        break;
                    }
                case RequestEventType.VIEW:
                    {
                        response = this.OnEvent_ViewRequest(eventRequest as ViewEventRequest);
                        break;
                    }
                default:
                    {
                        throw new WeixinException("未知的Event下属请求信息");
                        // break;
                    }
            }

            return response;

        }

        public virtual AbstractResponse OnImageRequest(ImageRequest imageRequest)
        {
            return this.DefaultResponse(imageRequest);
        }

        public AbstractResponse OnLinkRequest(LinkRequest linkRequest)
        {
            return this.DefaultResponse(linkRequest);
        }

        public virtual AbstractResponse OnLocationRequest(LocationRequest locationRequest)
        {
            return this.DefaultResponse(locationRequest);
        }

        public virtual AbstractResponse OnTextRequest(TextRequest textRequest)
        {
            return this.DefaultResponse(textRequest);
        }

        public virtual AbstractResponse OnVideoRequest(VideoRequest videoRequest)
        {
            return this.DefaultResponse(videoRequest);
        }

        public virtual AbstractResponse OnVoiceRequest(VoiceRequest voiceRequest)
        {
            return this.DefaultResponse(voiceRequest);
        }

        public XDocument RequestDocument { get; set; }

        public AbstractRequest RequestMessage { get; set; }

        public string ResponseDocument
        {
            get
            {
                if (this.ResponseMessage == null)
                {
                    return string.Empty;
                }
                return EntityHelper.ConvertEntityToXml<AbstractResponse>(this.ResponseMessage).ToString();
            }
        }

        public AbstractResponse ResponseMessage { get; set; }
    }
}
