:: nuget spec
set /p version=Version number:
nuget pack Utils.Transactions.IO.Files.Local.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Utils.Transactions.IO.Files.Local.%version%.nupkg
pause;