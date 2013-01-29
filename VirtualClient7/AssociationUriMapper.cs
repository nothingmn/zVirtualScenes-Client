using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace VirtualClient7
{
    public class AssociationUriMapper : UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            string url = HttpUtility.UrlDecode(uri.ToString());

            if (url.Contains("zVirtualScenes:MainPage"))
            {
                //{/Protocol?encodedLaunchUri=zVirtualScenes:MainPage?action=scene&id=1}
                var pos = url.IndexOf(":MainPage?") +10;
                var param = url.Substring(pos);

                return new Uri(string.Format("/MainPage.xaml?" + param), UriKind.Relative);

            }

            return uri;
        }
    }
}