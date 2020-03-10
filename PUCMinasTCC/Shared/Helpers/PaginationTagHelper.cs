using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using PUCMinasTCC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Shared.Helpers
{

    public class PaginationTagHelper<T> : TagHelper
    {

        public PagedList<T> pagedList { get; set; }


        public string actionLink { get; set; }


        public string selectorReplace { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (pagedList == null || pagedList.TotalPages < 2) return;

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
            ul.InnerHtml.SetHtmlContent(html.ToString());

            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(ul.ToHtmlString());
        }
    }
}
