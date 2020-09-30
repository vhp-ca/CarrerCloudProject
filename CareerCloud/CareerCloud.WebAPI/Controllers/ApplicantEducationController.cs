﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }
        [HttpGet]
        [Route("education/id")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        public ActionResult GetApplicantEducation(Guid id)
        {

            ApplicantEducationPoco poco = _logic.Get(id);
            if (poco != null)
            {
                return Ok(poco);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("education")]
        public ActionResult PostApplicantEducation([FromBody]ApplicantEducationPoco[] pocos)
        {

            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("education")]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {

            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("education")]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {

            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("education")]
        [ProducesResponseType(typeof(List<ApplicantEducationPoco>),200)]
        public ActionResult GetAllApplicantEducation()
        {
           return Ok(_logic.GetAll());
        }
    }
}
