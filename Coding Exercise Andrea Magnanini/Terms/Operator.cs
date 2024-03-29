﻿namespace Coding_Exercise_Andrea_Magnanini.Terms
{
    using Enums;

    public class Operator : Term, IOperator
    {
        public Operator(OperatorTypeEnum type)
        {
            this.Type = type;
        }

        public OperatorTypeEnum Type { get; }
    }
}