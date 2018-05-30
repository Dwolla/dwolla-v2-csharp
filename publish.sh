find . -type d '(' -name bin -o -name obj ')' -exec rm -rf "{}" \;
dotnet restore
dotnet clean
dotnet build --configuration Release
find . -name *.Tests.csproj -exec dotnet test "{}" --configuration Release --no-build \;
dotnet pack Dwolla.Client/Dwolla.Client.csproj --configuration Release --no-build --verbosity normal
dotnet nuget push Dwolla.Client/bin/Release/*.nupkg --source https://www.nuget.org/api/v2/package --api-key $NUGET_ORG_API_KEY
