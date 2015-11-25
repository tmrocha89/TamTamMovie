using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class Title
    {
        public string Name { get; private set; }

        public Title(string name)
        {
            setTitle(name);
        }

        private void setTitle(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                this.Name = name.Trim();
            }
        }
    }
}
