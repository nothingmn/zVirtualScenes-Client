using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Interfaces;


namespace zVirtualClient.VirtualScenes34
{
    public class VirtualScenes34UrlBuilder : IUrlBuilder
    {
        public VirtualScenes34UrlBuilder(Credentials Credentials)
        {
            this.Credentials = Credentials;
        }
        public Credentials Credentials { get; set; }

        public HttpPayload LoginPayload()
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/login", Credentials.Uri.ToString());
            pay.Data = System.Text.Encoding.UTF8.GetBytes(string.Format("u={1}&password={0}", Helpers.UrlHelper.Encode(Credentials.Password), rnd.NextDouble()));
            pay.POST = true;
            return pay;
        }
        public HttpPayload LogoutPayload()
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/logout", Credentials.Uri.ToString());
            pay.POST = true;
            return pay;
        }


        System.Random rnd = new Random();
        public HttpPayload DevicesPayload()
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/devices?u={1}&page=1&start=0&limit=999", Credentials.Uri.ToString(), rnd.NextDouble());
            pay.POST = false;
            return pay;
        }

        public HttpPayload DeviceDetailsPayload(int DeviceID)
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/device/{1}?u={2}", Credentials.Uri.ToString(), DeviceID, rnd.NextDouble());
            pay.POST = false;
            return pay;
        }
        public HttpPayload DeviceCommandsPayload(int DeviceID)
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/device/{1}/commands?u={2}", Credentials.Uri.ToString(), DeviceID, rnd.NextDouble());
            pay.POST = false;
            return pay;
        }

        public HttpPayload DeviceCommandPayload(int DeviceID, string Name, int arg, string type)
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/device/{1}/command/", Credentials.Uri.ToString(), DeviceID);
            pay.Data = System.Text.Encoding.UTF8.GetBytes(string.Format("name={3}&arg={4}&type={5}&u={2}", Credentials.Uri.ToString(), DeviceID, rnd.NextDouble(), Name, arg, type));
            pay.POST = true;
            return pay;
        }


        public HttpPayload DeviceValuesPayload(int DeviceID)
        {
            HttpPayload pay = new HttpPayload();
            pay.Url = string.Format("{0}API/device/{1}/values", Credentials.Uri.ToString(), DeviceID);
            //pay.Data = System.Text.Encoding.UTF8.GetBytes(string.Format("name={3}&arg={4}&type={5}&u={2}", Credentials.Uri.ToString(), DeviceID, rnd.NextDouble(), Name, arg, type));
            pay.POST = false;
            return pay;
        }
    }
}
