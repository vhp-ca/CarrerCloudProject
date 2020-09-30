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
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService: ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }
        public override Task<ApplicantEducationReply> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
           ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<ApplicantEducationReply>(FromPOCO(poco));
        }
        public override Task<ApplicantEducations> GetApplicantEducations(Empty request, ServerCallContext context)
        {
            ApplicantEducations CollectionofApplicantEducation = new ApplicantEducations();
            List<ApplicantEducationPoco> pocos = _logic.GetAll();
            foreach (ApplicantEducationPoco poco in pocos)
            {
                CollectionofApplicantEducation.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<ApplicantEducations>(CollectionofApplicantEducation);
        }
        public override Task<Empty> UpdateApplicantEducation(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteApplicantEducation(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> AddApplicantEducation(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        private ApplicantEducationReply FromPOCO(ApplicantEducationPoco poco)
        {
            return new ApplicantEducationReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertificateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = poco.CompletionPercent == null ? 0 :
                       (byte)(poco.CompletionPercent),

                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)


            };
        }
        private ApplicantEducationPoco ToPOCO(ApplicantEducationReply reply)
        {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Major = reply.Major,
                CertificateDiploma = reply.CertificateDiploma,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)reply.CompletionPercent,
                TimeStamp = reply.TimeStamp.ToByteArray()


            };
        }
    }
}
