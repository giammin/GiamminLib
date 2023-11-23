using System;

namespace GiamminLib.DomainModels;

public interface IClockProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTimeOffset NowOffset { get; }
    DateTimeOffset UtcNowOffset { get; }
}