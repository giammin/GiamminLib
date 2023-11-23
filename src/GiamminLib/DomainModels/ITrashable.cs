using System;

namespace GiamminLib.DomainModels
{
    public interface ITrashable
    {
        bool IsDeleted => DeletedDate.HasValue;
        DateTime? DeletedDate { get; set; }
    }
}