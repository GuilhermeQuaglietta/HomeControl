using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.Core.Infrastructure.Contract
{
    public interface IEntity
    {
        int Id { get; }
        int OwnerId { get; }
    }
}
