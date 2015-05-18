:: nuget spec
set /p version=Version number:
nuget pack Transactions.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Transactions.%version%.nupkg
pause;