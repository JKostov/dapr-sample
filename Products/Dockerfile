FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN mkdir /app
WORKDIR /app

COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN mkdir /app
WORKDIR /app
COPY --from=build /app/out .
CMD ["dotnet", "Products.dll"]
