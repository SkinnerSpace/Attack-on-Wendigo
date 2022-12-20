using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TitanBuildException : Exception
{
    public TitanBuildException(string message) : base(message) { }
}
