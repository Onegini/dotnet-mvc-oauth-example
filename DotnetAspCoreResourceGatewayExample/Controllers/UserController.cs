﻿/*
 * Copyright (c) 2017 Onegini B.V.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ExampleModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAspCoreResourceGatewayExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ReadProfile")]
    public class UserController : ControllerBase
    {
        
        //Sample data
        List<User> users = new List<User>()
        {
            new User()
            {
                UserId = "12",
                FirstName = "Person 1",
                LastName = "de Tester",
                Email = "test.persoon@onegini.com",
                BirthDate = new DateTime(1993, 4, 23)
            },
            new User()
            {
                UserId = "44",
                FirstName = "Kees",
                LastName = "Jansen",
                Email = "kees.jansen@example.com",
                BirthDate = new DateTime(1994, 3, 20)
            },
            new User()
            {
                UserId = "93",
                FirstName = "Test",
                LastName = "de Tester 2",
                Email = "test.tester2@example.com",
                BirthDate = new DateTime(2001, 7, 12)
            },
        };

        [HttpGet]
        [Route("/")]
        public ActionResult<string> Home()
        {
            return "Welcome home";
        } 
        
        // GET api/user
        [HttpGet]
        public ActionResult<Result> GetUser()
        {
            var r = new Result();
            
            //Get a claim from the authenticated user
            var identity = (ClaimsIdentity)User.Identity;
            var userIdClaim = identity.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                r.succes = false;
                r.message = "User not found";
            }
            else
            {
                //Query user data
                
                //In the example we just return a static user with UserId set to whatever is used in the `sub` field
                users[0].UserId = userIdClaim.Value;
                r.content = users[0];

                //Example find user data based on `sub` as email
                //r.content = users.First(i => i.Email == userIdClaim.Value);
            }
            
            return r;
        }
        
        [HttpGet]
        [Route("claims")]
        public ActionResult<Result> GetClaims()
        {
            
            //Get all claims from the authenticated user and return them for debug purpose
            var r = new Result();
            
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            
            List<string> claimList = new List<string>();
            foreach (var claim in claims)
            {
                claimList.Add(claim.Type + " -> " + claim.Value);
            }
            
            r.content = claimList;
            
            return r;
        }

        // POST api/values
        [HttpPost]
        public void PostUser([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void PutUser(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
        }
        
        [HttpGet]
        [Route("/api/users")]
        [Authorize(Policy = "ReadProfileAdmin")]
        public ActionResult<Result> GetAll()
        {
            return new Result(){ content = users };
        }
    }
}