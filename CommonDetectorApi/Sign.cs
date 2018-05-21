using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDetectorApi
{
    public class Sign
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private string className;

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string ClassName
        {
            get { return className; }
        }

        public Sign(int x, int y, int width, int height, string className)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.className = className;
        }
    }
}
