using System;
using System.Collections.Generic;
using System.Linq;
using DesenvWebApi.Domain;

namespace DesenvWebApi.WebApi.ViewModels
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<CurriculumViewModel> Curriculums { get; set; }

        public static implicit operator SubjectViewModel(Subject subject)
        {
            if (subject is null)
                return null;

            return new SubjectViewModel
            {
                Id = subject.Id,
                CreatedAt = subject.CreatedAt,
                Code = subject.Code,
                Name = subject.Name,
                Description = subject.Description,
            };
        }
    }
}