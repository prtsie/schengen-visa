﻿using Domains;
using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration
{
    public static class PastVisitConfiguration<T> where T : class, IEntity
    {
        public static void Configure(OwnedNavigationBuilder<T, PastVisit> entity)
        {
            entity.HasOne(p => p.DestinationCountry).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
