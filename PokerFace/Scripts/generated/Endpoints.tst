${
    using Typewriter.Extensions.Types;

    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file =>
        {
            var filename = file.Name.Replace("Controller", string.Empty).Replace(".cs", ".ts");
            return $"endpoints/{filename}";
        };
    }

    string Import(Class @class)
    {
        var imports = new Dictionary<string, string>();
        Func<Type, string> statement = (Type type) => $"import {{ {InterfaceName(type)} }} from '../models/{FileName(type)}';";
        Action<Type> decider = (Type type) => {
            var key = type.ClassName();
            if (IsAppType(type) && imports.ContainsKey(key) == false)
            {
                var import = statement(type);
                imports.Add(key, import);
            }
        };

        foreach (var method in @class.Methods)
        {
            foreach (var parameter in method.Parameters)
            {
                decider(parameter.Type);
            }

            decider(method.Type);
        }

        return string.Join("\r\n", imports.Values);
    }

    string InterfaceName(Type type)
    {
        return $"I{type.ClassName()}";
    }

    bool IsAppType(Type type)
    {
        return type.FullName.Contains("PokerFace");
    }

    string FileName(Type type)
    {
        return type.ClassName().Replace("Controller", string.Empty);
    }

    string ClassName(Class @class)
    {
        return @class.Name.Replace("Controller", string.Empty);
    }

    string ReturnType(Type type)
    {
        if (type.FullName.Contains("PokerFace"))
        {
            return $"I{type.Name}";
        }
        else if (type.Name == "IActionResult")
        {
            return "void";
        }
        else if (type.Name.EndsWith("Result"))
        {
            return "string";
        }
        return type.Name;
    }

    string ReturnType(Method method)
    {
        return ReturnType(method.Type);
    }

    string ParamType(Parameter parameter)
    {
        return ReturnType(parameter.Type);
    }

    string ResourceName(Class @class)
    {
        var route = GetRoute(@class.Attributes);

        if (route == null)
        {
            return @class.name;
        }

        return route.Value;
    }

    Attribute GetRoute(AttributeCollection attributes)
    {
        var allowed = new [] { "HttpDelete", "HttpGet", "HttpPatch", "HttpPost", "HttpPut", "Route" };
        return attributes.FirstOrDefault(a => allowed.Contains(a.Name) && string.IsNullOrWhiteSpace(a.Value) == false);
    }

    string SetResourceUrl(Method method)
    {
        var route = GetRoute(method.Attributes);

        if (route != null)
        {
            var name = route.Value.Replace("{", "${");
            var result = $"const url = `{name}`";
            return result + ";";
        }
        return "const url = this.baseUrl;";
    }
}$Classes(PokerFace.Controllers.*)[$Import

/**
 * $ClassName
 */
export class $ClassName {
    private static readonly resourceName: string = '$ResourceName';

    private readonly baseUrl: URL;

    constructor(baseUrl: URL) {
        this.baseUrl = new URL($ClassName.resourceName, baseUrl.toString());
    }
    $Methods[
    /**
     *  $Name$Parameters[
     *  @param $Name {$Type}]
     *  @return {$ReturnType}
     */
    public $name($Parameters[$name: $ParamType][, ]): Promise<$ReturnType> {
        // $Attributes[$Name=$Value]
        $SetResourceUrl
        console.log('$name', url);
        return Promise.resolve(null);
    }
]
}]