using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecteur_RSS
{
    class RSS
    {
        private string title;
        private string description;
        private string link;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Link
        {
            get { return link; }
            set { link = value; }
        }
    }
}
