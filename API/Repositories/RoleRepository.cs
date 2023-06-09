﻿using API.Contexts;
using API.Models;
using API.Contracts;

namespace API.Repositories;
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
 
    public RoleRepository(BookingManagementDbContext context) : base(context) { }

/*
    public Role Create(Role role)
    {
        try
        {
            _context.Set<Role>().Add(role);
            _context.SaveChanges();
            return role;
        }
        catch
        {
            return new Role();
        }
    }
    public bool Update(Role role)
    {
        try
        {
            _context.Set<Role>().Update(role);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Delete(Guid guid)
    {
        try
        {
            var role = GetByGuid(guid);
            if (role == null)
            {
                return false;
            }
            _context.Set<Role>().Remove(role);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Role> GetAll()
    {
        return _context.Set<Role>().ToList();

    }

    public Role? GetByGuid(Guid guid)
    {
        return _context.Set<Role>().Find(guid);
    }*/
}
