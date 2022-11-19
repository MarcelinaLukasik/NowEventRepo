const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/budget",
];

const context2 = [
    "/main",
];

const context3 = [
    "/events",
];

const context4 = [
    "/email",
];

const context5 = [
    "/location",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7267',
        secure: false
    });

    app.use(appProxy);

    const appProxy2 = createProxyMiddleware(context2, {
        target: 'https://localhost:7267',
        secure: false
    });

    app.use(appProxy2);

    const appProxy3 = createProxyMiddleware(context3, {
        target: 'https://localhost:7267',
        secure: false
    });

    app.use(appProxy3);

    const appProxy4 = createProxyMiddleware(context4, {
        target: 'https://localhost:7267',
        secure: false
    });

    app.use(appProxy4);

    const appProxy5 = createProxyMiddleware(context5, {
        target: 'https://localhost:7267',
        secure: false
    });

    app.use(appProxy5);
};
