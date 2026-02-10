# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Dependendo do sistema operacional dos computadores host que compilarão ou executarão os contêineres, a imagem especificada na instrução FROM pode precisar ser alterada.
# Para obter mais informações, consulte https://aka.ms/containercompat.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-nanoserver-ltsc2022 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0-nanoserver-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TMB_REST/TMB_REST.csproj", "TMB_REST/"]
RUN dotnet restore "./TMB_REST/TMB_REST.csproj"
COPY . .
WORKDIR "/src/TMB_REST"
RUN dotnet build "./TMB_REST.csproj" -c %BUILD_CONFIGURATION% -o /app/build


# Multi-stage build para backend .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copie csproj e restaure
COPY ["TMB_REST.csproj", "./"]
RUN dotnet restore "./TMB_REST.csproj"

# Copie toda a app e publique
COPY . .
RUN dotnet publish "TMB_REST.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "TMB_REST.dll"]