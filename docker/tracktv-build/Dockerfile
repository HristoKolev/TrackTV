FROM microsoft/dotnet:2.1-sdk-alpine3.7

COPY ./backend-src /workspace

WORKDIR /workspace/TrackTv.WebServices

RUN dotnet restore
RUN dotnet publish -c Release -r alpine.3.7-x64 -o /build

FROM node:alpine

COPY ./frontend-src /workspace

WORKDIR /workspace

RUN npm i

RUN npm run build

RUN cp ./dist /build -R

FROM alpine:3.7

RUN apk --no-cache add libstdc++
RUN apk --no-cache add libintl
RUN apk --no-cache add icu-libs
RUN apk --no-cache add openssl

RUN apk --no-cache add nginx
RUN apk --no-cache add supervisor

RUN mkdir -p /run/nginx

COPY ./supervisord.conf /etc/supervisord.conf
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=0 /build /app/backend
COPY --from=1 /build /app/frontend
COPY ./inject-json /app/inject-json
 
VOLUME [ "/data" ]

EXPOSE 80

ENTRYPOINT ["supervisord"]
