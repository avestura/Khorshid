using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Khorshid.Configurations
{
    public partial class Configuration
    {

        [XmlIgnore]
        public string IgnoredProperty { get; set; }

        public bool IsFirstTime { get; set; } = true;

    }
}
