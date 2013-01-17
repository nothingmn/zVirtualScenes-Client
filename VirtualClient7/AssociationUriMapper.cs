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

            if (url.Contains("VirtualClient7:MainPage"))
            {
                int paramIndex = url.IndexOf("source=") + 7;
                string paramValue = url.Substring(paramIndex);

                return new Uri("/MainPage.xaml?source=" + paramValue, UriKind.Relative);
            }

            return uri;
        }
    }
}
