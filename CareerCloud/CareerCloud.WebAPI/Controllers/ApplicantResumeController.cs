using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;


namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            EFGenericRepository<ApplicantResumePoco> repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }
        [HttpGet]
        [Route("resume/id")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        public ActionResult GetApplicantResume(Guid id)
        {

            ApplicantResumePoco poco = _logic.Get(id);
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
        [Route("resume")]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {

            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("resume")]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {

            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("resume")]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {

            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("resume")]
        [ProducesResponseType(typeof(List<ApplicantResumePoco>), 200)]
        public ActionResult GetAllApplicantResume()
        {
            return Ok(_logic.GetAll());
        }
    }
}
