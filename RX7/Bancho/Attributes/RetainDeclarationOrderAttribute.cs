using System;
using System.Runtime.CompilerServices;

namespace RX7.Bancho.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RetainDeclarationOrderAttribute : Attribute {
        public int Order;

        public RetainDeclarationOrderAttribute([CallerLineNumber] int order = 0) {
            this.Order = order;
        }
    }
}
