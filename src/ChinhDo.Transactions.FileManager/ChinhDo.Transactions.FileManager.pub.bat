:: nuget spec
set /p version=Version number:
nuget pack ChinhDo.Transactions.FileManager.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push ChinhDo.Transactions.FileManager.%version%.nupkg
pause;