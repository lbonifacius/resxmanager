using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace ResourceManager.Core.Configuration
{
    public class EntityNameMatcher
    {
        public EntityNameMatcher(XPathNavigator cfg)
        {
            if (cfg == null)
                throw new ArgumentNullException("cfg");

            Operators op = Operators.Like;
            if (Enum.TryParse<Operators>(cfg.GetAttribute("Operator", ""), out op))
                Operator = op;
            else
                Operator = Operators.Like;
            
            Pattern = cfg.GetAttribute("Pattern", "");

            // Equals and Like case insensitive.
            if (Operator == Operators.Equals || Operator == Operators.Like)
                Pattern = Pattern.ToUpperInvariant();
        }

        public string Pattern
        {
            get;
            private set;
        }
        public Operators Operator
        {
            get;
            private set;
        }

        public bool IsMatch(string name)
        {
            if (String.IsNullOrEmpty(name))
                return false;

            switch (Operator)
            { 
                case Operators.Equals:
                    return name.ToUpperInvariant() == Pattern;

                case Operators.Like:
                    return name.ToUpperInvariant().Contains(Pattern);

                case Operators.Regex:
                    return Regex.IsMatch(name, Pattern);

                default:
                    return false;
            }
        }
    }

    public enum Operators
    { 
        Equals,
        Like,
        Regex
    }
}
