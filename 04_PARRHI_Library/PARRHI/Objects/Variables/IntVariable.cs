using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.Objects.Variables
{
    public class IntVariable : BaseElement.Element
    {
        public IntVariable(string id, byte value)
        {
            this.id = id;
            Value = value;
        }

        private int Value { get; set; }

        public void IncrementValue()
        {
            Value++;
        }
        public int GetValue()
        {
            return Value;
        }
    }
}
