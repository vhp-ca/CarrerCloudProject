using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }
        public override Task<Empty> AddSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<SecurityLoginsLogReply> GetSecurityLoginsLog(SecurityLoginsLogIdRequest request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<SecurityLoginsLogReply>(FromPOCO(poco));
        }
        public override Task<SecurityLoginsLogs> GetSecurityLoginsLogs(Empty request, ServerCallContext context)
        {
            SecurityLoginsLogs CollectionofSecurityLoginsLog = new SecurityLoginsLogs();
            List<SecurityLoginsLogPoco> pocos = _logic.GetAll();
            foreach (SecurityLoginsLogPoco poco in pocos)
            {
                CollectionofSecurityLoginsLog.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<SecurityLoginsLogs>(CollectionofSecurityLoginsLog);
        }
        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private SecurityLoginsLogReply FromPOCO(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLogReply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = poco.LogonDate == null ? null:
                Timestamp.FromDateTime((DateTime)poco.LogonDate),
                IsSucessful = poco.IsSuccesful

            };
        }
        private SecurityLoginsLogPoco ToPOCO(SecurityLoginsLogReply reply)
        {
            return new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = Guid.Parse(reply.Login),
                SourceIP = reply.SourceIP,
                LogonDate = reply.LogonDate.ToDateTime(),
                IsSuccesful = reply.IsSucessful
 
            };
        }
    }
}
