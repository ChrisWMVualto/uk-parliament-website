using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public class SearchApiController : ApiController
    {
        private readonly IMemberService _memberService;

        public SearchApiController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public HttpResponseMessage Index(string query)
        {
            var results = _memberService.Search(query);
            return results != null ? Request.CreateResponse(HttpStatusCode.OK, results) : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}