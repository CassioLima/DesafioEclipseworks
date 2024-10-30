# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
#COPY *.csproj ./
#RUN dotnet restore

COPY DESAFIO.sln .
COPY ./Api2/*.csproj Api2/
COPY ./Application/*.csproj Application/
COPY ./Domain/*.csproj Domain/
COPY ./Infra/*.csproj Infra/
COPY ./Shared/*.csproj Shared/
COPY ./EW.Desafio.WebApi.Tests/*.csproj EW.Desafio.WebApi.Tests/

RUN dotnet restore
# Copia o restante do código e compila a aplicação
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Api.dll"]
