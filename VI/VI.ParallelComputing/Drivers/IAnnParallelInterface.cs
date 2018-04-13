namespace VI.ParallelComputing.Drivers
{
    public interface IGpuInterface
    {
        ParalleExecutorlInterface Executor { get; }
    }
}