// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections;
using System.Collections.Generic;

namespace SiliconStudio.Shaders.Ast
{
    /// <summary>
    /// A declaration inside a statement.
    /// </summary>
    public partial class DeclarationStatement : Statement
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "DeclarationStatement" /> class.
        /// </summary>
        public DeclarationStatement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclarationStatement"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        public DeclarationStatement(Node content)
        {
            Content = content;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the content.
        /// </summary>
        /// <value>
        ///   The content.
        /// </value>
        public Node Content { get; set; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override IEnumerable<Node> Childrens()
        {
            ChildrenList.Clear();
            ChildrenList.Add(Content);
            return ChildrenList;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("{0};", Content == null ? string.Empty : Content.ToString());
        }

        #endregion
    }
}
