FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

COPY . .

RUN dotnet restore "./Template restaurant-app.csproj"

EXPOSE 8080

CMD ["dotnet", "watch", "run", "--project", "./Template restaurant-app.csproj", "--no-launch-profile", "--urls", "http://+:8080"]