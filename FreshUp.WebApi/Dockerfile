FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY ./FreshUp.WebApi/FreshUp.WebApi.csproj ./FreshUp.WebApi/
RUN dotnet restore ./FreshUp.WebApi/FreshUp.WebApi.csproj
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 9050
ENV ASPNETCORE_URLS=http://+:9050
ENTRYPOINT ["dotnet", "FreshUp.WebApi.dll"]