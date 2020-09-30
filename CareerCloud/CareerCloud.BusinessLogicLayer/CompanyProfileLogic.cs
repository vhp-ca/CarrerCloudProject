﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
  
  public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {

        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)."));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)"));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234)"));
                        }
                    }
                }

                if (poco.CompanyWebsite != null && !(poco.CompanyWebsite.EndsWith(".ca") || poco.CompanyWebsite.EndsWith(".com") || poco.CompanyWebsite.EndsWith(".biz")))
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the extensions - '.ca','.com', '.biz'"));


            }
          
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }

            }
        }
    }
