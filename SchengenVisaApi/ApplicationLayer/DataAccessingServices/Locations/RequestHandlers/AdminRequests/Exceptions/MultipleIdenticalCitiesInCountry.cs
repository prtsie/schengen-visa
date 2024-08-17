﻿using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.DataAccessingServices.Locations.RequestHandlers.AdminRequests.Exceptions
{
    public class MultipleIdenticalCitiesInCountry() : ApiException("There are multiple cities with one name in the country.");
}
