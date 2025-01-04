using static Ecs.CSharp.Benchmark.Contexts.FrentBaseContext;
using Ecs.CSharp.Benchmark.Contexts;
using BenchmarkDotNet.Attributes;
using Frent;

namespace Ecs.CSharp.Benchmark
{
    public partial class CreateEntityWithTwoComponents
    {
        [Context]
        private readonly FrentBaseContext _frent;

        [BenchmarkCategory(Categories.Frent)]
        [Benchmark]
        public void Frent()
        {
            World world = _frent.World;
            world.EnsureCapacity<Component1, Component2>(EntityCount);

            for (int i = 0; i < EntityCount; i++)
                world.Create<Component1, Component2>(default, default);
        }
    }
}
