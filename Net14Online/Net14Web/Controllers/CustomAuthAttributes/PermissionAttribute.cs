﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net14Web.DbStuff.Models;
using Net14Web.DbStuff.Repositories;

namespace Net14Web.Controllers.CustomAuthAttributes
{
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        private PermissionType[] _permissionTypes;

        public PermissionAttribute(params PermissionType[] permissionTypes)
        {
            _permissionTypes = permissionTypes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authUserPermissions = context.HttpContext.RequestServices.GetService<PermissionRepository>();
            var userPermissions = authUserPermissions!.GetCurrentUserPermissions();
            foreach (var permission in userPermissions )
            {
                if (_permissionTypes.Any(p => p == permission.Type))
                {
                    return;
                }
            }

            context.Result = new ForbidResult(AuthController.AUTH_KEY);
        }
    }
}
