﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class TransferReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public TransferReportController(IMapper mapper, IReportService reportService)
        {
            _mapper = mapper;
            _reportService = reportService;
        }

        // GET: api/<TransferReportController>
        [HttpGet("{id}")]
        public IEnumerable<string> GetReportById()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            return Ok(await _reportService.GetAllTransferReports());
        }

        // GET
        [HttpGet("ByDate")]
        public async Task<IActionResult> GetReportByDate([FromQuery]string dateFrom, [FromQuery]string dateTo)
        {
            DateTime apiDateFrom;
            if (!DateTime.TryParse(dateFrom, out apiDateFrom))
            {
                return BadRequest();
            };
            DateTime apiDateTo;
            if (!DateTime.TryParse(dateTo, out apiDateTo))
            {
                return BadRequest();
            };
            return Ok(await _reportService.GetTransferReportByDate(apiDateFrom, apiDateTo));
        }

        // POST api/<TransferReportController>
        [HttpPost]
        public async Task<IActionResult> AddTransferReport([FromBody] TransferReportDTO transfer)
        {
            var transferToCreate = _mapper.Map<TransferReport>(transfer);
            await _reportService.AddTransferReport(transferToCreate);
            return CreatedAtAction(nameof(GetReportById), new { id = transferToCreate.Id }, transferToCreate);
        }

        // PUT api/<TransferReportController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransferReportController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
