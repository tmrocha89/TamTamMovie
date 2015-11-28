using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class Star
    {
        public string Name { get; private set; }

        public Star(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                this.Name = name.Trim();
            }
        }
    }
}
