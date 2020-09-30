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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
         private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }
        public override Task<Empty> AddCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<CompanyJobReply> GetCompanyJob(CompanyJobIdRequest request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<CompanyJobReply>(FromPOCO(poco));
        }
        public override Task<CompanyJobs> GetCompanyJobs(Empty request, ServerCallContext context)
        {
            CompanyJobs CollectionofCompanyJob = new CompanyJobs();
            List<CompanyJobPoco> pocos = _logic.GetAll();
            foreach (CompanyJobPoco poco in pocos)
            {
                CollectionofCompanyJob.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<CompanyJobs>(CollectionofCompanyJob);
        }
        public override Task<Empty> UpdateCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private CompanyJobReply FromPOCO(CompanyJobPoco poco)
        {
            return new CompanyJobReply()
            {
                 Id = poco.Id.ToString(),
                 Company = poco.Company.ToString(),
                 ProfileCreated = poco.ProfileCreated == null ? null :
                 Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                 IsInactive = poco.IsInactive,
                 IsCompanyHidden = poco.IsCompanyHidden,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)

            };
        }
        private CompanyJobPoco ToPOCO(CompanyJobReply reply)
        {
            return new CompanyJobPoco()
            {
                Id = Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                ProfileCreated = reply.ProfileCreated.ToDateTime(),
                IsInactive = reply.IsInactive,
                IsCompanyHidden = reply.IsCompanyHidden,
                TimeStamp = reply.TimeStamp.ToByteArray()

            };
        }
    }
}
