using static Ecs.CSharp.Benchmark.Contexts.FrentBaseContext;
using System.Runtime.CompilerServices;
using Ecs.CSharp.Benchmark.Contexts;
using BenchmarkDotNet.Attributes;
using System;
using Frent;

namespace Ecs.CSharp.Benchmark
{
    public partial class SystemWithOneComponent
    {
        [Context] 
        private readonly FrentContext _frent;

        private sealed class FrentContext : FrentBaseContext
        {
            //ignore entity padding since Frent is archetype based
            public FrentContext(int entityCount, int _)
            {
                for (int i = 0; i < entityCount; i++)
                {
                    World.Create<Component1>(default);
                }
            }

            internal struct InlineQuery : IQuery<Component1>
            {
                public void Run(ref Component1 arg) => arg.Value++;
            }
        }

        [BenchmarkCategory(Categories.Frent)]
        [Benchmark]
        public void Frent_Query()
        {
            _frent.World.Query((ref Component1 comp) => comp.Value++);
        }

        [BenchmarkCategory(Categories.Frent)]
        [Benchmark]
        public void Frent_InlineQuery()
        {
            _frent.World.InlineQuery<FrentContext.InlineQuery, Component1>(default);
        }
    }
}
