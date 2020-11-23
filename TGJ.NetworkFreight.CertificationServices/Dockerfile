#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TGJ.NetworkFreight.CertificationServices/TGJ.NetworkFreight.CertificationServices.csproj", "TGJ.NetworkFreight.CertificationServices/"]
COPY ["TGJ.NetworkFreight.Cores/TGJ.NetworkFreight.Cores.csproj", "TGJ.NetworkFreight.Cores/"]
COPY ["TGJ.NetworkFreight.Commons/TGJ.NetworkFreight.Commons.csproj", "TGJ.NetworkFreight.Commons/"]
RUN dotnet restore "TGJ.NetworkFreight.CertificationServices/TGJ.NetworkFreight.CertificationServices.csproj"
COPY . .
WORKDIR "/src/TGJ.NetworkFreight.CertificationServices"
RUN dotnet build "TGJ.NetworkFreight.CertificationServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TGJ.NetworkFreight.CertificationServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TGJ.NetworkFreight.CertificationServices.dll"]