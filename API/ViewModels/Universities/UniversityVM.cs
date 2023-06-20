using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModels.Universities;
    public class UniversityVM
    {
    public Guid? Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    /*public static UniversityVM ToVM(University university)
    {
        return new UniversityVM
        {
            Guid = university.Guid,
            Code = university.Code,
            Name = university.Name
        };
    }

    public static University ToModel(UniversityVM universityVM)
    {
        return new University()
        {
            Guid = System.Guid.NewGuid(),
            Code = universityVM.Code,
            Name = universityVM.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }*/
}

