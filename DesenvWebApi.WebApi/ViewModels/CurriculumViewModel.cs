using System;
using System.Collections.Generic;
using System.Linq;
using DesenvWebApi.Domain;

namespace DesenvWebApi.WebApi.ViewModels
{
    public class CurriculumViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<SubjectViewModel> Subjects { get; set; }

        public static implicit operator CurriculumViewModel(Curriculum curriculum)
        {
            if (curriculum is null)
                return null;

            return new CurriculumViewModel
            {
                Id = curriculum.Id,
                CreatedAt = curriculum.CreatedAt,
                Code = curriculum.Code,
                Name = curriculum.Name,
                Description = curriculum.Description,
                Subjects = curriculum.Subjects.Select(s => (SubjectViewModel) s.Subject)
            };
        }
    }
}