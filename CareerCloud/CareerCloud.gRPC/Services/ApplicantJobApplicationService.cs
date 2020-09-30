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
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }
        public override Task<Empty> AddApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<ApplicantJobApplicationReply> GetApplicantJobApplication(ApplicantJobApplicationIdRequest request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<ApplicantJobApplicationReply>(FromPOCO(poco));
        }
        public override Task<ApplicantJobApplications> GetApplicantJobApplications(Empty request, ServerCallContext context)
        {
            ApplicantJobApplications CollectionofApplicantJobApplication = new ApplicantJobApplications();
            List<ApplicantJobApplicationPoco> pocos = _logic.GetAll();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                CollectionofApplicantJobApplication.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<ApplicantJobApplications>(CollectionofApplicantJobApplication);
        }
        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private ApplicantJobApplicationReply FromPOCO(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = poco.ApplicationDate == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.ApplicationDate),
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)
                               

            }; 
        }
        private ApplicantJobApplicationPoco ToPOCO(ApplicantJobApplicationReply reply)
        {
            return new ApplicantJobApplicationPoco()
            {
                   Id = Guid.Parse(reply.Id),
                   Applicant = Guid.Parse(reply.Applicant),
                   Job = Guid.Parse(reply.Job),
                   ApplicationDate = reply.ApplicationDate.ToDateTime(),

                TimeStamp = reply.TimeStamp.ToByteArray()
            };
        }
    }
}
