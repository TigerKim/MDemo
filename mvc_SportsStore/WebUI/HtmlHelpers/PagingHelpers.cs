using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebUI.HtmlHelpers
{
    // 157page
    public static class PagingHelpers
    {
        public static string PageLinks(this HtmlHelper html, int currentPage,
            int totalPage, Func<int, string> pageUrl )
        {
            StringBuilder result = new StringBuilder();
            for(int i=1; i<=totalPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == currentPage)
                    tag.AddCssClass("selected");
                result.AppendLine(tag.ToString());
            }
            return result.ToString();
        }

    }
}