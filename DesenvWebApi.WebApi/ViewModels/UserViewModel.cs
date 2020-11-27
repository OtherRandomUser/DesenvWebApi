using System;
using DesenvWebApi.Domain;

namespace DesenvWebApi.WebApi.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public CurriculumViewModel Curriculum { get; set; }

        public static implicit operator UserViewModel(User user)
        {
            if (user is null)
                return null;

            return new UserViewModel
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Name = user.Name,
                Curriculum = user.Curriculum
            };
        }
    }
}