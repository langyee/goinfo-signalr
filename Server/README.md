### In the base diretory, run the following command to start development server

```
dotnet watch run -p signalr.csproj
```

### Docker container

```
docker-compose -f deploy/docker-compose.yml build
docker-compose -f deploy/docker-compose.yml stop
docker-compose -f deploy/docker-compose.yml up -d
```