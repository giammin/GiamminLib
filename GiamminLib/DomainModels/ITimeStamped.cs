using System;

namespace GiamminLib.DomainModels
{
    public interface ICreatedAndChangedStampEntity: ICreatedStampEntity, IChangedTimeStamped
    {
        int? ChangedById { get; set; }
    }
    public interface ICreatedStampEntity : ICreatedTimeStamped
    {
        int CreatedById { get; set; }
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