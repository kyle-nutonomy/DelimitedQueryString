### Example usage

Look at `example/`

### Publish the package

1. `dotnet pack`
2. `dotnet nuget push src/bin/Debug/DelimitedQueryString.1.0.1.nupkg -k <apikey> --source https://www.nuget.org/api/v2/package`