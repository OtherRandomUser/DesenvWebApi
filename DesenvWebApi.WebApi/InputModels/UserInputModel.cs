using DesenvWebApi.WebApi.ViewModels;

namespace DesenvWebApi.WebApi.InputModels
{
    public class UserInputModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public CurriculumViewModel Curriculum { get; set; }
    }
}