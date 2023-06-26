﻿using System;
using Scellecs.Morpeh;

namespace Ecs.CSharp.Benchmark.Context
{
    internal class MorpehBaseContext : IDisposable
    {
        public struct Component1 : IComponent
        {
            public int Value;
        }

        public struct Component2 : IComponent
        {
            public int Value;
        }

        public struct Component3 : IComponent
        {
            public int Value;
        }

        public World World { get; }

        protected MorpehBaseContext()
        {
            World = World.Create();
        }

        public virtual void Dispose()
        {
            World.Dispose();
        }
    }
}