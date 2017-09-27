namespace PokerFace.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Humanizer;

    public partial class RandomNameService : IRandomNameService
    {
        public string GetRandomName()
        {
            var adjective = RandomNameService.GetRandomString(RandomNameService.Adjectives);
            var noun = RandomNameService.GetRandomString(RandomNameService.Nouns);

            return adjective + " " + noun;
        }

        public string GetRandomNameDashed(string name = default(string))
        {
            var original = name ?? this.GetRandomName();
            return original.Underscore().Dasherize();
        }

        public string GetRandomNameUnderscored(string name = default(string))
        {
            var original = name ?? this.GetRandomName();
            return original.Underscore();
        }

        private static string GetRandomString(IReadOnlyList<string> values)
        {
            var random = new Random();
            var index = random.Next(0, values.Count - 1);
            return values[index];
        }
    }
}