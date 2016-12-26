﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Internals;
using EntryPoint.OptionStrategies;
using System.Reflection;

namespace EntryPoint.OptionModel {
    internal class ModelOperand {
        public ModelOperand(PropertyInfo property) {
            Property = property;
            Definition = property.GetOperandDefinition();
            Required = property.HasRequiredAttribute();
        }

        // The original property on the ApplicationOptions implementation
        public PropertyInfo Property { get; set; }

        // Operand attribute
        public OperandAttribute Definition { get; set; }

        // Strategy for value getting
        public OperandStrategy OperandStrategy { get; private set; } = new OperandStrategy();

        // Whether the Option is required
        public bool Required { get; private set; }
    }
}
