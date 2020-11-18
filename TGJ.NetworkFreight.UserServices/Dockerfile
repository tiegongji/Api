#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TGJ.NetworkFreight.UserServices/TGJ.NetworkFreight.UserServices.csproj", "TGJ.NetworkFreight.UserServices/"]
COPY ["TGJ.NetworkFreight.Cores/TGJ.NetworkFreight.Cores.csproj", "TGJ.NetworkFreight.Cores/"]
COPY ["TGJ.NetworkFreight.Commons/TGJ.NetworkFreight.Commons.csproj", "TGJ.NetworkFreight.Commons/"]
RUN dotnet restore "TGJ.NetworkFreight.UserServices/TGJ.NetworkFreight.UserServices.csproj"
COPY . .
WORKDIR "/src/TGJ.NetworkFreight.UserServices"
RUN dotnet build "TGJ.NetworkFreight.UserServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TGJ.NetworkFreight.UserServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TGJ.NetworkFreight.UserServices.dll"]