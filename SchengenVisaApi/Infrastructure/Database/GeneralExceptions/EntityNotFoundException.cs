﻿using ApplicationLayer.GeneralExceptions;
using Domains;

namespace Infrastructure.Database.GeneralExceptions;

/// Exception to throw when entity not found
/// <typeparam name="T">Not found entity type</typeparam>
public class EntityNotFoundException<T>(string message) : ApiException(message)
    where T : class, IEntity;
