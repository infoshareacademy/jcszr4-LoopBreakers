using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class TransferReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public TransferReportController(IMapper mapper, IReportService reportService)
        {
            _mapper = mapper;
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            return Ok(await _reportService.GetTransferReportById(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetReport([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetTransferReport(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpGet("CurrencyStatistics")]
        public async Task<IActionResult> GetCurrencyStatistics([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            return Ok(await _reportService.GetCurrencyStatistics(ParseDate.Convert(dateFrom, dateTo)));
        }

        [HttpPost]
        public async Task<IActionResult> AddTransferReport([FromBody] TransferReportDTO transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var transferToCreate = _mapper.Map<TransferReport>(transfer);
            await _reportService.AddTransferReport(transferToCreate);
            return CreatedAtAction(nameof(GetReportById), new { id = transferToCreate.Id }, transferToCreate);
        }
    }
}
