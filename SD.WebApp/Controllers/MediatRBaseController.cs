using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SD.WebApp.Controllers
{
    public class MediatRBaseController : Controller // bei ASP.Net kein ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => this.mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
