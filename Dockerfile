ARG BUILDKIT_CONTEXT_KEEP_GIT_DIR=true

# FROM mcr.microsoft.com/dotnet/sdk:7.0@sha256:d32bd65cf5843f413e81f5d917057c82da99737cb1637e905a1a4bc2e7ec6c8d as build-env
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY . ./
RUN ls
RUN git submodule init
RUN git submodule update
RUN cd War3Net
RUN git submodule init
RUN git submodule update
RUN cd ..
RUN dotnet publish ./W3X-Builder/W3X-Builder.csproj -c Release -o out --no-self-contained

LABEL repository="https://github.com/TheLazzoro/war3-map-build-action"
LABEL homepage="https://github.com/TheLazzoro/war3-map-build-action"

# Label as GitHub action
LABEL com.github.actions.name="Build Warcraft III Map"
# Limit to 160 characters
LABEL com.github.actions.description="The description of your GitHub Action."
# See branding:
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="activity"
LABEL com.github.actions.color="orange"

FROM mcr.microsoft.com/dotnet/sdk:8.0
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/W3X-Builder.dll" ]