${
    using Typewriter.Extensions.Types;

    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file => {
            return $"models/{file.Name.Replace(".cs", ".d.ts")}";
        };
    }

    string ClassName(Property property)
    {
        return property.Type.ClassName();
    }

    string InterfaceClassName(Property property)
    {
        return $"I{property.Type.ClassName()}";
    }

    string InterfaceName(Property property)
    {
        if (property.Type.FullName.Contains("PokerFace"))
        {
            return $"I{property.Type.Name}";
        }
        else if (property.Type.Name == "Uri") {
            return "string";
        }
        return property.Type.Name;
    }

    string FullTypeName(Property property)
    {
        return property.FullName;
    }

    string Import(Property property)
    {
        if (property.Type.FullName.Contains("PokerFace"))
        {
            return $"import {{ {InterfaceClassName(property)} }} from './{ClassName(property)}';" + "\r\n";
        }
        return null;
    }

    string PropertyName(Property property)
    {
        if (property.Attributes.Any(a => a.Name == "Required" || a.Name == "CanBeNull") == false)
        {
            return $"{property.name}?";
        }
        return property.name;
    }
}$Classes(PokerFace.Models.Poker.*)[$Properties[$Import]
/**
 *  @external $Namespace.$Name
 */
export interface I$Name {$Properties[
    /**
     *  $Name
     *  @return {$Type}
     *  @external $FullName
     */
    $PropertyName: $InterfaceName;
]}]