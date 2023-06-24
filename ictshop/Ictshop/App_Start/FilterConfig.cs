using System.Web;
using System.Web.Mvc;

namespace Ictshop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());     //sử dụng để xử lý lỗi và hiển thị trang lỗi tương ứng cho người dùng.
        }
    }
}
