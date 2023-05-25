﻿using API.Contexts;
using API.Models;
using API.Contracts;

namespace API.Repositories;
public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    public UniversityRepository(BookingManagementDbContext context) : base(context) { }

    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name));
    }
    //k2
    public University CreateWithValidate(University university)
    {
        try
        {
            var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
            var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

            if (existingUniversityWithCode != null & existingUniversityWithName != null)
            {
                university.Guid = existingUniversityWithCode.Guid;

                _context.SaveChanges();

            }

            Create(university);

            return university;

        }
        catch
        {
            return null;
        }
    }

}