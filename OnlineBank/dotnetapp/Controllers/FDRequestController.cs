// FDRequestController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/fdrequest")]
    [ApiController]
    public class FDRequestController : ControllerBase
    {
        private readonly FDRequestService _fdRequestService;

        public FDRequestController(FDRequestService fdRequestService)
        {
            _fdRequestService = fdRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDRequest>>> GetAllFDRequests()
        {
            var fdRequests = await _fdRequestService.GetAllFDRequests();
            return Ok(fdRequests);
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<FDRequest>> GetFDRequestById(long requestId)
        {
            var fdRequest = await _fdRequestService.GetFDRequestById(requestId);

            if (fdRequest == null)
            {
                return NotFound(new { message = "Cannot find any FDRequest" });
            }

            return Ok(fdRequest);
        }

        [HttpPost]
        public async Task<ActionResult> AddFDRequest([FromBody] FDRequest fdRequest)
        {
            try
            {
                var success = await _fdRequestService.AddFDRequest(fdRequest);

                if (success)
                {
                    return Ok(new { message = "FDRequest added successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add FDRequest" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Additional methods for update, delete, or other actions

    }
}
