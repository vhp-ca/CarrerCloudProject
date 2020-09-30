using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {
            _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }
        public override Task<Empty> AddCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<CompanyJobEducationReply> GetCompanyJobEducation(CompanyJobEducationIdRequest request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<CompanyJobEducationReply>(FromPOCO(poco));
        }
        public override Task<CompanyJobEducations> GetCompanyJobEducations(Empty request, ServerCallContext context)
        {
            CompanyJobEducations CollectionofCompanyJobEducation = new CompanyJobEducations();
            List<CompanyJobEducationPoco> pocos = _logic.GetAll();
            foreach (CompanyJobEducationPoco poco in pocos)
            {
                CollectionofCompanyJobEducation.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<CompanyJobEducations>(CollectionofCompanyJobEducation);
        }
        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private CompanyJobEducationReply FromPOCO(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducationReply()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Job.ToString(),
                Importance = poco.Importance,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)

            };
        }
        private CompanyJobEducationPoco ToPOCO(CompanyJobEducationReply reply)
        {
            return new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Job = Guid.Parse(reply.Job),
                Major = reply.Job,
                Importance = (short)(reply.Importance),
                TimeStamp = reply.TimeStamp.ToByteArray()


            };
        }
    }
}
