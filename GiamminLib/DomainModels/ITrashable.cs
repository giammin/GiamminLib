using System;

namespace GiamminLib.DomainModels
{
    public interface ITrashable
    {
#if NET5_0_OR_GREATER
        bool IsDeleted => DeletedDate.HasValue;
#endif
        DateTime? DeletedDate { get; set; }
    }
}