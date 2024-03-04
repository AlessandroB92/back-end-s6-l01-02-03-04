using System.Web;
using System.Web.Mvc;

namespace back_end_s6_l01_02_03_04
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
