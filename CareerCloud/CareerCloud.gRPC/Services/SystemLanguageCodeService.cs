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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<Empty> AddSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodeReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodeReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodeReply reply in request.AppEdus)
            {
                pocos.Add(ToPOCO(reply));

            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<SystemLanguageCodeReply> GetSystemLanguageCode(SystemLanguageCodeIdRequest request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.Id);
            return Task.FromResult<SystemLanguageCodeReply>(FromPOCO(poco));
        }
        public override Task<SystemLanguageCodes> GetSystemLanguageCodes(Empty request, ServerCallContext context)
        {
            SystemLanguageCodes CollectionofSystemLanguageCode = new SystemLanguageCodes();
            List<SystemLanguageCodePoco> pocos = _logic.GetAll();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                CollectionofSystemLanguageCode.AppEdus.Add(FromPOCO(poco));
            }
            return Task.FromResult<SystemLanguageCodes>(CollectionofSystemLanguageCode);
        }

        private SystemLanguageCodeReply FromPOCO(SystemLanguageCodePoco poco)
        {
            return new SystemLanguageCodeReply()
            {
                 LanguageID = poco.LanguageID,
                 Name = poco.Name,
                 NativeName = poco.NativeName


            };
        }
        private SystemLanguageCodePoco ToPOCO(SystemLanguageCodeReply reply)
        {
            return new SystemLanguageCodePoco()
            {
                LanguageID = reply.LanguageID,
                Name = reply.Name,
                NativeName = reply.NativeName

            };
        }
    }
}
