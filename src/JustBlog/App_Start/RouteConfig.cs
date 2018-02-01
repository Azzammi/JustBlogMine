using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* Part II */
            /* Admin Route */
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Admin", action = "Logout"}
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage",
                defaults: new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute(
                name: "AdminAction",
                url: "Admin/{action}",
                defaults: new { controller = "Admin", action = "Login" }
            );
            /* Part 1 */
            /* Content Post Route */
            routes.MapRoute(
                name: "Action",
                url: "{action}",
                defaults: new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category",
                url: "Category/{category}",
                defaults: new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "Tag/{tag}",
                defaults: new { controller = "Blog", action = "Tag"}
            );

            routes.MapRoute(
                name: "Post",
                url: "Archive/{year}/{month}/{title}",
                defaults: new { controller = "Blog", action = "Post" }
            );

           
        }
    }
}
