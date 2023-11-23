using System;
using GiamminLib.DomainModels;

namespace GiamminLib.Patterns;

public class DefaultClockProvider : IClockProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset NowOffset => DateTimeOffset.Now;
    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
}