// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
namespace SiliconStudio.Presentation.Windows
{
    /// <summary>
    /// An enum representing the initial position of a window shown with the <see cref="WindowManager"/>.
    /// </summary>
    public enum WindowInitialPosition
    {
        /// <summary>
        /// The window will be displayed centered relative to its owner.
        /// </summary>
        CenterOwner,
        /// <summary>
        /// The window will be displayed centered relative to the screen.
        /// </summary>
        CenterScreen,
        /// <summary>
        /// The window will be displayed close to the mouse cursor.
        /// </summary>
        MouseCursor,
    };
}
