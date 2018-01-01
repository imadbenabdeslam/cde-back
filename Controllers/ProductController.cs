using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace BackEnd.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseController
    {
        [HttpGet, Route()]
        //[ResponseType(typeof(List<ProductDto>))]
        public IHttpActionResult Get()
        {
            return Ok(new { Id = 0, Name = "Product001" });
        }

        //[HttpPost, Route()]
        //[ResponseType(typeof(ProductDto))]
        //public IHttpActionResult Post([FromBody]ProductDto product)
        //{
        //    var service = new ProductService();

        //    //service.AddProduct(AutoMapper.Mapper.Map<ProductData>(product));

        //    return Ok(product);
        //}
    }
}
