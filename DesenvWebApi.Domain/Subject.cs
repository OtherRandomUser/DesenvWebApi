using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesenvWebApi.Domain
{
    public class Subject : Entity
    {
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public ICollection<SubjectCurriculum> Curriculums { get; protected set; }

        private Subject()
        {
        }

        public Subject(string code, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("Subject code can't be null");

            Code = code;

            SetName(name);
            SetDescription(description);

            Curriculums = new Collection<SubjectCurriculum>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Subject name can't be null");

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("Subject description can't be null");

            Description = description;
        }
    }
}