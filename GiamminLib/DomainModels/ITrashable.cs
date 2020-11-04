using System;

namespace GiamminLib.DomainModels
{
    public interface ITrashable
    {
#if NETSTANDARD2_1
        bool IsDeleted => DeletedDate.HasValue;
#endif
        DateTime? DeletedDate { get; set; }
    }
}