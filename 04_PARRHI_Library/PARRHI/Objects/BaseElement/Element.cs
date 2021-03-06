﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.BaseElement
{
    public class Element : IElement
    {
        public static Action<string> RegisterId { get; set; }
        private string _id;
        public string id {
            get { return _id; }
            set
            {
                _id = value;
                RegisterId(_id);
            }
        }
    }
}
