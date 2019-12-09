using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using MediatR;
using SC.DevChallenge.Queries.Abstractions;

namespace SC.DevChallenge.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public class QueriesModule : Autofac.Module
    {
        protected override Assembly ThisAssembly => typeof(QueryHandlerBase<,>).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(this.ThisAssembly)
                   .AsClosedTypesOf(typeof(ISpecification<,>))
                   .AsImplementedInterfaces();
        }
    }
}
