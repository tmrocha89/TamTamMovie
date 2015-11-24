using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class Director
    {
        public string Name {
            get { return this.Name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    this.Name = value.Trim();
                }
            }
        }
    }
}
