// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;

namespace SiliconStudio.Core.Reflection
{
    /// <summary>
    /// A custom visitor used by <see cref="DataVisitorBase"/>.
    /// </summary>
    [AssemblyScan]
    public interface IDataCustomVisitor
    {
        /// <summary>
        /// Determines whether this instance can visit the specified object.
        /// </summary>
        /// <param name="type"></param>
        /// <returns><c>true</c> if this instance can visit the specified object; otherwise, <c>false</c>.</returns>
        bool CanVisit(Type type);

        /// <summary>
        /// Visits the specified object.
        /// </summary>
        /// <param name="context">The context.</param>
        void Visit(ref VisitorContext context);
    }
}
