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

        public Subject(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("Subject code can't be null");

            Code = code;

            SetName(name);

            Curriculums = new Collection<SubjectCurriculum>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Subject name can't be null");

            Name = name;
        }
    }
}