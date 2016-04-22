﻿using Ilaro.Admin.Configuration;
using Ilaro.Admin.Sample.Models.Northwind;

namespace Ilaro.Admin.Sample.Configurators
{
    public class TerritoryConfiguration : EntityConfiguration<Territory>
    {
        public TerritoryConfiguration()
        {
            Property(x => x.TerritoryID, x => x.StringLength(20));

            Property(x => x.TerritoryDescription, x =>
            {
                x.Required();
                x.StringLength(50);
            });

            Property(x => x.Region, x =>
            {
                x.Required();
                x.ForeignKey("RegionID");
            });
        }
    }
}