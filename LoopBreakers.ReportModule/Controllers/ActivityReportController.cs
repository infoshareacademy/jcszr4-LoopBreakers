using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.ReportModule.Models;
using LoopBreakers.ReportModule.Services;


namespace LoopBreakers.ReportModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public ActivityReportController(IMapper mapper, IReportService reportService)
        {
            _mapper = mapper;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            return Ok(await _reportService.GetAllActivityReports());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            return Ok(await _reportService.GetTransferReportById(id));
        }

        [HttpGet("ByDate")]
        public async Task<IActionResult> GetReportByDate([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            if (!DateTime.TryParse(dateFrom, out var apiDateFrom))
            {
                return BadRequest();
            };

            if (!DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return BadRequest();
            };
            return Ok(await _reportService.GetActivityReportByDate(apiDateFrom, apiDateTo));
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityReport([FromBody] ActivityReportDTO activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var activityToCreate = _mapper.Map<ActivityReport>(activity);
            await _reportService.AddActivityReport(activityToCreate);
            return CreatedAtAction(nameof(GetReportById), new { id = activityToCreate.Id }, activityToCreate);
        }
    }
}
