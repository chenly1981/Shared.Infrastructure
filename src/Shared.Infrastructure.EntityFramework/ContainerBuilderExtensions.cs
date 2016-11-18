﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EntityFramework.UnitOfWork;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddEntityFramework<TContext>(this ContainerBuilder builder, string alias, Action<DbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            string creatorName = UnitOfWorkHelper.GetUnitOfWorkName(alias);
            builder.RegisterGeneric(typeof(RepositoryBase<>));
            builder.RegisterType<EntityFrameworkUnitOfWork<TContext>>();
            builder.RegisterType<EntityFrameworkUnitOfWorkCreator<TContext>>()
                .Named<IUnitOfWorkCreator>(creatorName)
                .SingleInstance();

            return builder;
        }
    }
}