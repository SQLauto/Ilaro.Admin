﻿using System;
using System.Web.Mvc;

namespace Ilaro.Admin.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeWrapperAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AdminInitialise.Authorize != null)
            {
                AdminInitialise.Authorize.OnAuthorization(filterContext);
            }
        }
    }
}