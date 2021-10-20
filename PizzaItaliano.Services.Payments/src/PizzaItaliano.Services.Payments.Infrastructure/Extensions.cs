﻿using Convey;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PizzaItaliano.Services.Payments.Application.Events.External;
using PizzaItaliano.Services.Payments.Application.Services;
using PizzaItaliano.Services.Payments.Application.Services.Clients;
using PizzaItaliano.Services.Payments.Core.Repositories;
using PizzaItaliano.Services.Payments.Infrastructure.Exceptions;
using PizzaItaliano.Services.Payments.Infrastructure.Mongo.Documents;
using PizzaItaliano.Services.Payments.Infrastructure.Repositories;
using PizzaItaliano.Services.Payments.Infrastructure.Services;
using PizzaItaliano.Services.Payments.Infrastructure.Services.Clients;
using System;

namespace PizzaItaliano.Services.Payments.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder conveyBuilder)
        {
            conveyBuilder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
            conveyBuilder.Services.AddTransient<IMessageBroker, MessageBroker>();
            conveyBuilder.Services.AddSingleton<IEventMapper, EventMapper>();
            conveyBuilder.Services.AddTransient<IOrderServiceClient, OrderServiceClient>();

            conveyBuilder.AddErrorHandler<ExceptionToResponseMapper>();
            conveyBuilder.AddExceptionToMessageMapper<ExceptionToMessageMapper>();
            conveyBuilder.AddQueryHandlers();
            conveyBuilder.AddInMemoryQueryDispatcher();
            conveyBuilder.AddHttpClient();
            conveyBuilder.AddMongo();
            conveyBuilder.AddMongoRepository<PaymentDocument, Guid>("payments");
            conveyBuilder.AddSwaggerDocs();
            conveyBuilder.AddWebApiSwaggerDocs();
            conveyBuilder.AddRabbitMq();

            return conveyBuilder;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
               .UseConvey()
               .UseSwaggerDocs()
               .UseRabbitMq()
               .SubscribeEvent<OrderStateModified>(); // wywola event handler;

            return app;
        }
    }
}
