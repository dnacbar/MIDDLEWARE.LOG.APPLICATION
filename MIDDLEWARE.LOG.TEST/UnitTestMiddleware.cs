using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MIDDLEWARE.LOG.TEST
{
    public class UnitTestMiddleware
    {
        [Fact]
        public async Task TestFatalMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(x => { })
                    .Configure(app =>
                    {
                        app.UseFatalExceptionMiddleware();
                    });
            })
            .StartAsync();

            var response = host.GetTestClient().GetAsync("/");
        }

        [Fact]
        public async Task TestDataBaseMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(x => { })
                    .Configure(app =>
                    {
                        app.UseDataBaseExceptionMiddleware();
                    });
            })
            .StartAsync();

            var response = host.GetTestClient().GetAsync("/");
        }

        [Fact]
        public async Task TestBadGatewayMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(x => { })
                    .Configure(app =>
                    {
                        app.UseBadGatewayExceptionMiddleware();
                    });
            })
            .StartAsync();

            var response = host.GetTestClient().GetAsync("/");
        }

        [Fact]
        public async Task TestValidationMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(x => { })
                    .Configure(app =>
                    {
                        app.UseValidationExceptionMiddleware();
                    });
            })
            .StartAsync();

            var response = host.GetTestClient().GetAsync("/");
        }

        [Fact]
        public async Task TestNotFoundMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(x => { })
                    .Configure(app =>
                    {
                        app.UseNotFoundExceptionMiddleware();
                    });
            })
            .StartAsync();

            var response = host.GetTestClient().GetAsync("/");
        }
    } 
}