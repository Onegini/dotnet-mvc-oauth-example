﻿using System.Collections.Generic;
using ExampleModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAspCoreResourceGatewayExample.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        //These endpoints will be accessible without authentication, except when global authorize policy is set
        
        // GET api/wishlist/5
        [HttpGet("{id}")]
        public ActionResult<WishList> GetWishList(int id)
        {
            return new WishList(){ UserId = id, wishes = new List<string>() {"wish 1", "wish 2"}};
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string wishListName)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }

    }
}