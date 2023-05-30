using API.Models;
using API.ViewModels.Universities;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Educations;
public class EducationVM
{
    public Guid? Guid { get; set; }

    public string Major { get; set; }
   
    public string Degree { get; set; }

    [Range(0, 4, ErrorMessage = "Input a Valid Number")]
    public float GPA { get; set; }
    
    public Guid UniversityGuid { get; set; }

  /*  public static EducationVM ToVM(Education education)
    {

        return new EducationVM
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }*/

}
