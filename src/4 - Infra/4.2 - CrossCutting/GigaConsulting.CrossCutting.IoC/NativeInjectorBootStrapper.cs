using GigaConsulting.Application.Interfaces;
using GigaConsulting.Application.Services;
using GigaConsulting.Domain.CommandHandlers;
using GigaConsulting.Domain.Commands;
using GigaConsulting.Domain.Core.Events;
using GigaConsulting.Domain.Core.Interfaces;
using GigaConsulting.Domain.Core.Notifications;
using GigaConsulting.Domain.EventHandlers;
using GigaConsulting.Domain.Events;
using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Services;
using GigaConsulting.Infra.CrossCutting.Bus;
using GigaConsulting.Infra.CrossCutting.Identity.Authorization;
using GigaConsulting.Infra.CrossCutting.Identity.Models;
using GigaConsulting.Infra.CrossCutting.Identity.Services;
using GigaConsulting.Infra.Data.EventSourcing;
using GigaConsulting.Infra.Data.Repository;
using GigaConsulting.Infra.Data.Repository.EventSourcing;
using GigaConsulting.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GigaConsulting.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<IChairAppService, ChairAppService>();
            services.AddScoped<IAllocationAppService, AllocationAppService>();
            services.AddScoped<IRoomAppService, RoomAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<AllocationRegisteredEvent>, AllocationEventHandler>();
            services.AddScoped<INotificationHandler<ChairRegisteredEvent>, ChairEventHandler>();
            services.AddScoped<INotificationHandler<ChairRemovedEvent>, ChairEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewAllocationCommand, bool>, AllocationCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewChairCommand, bool>, ChairCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateChairCommand, bool>, ChairCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveChairCommand, bool>, ChairCommandHandler>();

            // Domain - 3rd parties
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            // Infra - Data
            services.AddScoped<IAllocationRepository, AllocationRepository>();
            services.AddScoped<IChairRepository, ChairRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
}
