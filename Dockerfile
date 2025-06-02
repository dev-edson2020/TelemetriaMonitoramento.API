# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo código e publicar
COPY . ./
RUN dotnet publish TelemetriaMonitoramento.API.csproj -c Release -o out

# Etapa 2: Imagem final runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expor porta padrão da sua aplicação (ajuste se for outra)
EXPOSE 5000

# Comando para rodar a aplicação
ENTRYPOINT ["TelemetriaMonitoramento.API.dll"]
