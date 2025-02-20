FROM node:alpine AS builder

WORKDIR /usr/local/app
COPY ./ /usr/local/app
RUN yarn install
RUN yarn run build

FROM nginx:alpine
COPY --from=builder /usr/local/app/dist/client/browser /usr/share/nginx/html
COPY /nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
