﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class RequiredParameterArgsModel : BaseApplicationOptions {
        [OptionParameter(
            DoubleDashName = "param-required",
            SingleDashChar = 'r')]
        [OptionRequired]
        public int ParamRequired { get; set; }

        [OptionParameter(
            DoubleDashName = "param-optional",
            SingleDashChar = 'o',
            NullValueBehaviour = ParameterDefaultEnum.CustomValue,
            CustomDefaultValue = 7)]
        public int ParamOptional { get; set; }
    }
}
