using System.Web;
using System.Web.Mvc;

namespace TravelMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
    public class MyFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取进入的方法名称
            string ActionName = filterContext.ActionDescriptor.ActionName;
            //获取进入的控制器名称
            string ControlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //如果进入的是UserInfo下的Login方法
            if (ActionName == "Login" && ControlName == "UserInfo")
            {
                //则什么都不做
            }
            else//如果是其他方法
            {
                //判断session["User"]是否有值
                if (filterContext.HttpContext.Session["User"] == null)
                {
                    //没值则跳转到登录页面进行登录
                    filterContext.Result = new RedirectResult("/UserInfo/Login");
                }
                else
                {
                    //有值则什么都不做
                }
            }
        }
    }
}
