﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons
{
    public interface IData
    {
        double[] X { get; set; }
        double[] D { get; set; }
    }
}