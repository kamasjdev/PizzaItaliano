﻿using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaItaliano.Services.Payments.Application.Events.Rejected
{
    [Contract]
    public class UpdatePaymentRejected : IRejectedEvent
    {
        public Guid PaymentId { get; }
        public string Reason { get; }
        public string Code { get; }

        public UpdatePaymentRejected(Guid paymentId, string reason, string code)
        {
            PaymentId = paymentId;
            Reason = reason;
            Code = code;
        }
    }
}
