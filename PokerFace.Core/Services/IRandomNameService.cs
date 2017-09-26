namespace PokerFace.Core.Services
{
    public interface IRandomNameService
    {
        string GetRandomName();

        string GetRandomNameDashed(string name = default(string));

        string GetRandomNameUnderscored(string name = default(string));
    }
}