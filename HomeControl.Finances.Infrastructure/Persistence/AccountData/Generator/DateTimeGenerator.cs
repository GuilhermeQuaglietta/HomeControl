using HomeControl.Finances.Infrastructure.Persistence.AccountData;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HomeControl.Finances.Infrastructure.AccountData.Generator
{
    public class DateTimeGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;
        protected override object NextValue([NotNull] EntityEntry entry)
        {
            return DateTime.Now;
        }
    }
}
