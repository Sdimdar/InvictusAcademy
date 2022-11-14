# Quasar Test Front (package.json)

Test Front-end project on Vue Quasar

## Install the dependencies
```bash
yarn
# or
npm install
```

### Start the app in development mode (hot-code reloading, error reporting, etc.)
```bash
quasar dev
```


### Build the app for production
```bash
quasar build
```

### Customize the configuration
See [Configuring quasar.config.js](https://v2.quasar.dev/quasar-cli-webpack/quasar-config-js).


### Docker commands
```bash
docker build -t quasar:test .
docker run -d -p 8080:8080 --rm --name quasar quasar:test
```