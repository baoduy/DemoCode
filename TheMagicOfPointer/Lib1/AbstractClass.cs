using System;

namespace Lib1
{
    public abstract class AbstractClass
    {
        public string Name => typeof(AbstractClass).Name;

        public string OtherName => this.GetType().Name;

        public void PrintNames()
        {
            Console.WriteLine($"{nameof(Name)}: {Name}, {nameof(OtherName)}: {OtherName}");
        }

        public void PrintAssemblyNames()
        {
            Console.WriteLine($"Assembly of Type: {typeof(AbstractClass).Assembly.GetName().Name}, Assembly of pointer: {this.GetType().Assembly.GetName().Name}");
        }
    }
}
