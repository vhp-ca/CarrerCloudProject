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
    public class ApplicantJobApplicationController : ControllerBase
    {
        private ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationController()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }

        [HttpGet]
        [Route("jobapplication/id")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        public ActionResult GetApplicantJobApplication(Guid id)
        {

            ApplicantJobApplicationPoco poco = _logic.Get(id);
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
        [Route("jobapplication")]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobapplication")]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobapplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("jobapplication")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        public ActionResult GetAllApplicantJobApplication()
        {
            return Ok(_logic.GetAll());
        }
    }


}
