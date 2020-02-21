using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace SerializationLess
{
    class Program
    {
        public static Random random = new Random(DateTime.Now.Millisecond);
        public static string[] Names =
        {
            "Roman", "Sasha", "Vika", "Ira", "Mukola", "Elina",
            "Mia", "Volodya", "Erika", "Orest", "Vsevolod", "Edward"
        };
        public static string[] Surnames =
        {
            "Cholkan", "Smith", "Anderson",
            "Black", "Stone", "Arkenson", "Hilton"
        };
        static void Main(string[] args)
        {
            var groups = new List<Group>();
            var students = new List<Student>();

            for (int i = 1; i < 10; ++i)
                groups.Add(new Group("Group", i));

            for (int i = 0; i < 300; ++i)
            {
                var student = new Student()
                {
                    Name = Names[random.Next(Names.Length)],
                    Surname = Surnames[random.Next(Surnames.Length)],
                    Age = random.Next(16, 21),
                    Group = groups[i % 9]
                };
                students.Add(student);
            }
            #region BinarySerializator
            var binary_formatter = new BinaryFormatter();

            //сиріалізація (зберігання інформації)
            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
                binary_formatter.Serialize(file, groups);

            //десиарілізаці (витягування інформації з файлика)
            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                var newGroups = binary_formatter.Deserialize(file) as List<Group>;
                if (newGroups != null)
                    foreach (var group in newGroups)
                        Console.WriteLine(group);
            }
            #endregion
            Console.ReadLine();
            #region SoapSeriazlizator
            var soap_formatter = new SoapFormatter();

            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
                soap_formatter.Serialize(file, groups.ToArray());
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                var newGroups = soap_formatter.Deserialize(file) as Group[];
                if (newGroups != null)
                    foreach (var group in newGroups)
                        Console.WriteLine(group);
            }
            #endregion
            Console.ReadLine();
            #region XMLSerializator
            var xml_formatter = new XmlSerializer(typeof(List<Group>));

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
                xml_formatter.Serialize(file, groups);
            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                var newGroups = xml_formatter.Deserialize(file) as List<Group>;
                if (newGroups != null)
                    foreach (var group in newGroups)
                        Console.WriteLine(group);
            }
            #endregion
            Console.ReadLine();
            #region JsonSerializator
            var json_formatter = new DataContractJsonSerializer(typeof(List<Student>));

            using (var file = new FileStream("students.json", FileMode.Create))
                json_formatter.WriteObject(file, students);
            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                var newStudents = json_formatter.ReadObject(file) as List<Student>;
                if (newStudents != null)
                    foreach (var student in newStudents)
                        Console.WriteLine(student);
            }
            #endregion
            Console.ReadLine();
        }
    }
}
