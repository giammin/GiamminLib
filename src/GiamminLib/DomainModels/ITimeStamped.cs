using System;

namespace GiamminLib.DomainModels
{
    public interface ICreatedAndChangedStampEntity<T> : ICreatedStampEntity<T>, IChangedTimeStamped
    {
        T? ChangedById { get; set; }
    }
    public interface ICreatedStampEntity<T> : ICreatedTimeStamped
    {
        T CreatedById { get; set; }
    }
    public interface ICreatedTimeStamped
    {
        DateTime CreatedDate { get; set; }
    }
    public interface IChangedTimeStamped
    {
        DateTime? ChangedDate { get; set; }
    }
}