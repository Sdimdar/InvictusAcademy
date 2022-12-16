# base image
FROM node AS build

# set working directory
WORKDIR /app

# set env variables
ARG GATEWAY
ENV GATEWAY $GATEWAY
ARG CLOUD_STORAGE_URL
ENV CLOUD_STORAGE_URL $CLOUD_STORAGE_URL

# install and cache app dependencies
COPY package.json .
RUN npm install 

# add app
COPY . .

# generate build
RUN GATEWAY=$GATEWAY CLOUD_STORAGE_URL=$CLOUD_STORAGE_URL npx quasar build 

############
### prod ###
############

# base image
FROM nginx:alpine

# remove default NGINX site
RUN rm -rf /usr/share/nginx/html/*

# copy artifact build from the 'build environment'
COPY --from=build /app/dist/spa /usr/share/nginx/html

# Add nginx config
COPY --from=build /app/nginx.conf /etc/nginx/nginx.conf

# expose port 80
EXPOSE 80

# run nginx
CMD ["nginx", "-g", "daemon off;"]

# docker build -t invictus-user-front .
# docker run --rm -d -p 80:80 --name invictus-user-front invictus-user-front