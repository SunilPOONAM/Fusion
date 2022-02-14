using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class DiscussionBase 
    {
        public abstract List<Discussion> GetDiscussions(string assignedObjectType, string id);
        public abstract bool AddDiscussion(Discussion od);
        public abstract bool UpdateDiscussion(Discussion od);
        public abstract Discussion GetDiscussion(string id);


    }
}