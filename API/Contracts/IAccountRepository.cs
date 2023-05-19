﻿using API.Models;

namespace API.Contracts;

    public interface IAccountRepository
    {
    Account Create(Account account);
    bool Update(Account account);
    bool Delete(Guid guid);
    IEnumerable<Account> GetAll();
    Account? GetByGuid(Guid guid);

}

