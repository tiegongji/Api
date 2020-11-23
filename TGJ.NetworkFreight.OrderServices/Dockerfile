#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TGJ.NetworkFreight.SeckillAggregateServices/TGJ.NetworkFreight.SeckillAggregateServices.csproj", "TGJ.NetworkFreight.SeckillAggregateServices/"]
COPY ["TGJ.NetworkFreight.Cores/TGJ.NetworkFreight.Cores.csproj", "TGJ.NetworkFreight.Cores/"]
COPY ["TGJ.NetworkFreight.Commons/TGJ.NetworkFreight.Commons.csproj", "TGJ.NetworkFreight.Commons/"]
COPY ["TGJ.NetworkFreight.OrderServices/TGJ.NetworkFreight.OrderServices.csproj", "TGJ.NetworkFreight.OrderServices/"]
COPY ["TGJ.NetworkFreight.UserServices/TGJ.NetworkFreight.UserServices.csproj", "TGJ.NetworkFreight.UserServices/"]
RUN dotnet restore "TGJ.NetworkFreight.SeckillAggregateServices/TGJ.NetworkFreight.SeckillAggregateServices.csproj"
COPY . .
WORKDIR "/src/TGJ.NetworkFreight.SeckillAggregateServices"
RUN dotnet build "TGJ.NetworkFreight.SeckillAggregateServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TGJ.NetworkFreight.SeckillAggregateServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TGJ.NetworkFreight.OrderServices.dll"]