/* ------------------------------------------------------------------------------
<auto-generated>
    This file was generated by Sitefinity CLI v1.1.0.31
</auto-generated>
------------------------------------------------------------------------------ */

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.News.Model;

namespace SFDec2022.Mvc.Models
{
	public class NewsCrudModel
	{
        public List<NewsItemInfo> NewsItemsInfo
        {
            get;
            private set;
        }
        public class NewsItemInfo
        {
            public string NewsTitle { get; set; }
        }

        public NewsCrudModel(IQueryable<NewsItem> news)
        {
            this.NewsItemsInfo = GetNewsInfo(news);
        }

        private List<NewsItemInfo> GetNewsInfo(IQueryable<NewsItem> news)
        {
            List<NewsItemInfo> newsItemsinfo = new List<NewsItemInfo>();
            foreach (var n in news)
            {
                NewsItemInfo newsiteminfo = new NewsItemInfo();
                newsiteminfo.NewsTitle = n.Title;
                newsItemsinfo.Add(newsiteminfo);
            }
            return newsItemsinfo;
        }
    }
}