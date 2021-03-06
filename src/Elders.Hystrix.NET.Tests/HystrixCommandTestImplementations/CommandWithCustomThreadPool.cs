﻿namespace Elders.Hystrix.NET.Test.HystrixCommandTestImplementations
{
    using System;
    using System.Threading;
    using Elders.Hystrix.NET.Test.CircuitBreakerTestImplementations;
    using Elders.Hystrix.NET;
    using Elders.Hystrix.NET.ThreadPool;

    /// <summary>
    /// Command that receives a custom thread-pool, sleepTime, timeout
    /// </summary>
    internal class CommandWithCustomThreadPool : TestHystrixCommand<bool>
    {
        public bool DidExecute { get; private set; }

        private readonly TimeSpan sleepTime;

        public CommandWithCustomThreadPool(TestCircuitBreaker circuitBreaker, IHystrixThreadPool threadPool, TimeSpan sleepTime, HystrixCommandPropertiesSetter properties)
            : base(new TestCommandBuilder()
            {
                ThreadPool = threadPool,
                CircuitBreaker = circuitBreaker,
                Metrics = circuitBreaker.Metrics,
                CommandPropertiesDefaults = properties,
            })
        {
            this.sleepTime = sleepTime;
        }

        protected override bool Run()
        {
            DidExecute = true;
            try
            {
                Thread.Sleep(this.sleepTime);
            }
            catch (ThreadInterruptedException)
            {
            }
            return true;
        }
    }
}
