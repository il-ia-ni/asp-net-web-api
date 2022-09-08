﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;  // contains attrs to change the behaviour of a controller
using Microsoft.EntityFrameworkCore;
using Tracking_api.Data;
using Tracking_api.Models;

namespace Tracking_api.Controllers
// Added by: RMC on Controllers folder -> Add -> Controller -> Common/API/ -> API COntroller empty
// Controller-based classes contain actions that process certain requests. 

{
    [Route("api/[controller]")]  // maps requests to actions, can be applied at controller- or action-level
    // the string specifes a url to request the api: api/issue ([controller] = "IssueController" - "Controller")
    [ApiController]  // attr applies common conventions to the controller (autovalidation of the model, binding req data to model, etc)
    public class IssueController : ControllerBase  // ControllerBase cls provides methods to manage HTTP reqs (such as NotFound, Ok, etc)
    {
        private readonly IssueDbContext _context;

        public IssueController(IssueDbContext context) => _context = context;
        // a constructor adds an instance of a DB at runtime to allow controller access the DB

        [HttpGet]  // attr specifies that the action method is a handler for HTTP GET requests
        public async Task<IEnumerable<Issue>> Get()
        // an async handler action for GET requests to get all data from the DB
        {
            return await _context.Issues.ToListAsync();
        }

        [HttpGet("{id}")]  // attr specifies that the action method is a handler for HTTP GET by id requests using the id placeholder
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]  // the attr enhanses documentation of the action specifing which status code is returned
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        /* an async handler action for GET requests with a url in the head of a request api/issue/5 to get a specific item from the DB 
         Returns an IAction result, not a response as we cannot return 2 types of response (OK / Not found) */
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue); // NotFound generates a 404-status code if the action was not successful, Ok - 200

        }

    }
}
