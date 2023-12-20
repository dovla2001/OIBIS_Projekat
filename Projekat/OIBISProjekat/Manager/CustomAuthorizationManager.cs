﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CustomAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            CustomPrincipal principal = operationContext.ServiceSecurityContext.
                AuthorizationContext.Properties["Principal"] as CustomPrincipal;

            bool retValue = principal.IsInRole("Read");

            if (!retValue)
            {
                try
                {
                    Audit.AuthorizationFailed(Formater.ParseName(principal.Identity.Name),
                        OperationContext.Current.IncomingMessageHeaders.Action, "Need Read permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return retValue;
        }
    }
}
