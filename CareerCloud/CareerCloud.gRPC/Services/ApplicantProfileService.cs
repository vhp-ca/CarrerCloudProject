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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<Empty> AddApplicantProfile(ApplicantProfiles request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfileReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteApplicantProfile(ApplicantProfiles request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfileReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<ApplicantProfileReply> GetApplicantProfile(ApplicantProfileIdRequest request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<ApplicantProfileReply>(FromPOCO(poco));
        }
        public override Task<ApplicantProfiles> GetApplicantProfiles(Empty request, ServerCallContext context)
        {
            ApplicantProfiles CollectionofApplicantProfile = new ApplicantProfiles();
            List<ApplicantProfilePoco> pocos = _logic.GetAll();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                CollectionofApplicantProfile.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<ApplicantProfiles>(CollectionofApplicantProfile);
        }
        public override Task<Empty> UpdateApplicantProfile(ApplicantProfiles request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfileReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private ApplicantProfileReply FromPOCO(ApplicantProfilePoco poco)
        {
            return new ApplicantProfileReply()
            {
                 Id = poco.Id.ToString(),
                 Login = poco.Login.ToString(),
                 CurrentSalary = poco.CurrentSalary,
                 CurrentRate = poco.CurrentRate,
                 Currency = poco.Currency,
                 Country = poco.Country,
                 Province = poco.Province,
                 Street = poco.Street,
                 City = poco.City,
                 PostalCode = poco.PostalCode,
                 TimeStamp = ByteString.CopyFrom(poco.TimeStamp)

            };
        }
        private ApplicantProfilePoco ToPOCO(ApplicantProfileReply reply)
        {
            return new ApplicantProfilePoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = Guid.Parse(reply.Login),
                CurrentSalary = reply.CurrentSalary,
                CurrentRate = reply.CurrentRate,
                Currency = reply.Currency,
                Country = reply.Country,
                Province = reply.Province,
                Street = reply.Street,
                City = reply.City,
                PostalCode = reply.PostalCode,
                TimeStamp = reply.TimeStamp.ToByteArray()

            };
        }
    }
}
