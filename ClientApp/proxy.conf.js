const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:10426';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    // target: target, // When FE is started from backend
    target: "https://localhost:7275", // When FE and BE is run separately
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

module.exports = PROXY_CONFIG;
