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
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            EFGenericRepository<CompanyLocationPoco> repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }
        [HttpGet]
        [Route("location/id")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        public ActionResult GetCompanyLocation(Guid id)
        {

            CompanyLocationPoco poco = _logic.Get(id);
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
        [Route("location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {

            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {

            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {

            _logic.Delete(pocos);
            return Ok();
        }

        [HttpGet]
        [Route("location")]
        [ProducesResponseType(typeof(List<CompanyLocationPoco>), 200)]
        public ActionResult GetAllCompanyLocation()
        {
            return Ok(_logic.GetAll());
        }
    }
}
