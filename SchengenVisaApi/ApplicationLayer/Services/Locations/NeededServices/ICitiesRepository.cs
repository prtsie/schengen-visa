﻿using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.NeededServices;

public interface ICitiesRepository : IGenericRepository<City>;
