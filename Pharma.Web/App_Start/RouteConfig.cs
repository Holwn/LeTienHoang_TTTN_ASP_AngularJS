using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pharma.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "lien-he.html",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "AllPost",
                url: "tat-ca-tin-tuc.html",
                defaults: new { controller = "Page", action = "PostList", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Post",
                url: "tin-tuc/{alias}.html",
                defaults: new { controller = "Page", action = "Post", alias = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page",
                url: "trang/{alias}.html",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ProductCategory",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "TagList",
                url: "tag/{tagId}.html",
                defaults: new { controller = "Product", action = "ListByTag", tagId = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pharma.Web.Controllers" }
            );
        }
    }
}
