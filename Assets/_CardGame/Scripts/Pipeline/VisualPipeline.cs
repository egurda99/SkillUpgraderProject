using System.Threading.Tasks;

namespace _CardGame.Pipeline
{
    public sealed class VisualPipeline: Pipeline
    {
        public override async Task Run()
        {
            await base.Run();
            Clear();
        }
    }
}