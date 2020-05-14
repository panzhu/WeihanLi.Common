﻿using System;
using WeihanLi.Common.Helpers;

namespace WeihanLi.Common.Aspect.Castle
{
    internal sealed class CastleProxyFactory : IProxyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public static readonly IProxyFactory Instance = new CastleProxyFactory(DependencyResolver.Current);

        public CastleProxyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CreateProxy(Type serviceType)
        {
            if (null == serviceType)
                throw new ArgumentNullException(nameof(serviceType));

            if (serviceType.IsInterface)
            {
                return CastleHelper.ProxyGenerator.CreateInterfaceProxyWithoutTarget(serviceType,
                        new FluentAspectInterceptor());
            }

            return CastleHelper.ProxyGenerator.CreateClassProxy(serviceType, new FluentAspectInterceptor());
        }

        public object CreateProxy(Type serviceType, Type implementType)
        {
            if (null == serviceType)
                throw new ArgumentNullException(nameof(serviceType));

            if (null == implementType)
                throw new ArgumentNullException(nameof(implementType));

            var target = _serviceProvider.CreateInstance(implementType);
            return CreateProxyWithTarget(serviceType, target);
        }

        public object CreateProxyWithTarget(Type serviceType, object implement)
        {
            if (null == serviceType)
                throw new ArgumentNullException(nameof(serviceType));

            if (null == implement)
                throw new ArgumentNullException(nameof(implement));

            if (serviceType.IsInterface)
            {
                return CastleHelper.ProxyGenerator.CreateInterfaceProxyWithTarget(serviceType, implement,
                    new FluentAspectInterceptor());
            }
            return CastleHelper.ProxyGenerator.CreateClassProxyWithTarget(serviceType, implement,
                new FluentAspectInterceptor());
        }
    }
}