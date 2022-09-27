FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set environment variables for ExampleApp
ENV DWOLLA_APP_KEY=YOUR_APP_KEY
ENV DWOLLA_APP_SECRET=YOUR_APP_SECRET

RUN mkdir app && mkdir app/Dwolla.Client && mkdir app/ExampleApp
ADD Dwolla.Client /app/Dwolla.Client
ADD ExampleApp /app/ExampleApp

WORKDIR /app/ExampleApp

CMD [ "dotnet", "run" ]
