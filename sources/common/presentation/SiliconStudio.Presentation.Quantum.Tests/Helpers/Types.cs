// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System.Collections.Generic;
using SiliconStudio.Core;
using SiliconStudio.Presentation.Quantum.Presenters;
using SiliconStudio.Quantum;
using SiliconStudio.Quantum.References;

namespace SiliconStudio.Presentation.Quantum.Tests.Helpers
{
    public static class Types
    {
        public class TestPropertyProvider : IPropertyProviderViewModel
        {
            private readonly IObjectNode rootNode;

            public TestPropertyProvider(IObjectNode rootNode)
            {
                this.rootNode = rootNode;
            }
            public bool CanProvidePropertiesViewModel => true;

            public IObjectNode GetRootNode()
            {
                return rootNode;
            }

            bool IPropertyProviderViewModel.ShouldConstructMember(IMemberNode member) => true;

            bool IPropertyProviderViewModel.ShouldConstructItem(IObjectNode collection, Index index) => true;
        }

        public class SimpleObject
        {
            public string Name { get; set; }

            public string Nam { get; set; } // To test scenario when Name.StartsWith(Nam) is true
        }

        public class DependentPropertyContainer
        {
            public string Title { get; set; }

            public SimpleObject Instance { get; set; }
        }

        public class SimpleType
        {
            public string String { get; set; }
        }

        public class ClassWithRef
        {
            [Display(1)]
            public string String { get; set; }
            [Display(2)]
            public ClassWithRef Ref { get; set; }
        }

        public class ClassWithCollection
        {
            [Display(1)]
            public string String { get; set; }
            [Display(2)]
            public List<string> List { get; set; } = new List<string>();
        }

        public class ClassWithRefCollection
        {
            [Display(1)]
            public string String { get; set; }
            [Display(2)]
            public List<SimpleType> List { get; set; } = new List<SimpleType>();
        }
    }
}
