using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public enum Status
    {
        Enable,
        Disable
    }

    
    public enum OrderStatus
    {
        New,
        Active,
        Dispatch,
        Complete,
        Cancel,
        Assigned,
        RiderAccepted,
        RiderRejected,

    }
     public enum RiderStatus
    {
        New,
        Accepted,
        Rejected
    }
}