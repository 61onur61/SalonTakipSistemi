using System.Web;
using System.Web.Mvc;

namespace Gazi_Salon_Takip
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
