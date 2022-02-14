using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared
{
    public enum DeliveryMethods
    {
        [Description(" ")]
        None = 0,
        [Description("Email")]
        Email = 1,
        [Description("Consolidate/Upload")]
        ConsolidateUpload = 2,
        [Description("Mail")]
        Mail = 3,
    }

    public enum SignMethods : int
    {
        [Description(" ")]
        None = 0,
        [Description("A Sign Quote is as good as a signed ticket")]
        SignQuote = 1,
        [Description("Need Signature on Ticket")]
        SignTicket = 2,
    }

    public enum POMethods : int
    {
        [Description(" ")]
        None = 0,
        [Description("Not Required")]
        NotRequired = 1,
        [Description("Number Required")]
        NumberRequired = 2,
        [Description("Number Required with over $xxx")]
        NumberRequiredOver = 3,
        [Description("Number Required with amount to be allocated to work")]
        NumberRequiredWithAmount = 4,
    }

    public enum AttachMethods : int
    {
        [Description(" ")]
        None = 0,
        [Description("Attach to Invoice")]
        AttachToInvoice = 1,
        [Description("No Attachment Required (but include PO# on Printed Invoice)")]
        NoAttachment = 2,
    }
}
