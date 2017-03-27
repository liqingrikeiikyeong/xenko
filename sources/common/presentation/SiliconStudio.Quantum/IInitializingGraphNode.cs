﻿namespace SiliconStudio.Quantum
{
    /// <summary>
    /// An interface representing an <see cref="IGraphNode"/> during its initialization phase.
    /// </summary>
    public interface IInitializingGraphNode : IGraphNode
    {
        /// <summary>
        /// Seal the node, indicating its construction is finished and that no more children will be added.
        /// </summary>
        void Seal();
    }
}
