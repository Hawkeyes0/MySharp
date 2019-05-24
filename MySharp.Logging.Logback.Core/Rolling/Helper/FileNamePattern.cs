using System;
using System.Collections.Generic;
using MySharp.Logging.Logback.Core.Spi;

namespace MySharp.Logging.Logback.Core.Rolling.Helper
{
    public class FileNamePattern : ContextAwareBase
    {
        private string _pattern;
        private Converter<object> _headTokenConverter;

        private static readonly Dictionary<string, string> Converters = new Dictionary<string, string>
        {
            { IntegerTokenConverter.ConverterKey, typeof(IntegerTokenConverter).FullName },
            { DateTokenConverter.ConverterKey, typeof(DateTokenConverter).FullName }
        };

        public FileNamePattern(string pattern, IContext context)
        {
            Pattern = FileFilterUtil.slashify(pattern);
            Context = context;
            Parse();
            ConverterUtil.startConverters(_headTokenConverter);
        }

        public string Pattern
        {
            get => _pattern;
            set
            {
                if (value != null) _pattern = value.Trim();
            }
        }

        internal void Parse()
        {
            try
            {
                string patternForParsing = EscapeRightParantesis(Pattern);
                Parser<object> p = new Parser<object>(patternForParsing, new AlmostAsIsEscapeUtil());
                p.Context = Context;
                Node n = p.Parse();
                _headTokenConverter = p.Compile(n, Converters);
            }
            catch (Exception ex)
            {
                AddError($"Failed to parse pattern \"{Pattern}\".", ex);
            }
        }

        private string EscapeRightParantesis(string str)
        {
            return Pattern.Replace(")", "\\)");
        }

        public override string ToString()
        {
            return Pattern;
        }

        public override int GetHashCode()
        {
            int prime = 31, result = 1;
            return prime * result + (Pattern?.GetHashCode() ?? 0);
        }

        public override bool Equals(object obj)
        {
            FileNamePattern other = obj as FileNamePattern;
            if (other == null)
            {
                return false;
            }

            if (other == this)
            {
                return true;
            }

            if (Pattern == null)
            {
                if (other.Pattern != null) return false;
            }else if (Pattern != other.Pattern)
                return false;

            return true;
        }
    }
}