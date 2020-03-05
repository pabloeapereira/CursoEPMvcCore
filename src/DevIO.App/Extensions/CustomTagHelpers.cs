using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace DevIO.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class SupressByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SupressByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));
            if(output==null)throw new ArgumentNullException(nameof(output));

            var acessExists = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (acessExists) return;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DisableLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var acessExists = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (acessExists) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style","cursor:not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
        }
    }

    [HtmlTargetElement("a", Attributes = "supress-by-action")]
    public class DeleteElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}