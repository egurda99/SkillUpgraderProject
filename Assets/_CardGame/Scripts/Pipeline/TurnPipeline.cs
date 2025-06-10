using System.Threading.Tasks;

namespace _CardGame.Pipeline
{
    public sealed class TurnPipeline : Pipeline
    {
        private readonly VisualPipeline _visualPipeline;

        public TurnPipeline(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }

        public override async Task Run()
        {
            await base.Run();
            await _visualPipeline.Run();
            await Run();
        }
    }
}
