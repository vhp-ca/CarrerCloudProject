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
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }
        public override Task<Empty> AddSecurityLogin(SecurityLogins request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteSecurityLogin(SecurityLogins request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<SecurityLoginReply> GetSecurityLogin(SecurityLoginIdRequest request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));
            return Task.FromResult<SecurityLoginReply>(FromPOCO(poco));
        }
        public override Task<SecurityLogins> GetSecurityLogins(Empty request, ServerCallContext context)
        {
            SecurityLogins CollectionofSecurityLogin = new SecurityLogins();
            List<SecurityLoginPoco> pocos = _logic.GetAll();
            foreach (SecurityLoginPoco poco in pocos)
            {
                CollectionofSecurityLogin.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<SecurityLogins>(CollectionofSecurityLogin);
        }
        public override Task<Empty> UpdateSecurityLogin(SecurityLogins request, ServerCallContext context)
        {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        private SecurityLoginReply FromPOCO(SecurityLoginPoco poco)
        {
            return new SecurityLoginReply()
            {
                 Id = poco.Id.ToString(),
                 Login = poco.Login.ToString(),
                 Password = poco.Password.ToString(),
                Created = poco.Created == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.Created),
                PasswordUpdate = poco.PasswordUpdate == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                AgreementAccepted = poco.AgreementAccepted == null ? null :
                       Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                 IsLocked = poco.IsLocked,
                 IsInactive = poco.IsInactive,
                 EmailAddress = poco.EmailAddress,
                 PhoneNumber = poco.PhoneNumber,
                 FullName = poco.PhoneNumber,
                 ForceChangePassword = poco.ForceChangePassword,
                 PreferredLanguage = poco.PrefferredLanguage,
                TimeStamp = ByteString.CopyFrom(poco.TimeStamp)


            };
        }
        private SecurityLoginPoco ToPOCO(SecurityLoginReply reply)
        {
            return new SecurityLoginPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = reply.Login.ToString(),
                Password = reply.Password.ToString(),
                Created = reply.Created.ToDateTime(),
                PasswordUpdate = reply.PasswordUpdate.ToDateTime(),
                AgreementAccepted = reply.AgreementAccepted.ToDateTime(),
                IsLocked = reply.IsLocked,
                IsInactive = reply.IsInactive,
                EmailAddress = reply.EmailAddress,
                PhoneNumber = reply.PhoneNumber,
                FullName = reply.PhoneNumber,
                ForceChangePassword = reply.ForceChangePassword,
                PrefferredLanguage = reply.PreferredLanguage,
                TimeStamp = reply.TimeStamp.ToByteArray()


            };
        }
    }
}
