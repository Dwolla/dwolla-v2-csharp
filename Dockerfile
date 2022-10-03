FROM mcr.microsoft.com/dotnet/sdk:6.0

# Allow application key and secret to get passed in as build arguments.
# If you don't want to include your API key and secret with each build,
# you can also set their value statically below, meaning that their values
# won't need to be supplied when a new image is built in the future.
ARG app_key=YOUR_APP_KEY
ARG app_secret=YOUR_APP_SECRET

# Set environment variables for ExampleApp, based on the build arguments.
ENV DWOLLA_APP_KEY=$app_key
ENV DWOLLA_APP_SECRET=$app_secret

RUN mkdir app app/Dwolla.Client app/ExampleApp
ADD Dwolla.Client /app/Dwolla.Client
ADD ExampleApp /app/ExampleApp

WORKDIR /app/ExampleApp

CMD [ "dotnet", "run" ]
