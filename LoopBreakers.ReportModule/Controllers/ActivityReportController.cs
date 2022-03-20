using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.ReportModule.Helpers;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            return Ok(await _reportService.GetActivityReportById(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetReport([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetActivityReport(ParseDate.Convert(dateFrom, dateTo)));
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
        [HttpGet("LoginStatistics")]
        public async Task<IActionResult> GetLoginStatistics([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetLoginStatistics(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpGet("TransferStatistics")]
        public async Task<IActionResult> GetTransferStatistics([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetTransferStatistics(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpGet("RegisterStatistics")]
        public async Task<IActionResult> GetRegisterStatistics([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetRegisterStatistics(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpGet("ActivityStatistics")]
        public async Task<IActionResult> GetActivityStatistics([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetActivityStatistics(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpGet("MostCommonTransferHours")]
        public async Task<IActionResult> GetTransferStatisticsByHours([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetTransferStaticsByHours(ParseDate.Convert(dateFrom, dateTo)));
        }
    }
}
