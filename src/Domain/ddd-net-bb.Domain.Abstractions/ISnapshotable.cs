namespace DDDNETBB.Domain.Abstractions
{
    public interface ISnapshotable
    {
        (object snapshot, int snapshotVersion) TakeSnapshot();
        void ApplySnapshot(object snapshot, int snapshotVersion);

        int SnapshotVersion { get; }
        int? SnapshotVersionFrequency { get; }
    }
}