using System;
using System.Runtime.Serialization;

namespace SerializationLess
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Group Group { get; set; }
        public Student() { }
        public Student(string Name, string Surname, int Age)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException(nameof(Name), "Wrong type of Name.");
            else
                this.Name = Name;

            if (string.IsNullOrWhiteSpace(Surname))
                throw new ArgumentNullException(nameof(Surname), "Wrong type of Surname.");
            else
                this.Surname = Surname;

            if (Age <= 0)
                throw new ArgumentException(nameof(Age), "Wrong age.");
            else
                this.Age = Age;
        }
        public override string ToString()
        {
            return $"Student:\n\tName: {this.Name};\n\tSurname: {this.Surname};\n\tAge: {this.Age};\n";
        }
    }
}
