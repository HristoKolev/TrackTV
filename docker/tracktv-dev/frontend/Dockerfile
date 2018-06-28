FROM node:alpine

RUN apk --no-cache add nginx
RUN apk --no-cache add supervisor

#RUN touch /var/run/supervisor.sock && chmod 0770 /var/run/supervisor.sock

COPY ./inject-settings.js /inject-settings.js
COPY ./supervisord.conf /etc/supervisord.conf

ENTRYPOINT ["supervisord"]
