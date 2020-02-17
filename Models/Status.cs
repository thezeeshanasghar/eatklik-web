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
        Cancel
    }
     public enum RiderStatus
    {
        New,
        Accepted,
        Rejected
    }
}