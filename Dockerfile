# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia todos os arquivos do projeto para dentro do container
COPY . .

# Restaura as dependências do projeto
RUN dotnet restore

# Compila e publica a aplicação em modo Release para a pasta /out
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copia os arquivos compilados da etapa de build para o runtime
COPY --from=build /app/out .

# Define a URL para escutar conexões HTTP na porta 5000
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "TelemetriaMonitoramento.API.dll"]
