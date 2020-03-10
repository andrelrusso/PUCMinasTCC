using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Utils
{
    public static class HtmlUtil
    {
        public static string ToHtmlString(this IHtmlContent html)
        {
            using (var sw = new StringWriter())
            {
                html.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                return sw.ToString();
            }
        }
        public static IHtmlContent Pagination<T>(this IHtmlHelper helper, PagedList<T> pagedList, string actionLink, string selectorReplace = null, object htmlAttributes = null)
        {
            if (pagedList == null || pagedList.TotalPages < 2) return null;

            var html = new StringBuilder();

            var iconPrev = new TagBuilder("i");
            iconPrev.AddCssClass("fas fa-angle-double-left");
            var aPrevious = new TagBuilder("a");
            aPrevious.AddCssClass("page-link");

            if (pagedList.HasPreviousPage)
            {
                if (string.IsNullOrEmpty(selectorReplace))
                    aPrevious.MergeAttribute("onclick", actionLink);
                else
                    aPrevious.MergeAttribute("onclick", actionLink.Replace(selectorReplace, (pagedList.ActualPage - 1).ToString()));
            }

            aPrevious.InnerHtml.SetHtmlContent(iconPrev.ToHtmlString());
            var liPrevious = new TagBuilder("li");
            liPrevious.InnerHtml.SetHtmlContent(aPrevious.ToHtmlString());
            if (!pagedList.HasPreviousPage)
                liPrevious.AddCssClass("disabled");
            liPrevious.AddCssClass("page-item");

            html.Append(liPrevious.ToHtmlString());

            for (int i = 1; i <= pagedList.TotalPages; i++)
            {
                TagBuilder a = new TagBuilder("a");
                a.AddCssClass("page-link");
                if (string.IsNullOrEmpty(selectorReplace))
                    a.MergeAttribute("onclick", actionLink);
                else
                    a.MergeAttribute("onclick", actionLink.Replace(selectorReplace, i.ToString()));
                a.InnerHtml.SetHtmlContent(i.ToString());
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("page-item");
                if (pagedList.ActualPage == i)
                    li.AddCssClass("active");
                li.InnerHtml.SetHtmlContent(a.ToHtmlString());
                html.Append(li.ToHtmlString());
            }

            var iconNext = new TagBuilder("i");
            iconNext.AddCssClass("fas fa-angle-double-right");
            var aNext = new TagBuilder("a");
            aNext.AddCssClass("page-link");

            if (pagedList.HasNextPage)
            {
                if (string.IsNullOrEmpty(selectorReplace))
                    aNext.MergeAttribute("onclick", actionLink);
                else
                    aNext.MergeAttribute("onclick", actionLink.Replace(selectorReplace, (pagedList.ActualPage + 1).ToString()));
            }
            aNext.InnerHtml.SetHtmlContent(iconNext.ToHtmlString());
            var liNext = new TagBuilder("li");
            liNext.InnerHtml.SetHtmlContent(aNext.ToHtmlString());

            if (!pagedList.HasNextPage)
                liNext.AddCssClass("disabled");
            liNext.AddCssClass("page-item");
            html.Append(liNext.ToHtmlString());

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            ul.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            ul.InnerHtml.SetHtmlContent(html.ToString());
            var nav = new TagBuilder("nav");
            nav.InnerHtml.SetHtmlContent(ul.ToHtmlString());

            return new HtmlString(nav.ToHtmlString());
        }

        public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, object selectedValue = null)
        {
            if (items == null) return null;

            if (selectedValue != null)
                return new SelectList(items, dataValueField, dataTextField, selectedValue);
            else
                return new SelectList(items, dataValueField, dataTextField);

        }
    }
}
