using System;

namespace SerializationLess
{
    [Serializable]
    public class Group
    {
        [NonSerialized]
        private readonly Random random = new Random(DateTime.Now.Millisecond);
        public string Name { get; set; }
        public int Number { get; set; }
        public Group()
        {
            Number = random.Next(10, 50);
            Name = "Group " + random;
        }
        public Group(string Name, int Number)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException(nameof(Name), "Wrong group name.");
            else
                this.Name = Name;

            if (Number <= 0)
                throw new ArgumentException(nameof(Number), "Wrong number of group.");
            else
                this.Number = Number;
        }
        public override string ToString()
        {
            return Name + Number;
        }
    }
}
