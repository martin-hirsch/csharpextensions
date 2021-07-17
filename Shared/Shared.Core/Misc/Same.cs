using System;

namespace Shared.Core.Misc
{
    public static class Same
    {
        public static Guid Id1 { get; } = Guid.NewGuid();

        public static Guid Id2 { get; } = Guid.NewGuid();
    }
}