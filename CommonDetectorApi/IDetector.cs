using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDetectorApi
{
    /// <summary>
    /// Интерфейс плагина детектирования
    /// </summary>
    public interface IDetector
    {
        /// <summary>
        /// Название плагина
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Уникальный идентификатор плагина
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Получить список детектированных объектов на изображении
        /// </summary>
        /// <param name="image">Изображение, на котором будет происходить детектирование</param>
        /// <returns>Список детектированных объектов</returns>
        List<Sign> Detect(Bitmap image);
    }
}
