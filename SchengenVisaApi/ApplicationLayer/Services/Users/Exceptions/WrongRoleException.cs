using ApplicationLayer.GeneralExceptions;
using Domains.Users;

namespace ApplicationLayer.Services.Users.Exceptions;

public class WrongRoleException(Guid userId) : EntityNotFoundByIdException<User>(userId);
