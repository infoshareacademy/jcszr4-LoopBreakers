﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransferReportController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransferReportController>
        [HttpPost]
        public async Task<IActionResult> AddTransferReport([FromBody] TransferReportDTO transfer)
        {
            var transferToCreate = _mapper.Map<TransferReport>(transfer);
            await _reportService.AddTransferReport(transferToCreate);
            return Ok();
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