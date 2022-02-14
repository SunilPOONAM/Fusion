using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class OpportunityBase 
    {
        public abstract Opportunity GetOpportunity(string id);
        public abstract int AddOpportunity(Opportunity opp);
        public abstract int GetRecentOpportunity();
        public abstract bool UpdateOpportunity(Opportunity opp);
        public abstract bool CloseOpportunity(Opportunity opp);
        public abstract bool PromoteOpportunity(Opportunity opp);
        public abstract List<Opportunity> GetAllOpportunity();
    }
}