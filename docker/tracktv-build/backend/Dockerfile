FROM microsoft/dotnet:2.1-sdk-alpine3.7

COPY ./src /workspace

WORKDIR /workspace/TrackTv.WebServices

RUN dotnet restore
RUN dotnet publish -c Release -r alpine.3.7-x64 -o /build

FROM alpine:3.7

RUN apk --no-cache add libstdc++
RUN apk --no-cache add libintl
RUN apk --no-cache add icu-libs
RUN apk --no-cache add openssl

COPY --from=0 /build /app/backend
 
EXPOSE 5000

WORKDIR /app/backend

ENTRYPOINT [ "./tracktv-web" ]
