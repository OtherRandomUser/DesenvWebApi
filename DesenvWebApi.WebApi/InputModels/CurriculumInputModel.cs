using System;
using System.Collections.Generic;
using DesenvWebApi.WebApi.ViewModels;

namespace DesenvWebApi.WebApi.InputModels
{
    public class CurriculumInputModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<SubjectViewModel> Subjects {get; set;}
    }
}