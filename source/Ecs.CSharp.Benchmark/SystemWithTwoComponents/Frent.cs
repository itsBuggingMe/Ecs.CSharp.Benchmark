using static Ecs.CSharp.Benchmark.Contexts.FrentBaseContext;
using System.Runtime.CompilerServices;
using Ecs.CSharp.Benchmark.Contexts;
using BenchmarkDotNet.Attributes;
using System;
using Frent;

namespace Ecs.CSharp.Benchmark
{
    public partial class SystemWithTwoComponents
    {
        [Context] 
        private readonly FrentContext _frent;

        private sealed class FrentContext : FrentBaseContext
        {
            public FrentContext(int entityCount, int _)
            {
                for (int i = 0; i < entityCount; i++)
                {
                    World.Create<Component1, Component2>(default, new() { Value = 1 });
                }
            }

            internal struct InlineQuery : IQuery<Component1, Component2>
            {
                public void Run(ref Component1 arg1, ref Component2 arg2) => arg1.Value += arg2.Value;
            }
        }

        [BenchmarkCategory(Categories.Frent)]
        [Benchmark]
        public void Frent_Query()
        {
            _frent.World.Query((ref Component1 arg1, ref Component2 arg2) => arg1.Value += arg2.Value);
        }

        [BenchmarkCategory(Categories.Frent)]
        [Benchmark]
        public void Frent_InlineQuery()
        {
            _frent.World.InlineQuery<FrentContext.InlineQuery, Component1, Component2>(default);
        }
    }
}
