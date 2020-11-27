using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesenvWebApi.Domain
{
    public class Curriculum : Entity
    {
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public ICollection<SubjectCurriculum> Subjects { get; protected set; }

        private Curriculum()
        {
        }

        public Curriculum(string code, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("Curriculum code can't be null");

            Code = code;

            SetName(name);
            SetDescription(description);

            Subjects = new Collection<SubjectCurriculum>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Curriculum name can't be null");

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("Curriculum description can't be null");

            Description = description;
        }
    }
}