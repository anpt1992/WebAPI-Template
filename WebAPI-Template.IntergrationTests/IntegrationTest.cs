﻿using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI_Template.Data;
using System.Net.Http.Json;
using WebAPI_Template.Contracts.V1;
using WebAPI_Template.Contracts.V1.Requests;
using WebAPI_Template.Contracts.V1.Responses;
using System.Linq;
using System;

namespace WebAPI_Template.IntergrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });

                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<DataContext>();

                            db.Database.EnsureCreated();
                        }
                    });
                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<TestResponse> CreateTestAsync(CreateTestRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Tests.Create, request);
            return await response.Content.ReadAsAsync<TestResponse>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "anpt1992@gmail.com",
                Password = "P@ssword123"
            });
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }
    }
}
