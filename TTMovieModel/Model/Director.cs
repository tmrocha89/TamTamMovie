using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class Director
    {
        public string Name { get; private set; }

        public Director(string name)
        {
            setName(name);
        }

        private void setName(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                this.Name = name.Trim();
            }
        }
    }
}
