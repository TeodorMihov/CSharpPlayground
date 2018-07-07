namespace GoodPractices.Web
{
    using System.Text;

    public static class HtmlHelperExtensions
    {
        //// Use in view @Html.Raw(Html.BuildBreadcrumbNavigation())

        //public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        //{
        //    // optional condition: I didn't wanted it to show on home and account controller
        //    if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
        //        helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
        //    {
        //        return string.Empty;
        //    }

        //    var breadcrumb = new StringBuilder("<ol class='breadcrumb'><li>").Append(helper.ActionLink("..", "Index", "Home").ToHtmlString()).Append("</li>");

        //    breadcrumb.Append("<li>");
        //    breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
        //                                       "Index",
        //                                       helper.ViewContext.RouteData.Values["controller"].ToString()));
        //    breadcrumb.Append("</li>");

        //    if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
        //    {
        //        breadcrumb.Append("<li>");
        //        breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString().Titleize(),
        //                                            helper.ViewContext.RouteData.Values["action"].ToString(),
        //                                            helper.ViewContext.RouteData.Values["controller"].ToString()));
        //        breadcrumb.Append("</li>");
        //    }

        //    return breadcrumb.Append("</ol>").ToString();

        //}
    }
}
