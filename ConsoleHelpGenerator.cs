using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleAppHelp
{
    public class ConsoleHelpGenerator
    {
        public static void Map<T>(T obj, string[] attr)
        {
            try
            {
                Dictionary<string, string> userData = new Dictionary<string, string>();

                for (int i = 0; i < attr.Length / 2; i++)
                {
                    userData.Add(attr[i].Substring(1, attr[i].Length - 1), attr[i + 1]);
                }

                ValidateUserEntry<T>(userData);

                var properties = typeof(T).GetProperties()
                .Where(prop => prop.IsDefined(typeof(Help), false));

                foreach (var prop in properties)
                {
                    if (userData.ContainsKey(prop.Name))
                    {
                        prop.SetValue(obj, userData[prop.Name]);
                    }
                }
            }
            catch (Exception)
            {
                DisplayHelptext<T>();
            }
        }

        private static void ValidateUserEntry<T>(Dictionary<string, string> userData)
        {
            MemberInfo info = typeof(T);
            object[] helpAttribute = info.GetCustomAttributes(typeof(Help), false);

            foreach (var prop in helpAttribute)
            {
                if ((prop as Help).Mandetory && userData.ContainsKey((prop as Help).Name))
                {
                    Console.WriteLine($"Missing parameter -{(prop as Help).Name}");
                }
            }
        }

        private static void DisplayHelptext<T>()
        {
            Help[] helpAttribute = (Help[])Attribute.GetCustomAttributes(typeof(T), typeof(Help));

            foreach (var prop in helpAttribute)
            {
                Console.WriteLine($"-{prop.Name} is {prop.Mandetory}");
                Console.WriteLine($"-{prop.Description}");
            }
        }
    }
}
