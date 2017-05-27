using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

using StreetViewer.JsonObjects.GoogleApiJson.Geocoding;
using StreetViewer.JsonObjects.GoogleApiJson.Direction;
using StreetViewer.JsonObjects.GoogleApiJson.Common;

namespace StreetViewer.Service
{
    /// <summary>
    /// Сервис работы с Google Map API.
    /// </summary>
    public class GoogleRestService
    {
        private const String GOOGLE_API_KEY = "AIzaSyDZvb2R8tCxXLOJQMo7i-38-wzRGmPKKLE";
        private const String GEOCODING_URL_FORMAT = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        private const String DIRECTION_URL_FORMAT = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode=driving&key={2}";
        private const String STREET_VIEW_URL_FORMAT = "https://maps.googleapis.com/maps/api/streetview?size=640x640&location={0},{1}&heading={2}&pitch=-0.76&key={3}";
        private const String GEOCODING_REVERS_URL_FORMAT = "https://maps.googleapis.com/maps/api/geocode/json?latlng={0}&key={1}";

        private const int BYTES_LENGTH = 2048;

        /// <summary>
        /// Запрос к Geocoding API c ответом в виде GeocodeJsonReply.
        /// Данный метод используется для получения географических координат заданного запроса.
        /// </summary>
        /// <param name="place">Запрос (улица, город, метро или любое иное название).</param>
        /// <returns>GeocodeJsonReply с ответным результатом.</returns>
        public GeocodeJsonReply getGeocoding(String place)
        {
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(GEOCODING_URL_FORMAT, place, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeJsonReply));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as GeocodeJsonReply;
        }

        /// <summary>
        /// Запрос к Geocoding API c ответом в виде GeocodeJsonReply.
        /// Данный метод используется для получения информации с адресом 
        /// и сопутствующей информацией по заданным координатам.
        /// </summary>
        /// <param name="lat">Широта.</param>
        /// <param name="lng">Долгота.</param>
        /// <returns>GeocodeJsonReply с ответным результатом.</returns>
        public GeocodeJsonReply getGeocoding(string lat, string lng)
        {
            string place = lat.Replace(",", ".")+"," + lng.Replace(",", ".");
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(GEOCODING_REVERS_URL_FORMAT, place, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeJsonReply));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as GeocodeJsonReply;
        }

        /// <summary>
        /// Запрос к Direction API с ответом в виде DirectionsStatusJson.
        /// Данный метод используется для получения пути между двумя координатами.
        /// </summary>
        /// <param name="start">
        /// Начальная точка, в формате xxx.xxxx,yyy.yyyy,
        /// или географическое наименование, которое будет началом пути.
        /// </param>
        /// <param name="end">
        /// Конечная точка, в формате xxx.xxxx,yyy.yyyy,
        /// или географическое наименование, которое будет концом пути.
        /// </param>
        /// <returns>DirectionsStatusJson с ответным результатом</returns>
        public DirectionsStatusJson getDirection(string start, string end)
        {
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(DIRECTION_URL_FORMAT, start, end, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DirectionsStatusJson));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as DirectionsStatusJson;
        }

        /// <summary>
        /// Запрос к StreetView API.
        /// Данный метод используется для получения панорамных снимков заданной точки.
        /// </summary>
        /// <param name="lat">Широта.</param>
        /// <param name="lng">Долгота.</param>
        /// <param name="heading">Угол поворота снимка.</param>
        /// <returns>Поток содержащий изображение.</returns>
        public Stream getStreetViewStream(string lat, string lng, string heading)
        {
            string url = String.Format(STREET_VIEW_URL_FORMAT, lat.Replace(",", "."), lng.Replace(",", "."), heading.Replace(",", "."), GOOGLE_API_KEY);
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();
        }
    }
}
