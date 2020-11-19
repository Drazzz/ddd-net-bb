namespace DDDNETBB.Core.Abstractions
{
    public interface IMetadataProvider<out TMetadata>
    {
        TMetadata Metadata{get;}
    }
}