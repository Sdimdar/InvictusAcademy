# base image
FROM node AS build

# set working directory
WORKDIR /app

# install and cache app dependencies
COPY package.json /app/package.json
RUN npm install

# add app
COPY . .

# generate build
RUN npx quasar build

############
### prod ###
############

# base image
FROM nginx:alpine

# remove default NGINX site
RUN rm -rf /usr/share/nginx/html/*

# copy artifact build from the 'build environment'
COPY --from=build /app/dist/spa /usr/share/nginx/html

# expose port 80
EXPOSE 80

# run nginx
CMD ["nginx", "-g", "daemon off;"]

# docker build -t invictus-user-front .
# docker run --rm -d -p 80:80 --name invictus-user-front invictus-user-front