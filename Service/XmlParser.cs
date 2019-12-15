using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Service
{

    #region Forecast

    public class Forecast
    {
        public string Location { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public List<ForecastRow> Rows { get; set; }
    }

    public class ForecastRow
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public double WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public double Temperature { get; set; }
    }

    #endregion Forecast

    #region ParsingException

    public class ParsingException : Exception
    {
        public ParsingException()
        {

        }

        public ParsingException(string p_Text)
            : base(String.Format("Could not parse xml file"))
        {

        }
    }

    #endregion ParsingException

    public class XmlParser
    {

        #region Private 

        public string XML { get; set; }

        #endregion Private

        #region ctor

        public XmlParser(string p_XML)
        {
            XML = p_XML;
        }

        #endregion ctor

        #region Logic

        public static Forecast Parse(string p_XML)
        {
            var output = new Forecast
            {
                Rows = new List<ForecastRow>()
            };

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(p_XML);

                //try parsing error
                //Convert.ToDateTime(xml.Attributes.GetNamedItem("from").Value);

                #region ParsingData

                foreach (XmlNode xmlNode in xml)
                {
                    if (xmlNode.Name == "weatherdata")
                    {
                        foreach (XmlNode rowNode in xmlNode.ChildNodes)
                        {
                            if (rowNode.Name == "forecast")
                            {
                                foreach (XmlNode forecastNode in rowNode.ChildNodes)
                                {
                                    if (forecastNode.Name == "time")
                                    {
                                        var outputRow = new ForecastRow
                                        {
                                            TimeFrom = Convert.ToDateTime(forecastNode.Attributes.GetNamedItem("from").Value),
                                            TimeTo = Convert.ToDateTime(forecastNode.Attributes.GetNamedItem("to").Value)
                                        };

                                        foreach (XmlNode timeNode in forecastNode.ChildNodes)
                                        {
                                            if (timeNode.Name == "temperature")
                                            {
                                                double.TryParse(timeNode.Attributes.GetNamedItem("value").Value, NumberStyles.Any, CultureInfo.GetCultureInfo("en"), out double result);
                                                outputRow.Temperature = result;
                                            }
                                            if (timeNode.Name == "windDirection")
                                                outputRow.WindDirection = Convert.ToInt32(timeNode.Attributes.GetNamedItem("deg").Value);
                                            if (timeNode.Name == "windSpeed")
                                            {
                                                double.TryParse(timeNode.Attributes.GetNamedItem("mps").Value, NumberStyles.Any, CultureInfo.GetCultureInfo("en"), out double result);
                                                outputRow.WindSpeed = result;
                                            }
                                        }

                                        output.Rows.Add(outputRow);
                                    }
                                }
                            }

                            else if (rowNode.Name == "location")
                            {
                                foreach (XmlNode locationNode in rowNode.ChildNodes)
                                {
                                    if (locationNode.Name == "name")
                                        output.Location = locationNode.InnerText;
                                    if (locationNode.Name == "country")
                                        output.Country = locationNode.InnerText;
                                    if (locationNode.Name == "timezone")
                                        output.Timezone = locationNode.InnerText;
                                }
                            }
                        }
                    }
                }

                #endregion ParsingData

            }
            catch
            {
                throw new ParsingException(p_XML);
            }

            return output;
        }

        #endregion Logic
    }
}
