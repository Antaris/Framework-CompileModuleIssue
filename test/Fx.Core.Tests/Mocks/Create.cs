namespace Fx.Tests.Mocks
{
    public abstract class Creature
    {
        public abstract string Sound { get; }
    }

    public class Person : Creature
    {
        public Person(string forename, string surname)
        {
            Forename = forename;
            Surname = surname;
        }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public override string Sound
        {
            get
            {
                return $"My name is {Forename} {Surname}";
            }
        }
    }

    public class Dog : Creature
    {
        public Dog(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string Sound
        {
            get
            {
                return "Woof!";
            }
        }
    }

    public class Cat : Creature
    {
        public override string Sound
        {
            get
            {
                return "Meow";
            }
        }
    }
}
