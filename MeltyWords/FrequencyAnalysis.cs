using NMeCab;
using NMeCab.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MeltyWords
{
    /// <summary>
    /// 頻度分析のためのクラス
    /// </summary>
    public class FrequencyAnalysis
    {
        public class MinimumUnit
        {
            public string Name { get; set; }
            public override string ToString()
            {
                return Name;
            }
            public static explicit operator string(MinimumUnit unit)
            {
                return unit.ToString();
            }
        }
        public class JapaneseUnit : MinimumUnit
        {
            public string Part { get; set; }
        }

        public string Text { get; set; } = string.Empty;
        public string RemovePattern { get; set; } = string.Empty;
        public bool IsIgnoreCase { get; set; }
        public FrequencyAnalysis(string text)
        {
            Text = text;
        }
        public FrequencyAnalysis(string text, string remove)
        {
            Text = text;
            RemovePattern = remove;
        }
        public string Preprocessing(string replacement)
        {
            var text = Text;
            text = Regex.Replace(text, RemovePattern, replacement);
            if (IsIgnoreCase)
            {
                text = text.ToLower();
            }
            return text;
        }
        public Dictionary<string, int> CharacterFrequencyAnalysis
        {
            get
            {
                var text = Preprocessing(string.Empty);
                var dictionary = new Dictionary<string, int>();
                while (text.Length > 0)
                {
                    dictionary.Add(text[0].ToString(), text.Length - (text = text.Replace(text[0].ToString(), string.Empty)).Length);
                }
                return dictionary;
            }
        }

        public Dictionary<string, int> WordFrequencyAnalysis
        {
            get
            {
                var text = Preprocessing(" ");
                var dictionary = new Dictionary<string, int>();
                var list = text.Split(' ').ToList();
                while (list.Count > 0)
                {
                    var s = list[0];
                    dictionary.Add(s, list.Count(i => i == s));
                    list.RemoveAll(i => i == s);
                }
                return dictionary;
            }
        }

        public Dictionary<string, int> JapaneseFrequencyAnalysis
        {
            get
            {
                using (var mecab = MeCabIpaDicTagger.Create())
                {
                    var nodes = mecab.Parse(Text);
                    var dictionary = new Dictionary<string, int>();
                    foreach (var node in nodes)
                    {
                        if (dictionary.ContainsKey(node.Surface))
                        {
                            dictionary[node.Surface]++;
                        }
                        else
                        {
                            dictionary.Add(node.Surface, 1);
                        }
                    }
                    return dictionary;
                }
            }
        }
    }
}
