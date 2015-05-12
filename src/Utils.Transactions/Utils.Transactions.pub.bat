:: nuget spec
set /p version=Version number:
nuget pack Utils.Transactions.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Utils.Transactions.%version%.nupkg
pause;