FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./*sln ./
COPY ./WebApi/*.csproj ./WebApi/
COPY ./EFRandevouDAL/*.csproj ./EFRandevouDAL/
COPY ./BusinessServices/*.csproj ./BusinessServices/
COPY ./RandevouData/*.csproj ./RandevouData/
COPY ./BusinessServices.Tests/*.csproj ./BusinessServices.Tests/

copy ./Randevou.db ./out/Randevou.db

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .



ENTRYPOINT ["dotnet", "WebApi.dll"]

