using EbayScraper.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbayScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbayScraperController : ControllerBase
    {
        [HttpGet]
        [Route("scrape")]
        public List<EbayItem> Scrape(string searchTerm)
        {
            return EbayScraper.Data.EbayScraper.Scrape(searchTerm);
        }
    }
}
