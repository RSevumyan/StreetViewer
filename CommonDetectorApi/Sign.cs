
namespace CommonDetectorApi
{
    /// <summary>
    /// Класс, инкапсулирующий информацию по детектированному объекту
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// Координата начальной точки области детектированного объекта по оси x
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Координата начальной точки области детектированного объекта по оси y
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Ширина области детектированного объекта
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Высота области детектированного объекта
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Класс детектированного объекта
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Конструктор результата детектирования
        /// </summary>
        /// <param name="x">Начальная точка облачти детектированног объекта по x</param>
        /// <param name="y">Начальная точка облачти детектированног объекта по y</param>
        /// <param name="width">Ширина области детектированного объекта</param>
        /// <param name="height">Высота области детектированного объекта</param>
        /// <param name="className">Класс детектированного объекта</param>
        public Sign(int x, int y, int width, int height, string className)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            ClassName = className;
        }
    }
}
