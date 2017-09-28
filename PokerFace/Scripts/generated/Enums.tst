${
    using Typewriter.Extensions.Types;

    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file => {
            return $"enums/{file.Name.Replace(".cs", ".d.ts")}";
        };
    }
}$Enums(PokerFace.Models.Poker.*)[export enum $Name {$Values[
    // $Name
    $Name = $Value,
]}]