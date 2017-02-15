using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.Common
{
    [DataContract]
    public class TextValueJson
    {
        private String text;
        [DataMember(Name = "text")]
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        private int val;
        [DataMember(Name = "value")]
        public int Val
        {
            get { return val; }
            set { val = value; }
        }
    }
}
