﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WcfClientProxyGenerator.Tests.Infrastructure;

namespace WcfClientProxyGenerator.Tests
{
    public class Test
    {
        [Test]
        public void TestIt()
        {
            var mockService = new Mock<ITestService>();
            mockService.Setup(m => m.TestMethod("good")).Returns("OK");
            mockService.Setup(m => m.TestMethod("bad")).Throws<Exception>();

            var serviceHost = InProcTestFactory.CreateHost<ITestService>(new TestServiceImpl(mockService));

            var generator = new Generator<ITestService>();
            var proxy = generator.Generate();

            var result = proxy.TestMethod("arg");
        }

        [Test]
        public void SanityCheck_Fault_Happens_With_Default_Channel_Proxy()
        {
            var mockService = new Mock<ITestService>();
            mockService.Setup(m => m.TestMethod("good")).Returns("OK");
            mockService.Setup(m => m.TestMethod("bad")).Throws<Exception>();

            var serviceHost = InProcTestFactory.CreateHost<ITestService>(new TestServiceImpl(mockService));

            var proxy = new ChannelFactory<ITestService>(serviceHost.Binding, serviceHost.EndpointAddress).CreateChannel();

            // Will fault the channel
            Assert.That(() => proxy.TestMethod("bad"), Throws.Exception);
            Assert.That(() => proxy.TestMethod("good"), Throws.Exception.TypeOf<CommunicationObjectFaultedException>());
        }

        [Test]
        public void Test2()
        {
            var mockService = new Mock<ITestService>();
            mockService.Setup(m => m.TestMethod("good")).Returns("OK");
            mockService.Setup(m => m.TestMethod("bad")).Throws<Exception>();

            var serviceHost = InProcTestFactory.CreateHost<ITestService>(new TestServiceImpl(mockService));

            var proxy = new ChannelFactory<ITestService>(serviceHost.Binding, serviceHost.EndpointAddress).CreateChannel();

            // Will fault the channel
            Assert.That(() => proxy.TestMethod("bad"), Throws.Exception);
            Assert.That(() => proxy.TestMethod("good"), Throws.Exception.TypeOf<CommunicationObjectFaultedException>());
        }
    }
}