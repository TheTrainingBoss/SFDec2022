/* ------------------------------------------------------------------------------
<auto-generated>
    This file was generated by Sitefinity CLI v1.1.0.31
</auto-generated>
------------------------------------------------------------------------------ */

using SFDec2022.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow;

namespace SFDec2022.Mvc.Controllers
{
	[ControllerToolboxItem(Name = "PageCrud", Title = "Page Crud", SectionName = "SFDev2022")]
	public class PageCrudController : Controller, IPersonalizable
	{
        PageManager pm = PageManager.GetManager();
        // GET: PageCrud
        public ActionResult Index()
		{
			var pages = pm.GetPageDataList().Where(p => p.Status == ContentLifecycleStatus.Live)
											.Select(p => p.NavigationNode)
											.Where(n => !n.IsBackend);

            var model = new PageCrudModel(pages);
			
			return View("Index", model);
		}
		
        public ActionResult Create()
		{
			var name = "Training Page";
			var id = Guid.NewGuid();

            var parent = pm.GetPageNode(SiteInitializer.CurrentFrontendRootNodeId);
			var pageNode = pm.CreatePage(parent, id, NodeType.Standard);
            pageNode.Name = name.ToLower();
            pageNode.Title = name;
            pageNode.Description = name;
            pageNode.UrlName = Regex.Replace(name.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            pageNode.ShowInNavigation = true;
            pageNode.DateCreated = DateTime.Now;
            pageNode.LastModified = DateTime.UtcNow;
            pageNode.Owner = ClaimsManager.GetCurrentUserId();
            PageData pageData = pageNode.GetPageData();
            pageData.HtmlTitle = name + " Title for search engines";
            pageData.Visible = true;

            pm.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(PageNode).FullName);
            WorkflowManager.MessageWorkflow(id, typeof(PageNode), null, "Publish", false, bag);

            return View("Create");
        }


        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

		
	}
}