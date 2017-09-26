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
            return (name ?? this.GetRandomName()).Dasherize();
        }

        public string GetRandomNameUnderscored(string name = default(string))
        {
            return (name ?? this.GetRandomName()).Underscore();
        }

        private static string GetRandomString(IReadOnlyList<string> values)
        {
            var random = new Random();
            var index = random.Next(0, values.Count - 1);
            return values[index];
        }
    }
}