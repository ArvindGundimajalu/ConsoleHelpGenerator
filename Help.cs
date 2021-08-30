using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppHelp
{
    public class Help : Attribute
    {
        public string Name { get; set; }
        public string Description { get; private set; }
        public bool Mandetory { get; private set; }
        public Help(string name, string description, bool mandetory = false)
        {
            Name = name;
            Description = description;
            Mandetory = mandetory;
        }
    }
}
