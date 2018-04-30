using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/**
 * CustomAuthorizationAttribute
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Custom filter attribute for preventing non logged in users from accessing route
 * 
 */
namespace CLC.Controllers
{
    public class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        
		void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
		{

			if(System.Web.HttpContext.Current.Session["user"] == null)
				filterContext.Result = new RedirectResult("/Login/Index");
		}

    }
}