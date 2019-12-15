using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Service
{
    public class Drawing
    {

        #region Public

        public static Bitmap Draw(Forecast p_Forecast, double p_Scale)
        {
            if (p_Forecast != null && p_Forecast.Rows != null && p_Forecast.Rows.Count > 0)
            {
                #region Init

                double minTemp = p_Forecast.Rows.Min(i => i.Temperature), maxTemp = p_Forecast.Rows.Max(i => i.Temperature);
                var forecastRows = p_Forecast.Rows.OrderBy(i => i.TimeFrom).ToList();
                DateTime minTime = p_Forecast.Rows[0].TimeFrom;

                int height = (int)Math.Ceiling(300 * p_Scale), width = (int)Math.Ceiling(p_Forecast.Rows.Count * 15 * p_Scale);
                int heightSeparator = (int)Math.Ceiling(50 * p_Scale);
                int heightTop = (int)Math.Ceiling(100 * p_Scale);
                int heightBottom = (int)Math.Ceiling(150 * p_Scale);

                Bitmap bmp = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bmp);

                #endregion Init

                #region DrawingTemplate

                string text = $"{p_Forecast.Location} - {p_Forecast.Country}";
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));
                g.DrawLine(new Pen(Color.Black, 5), 0, heightSeparator, width, heightSeparator);
                g.DrawString(text, new Font(FontFamily.GenericSansSerif, (int)Math.Ceiling(30 * p_Scale)), new SolidBrush(Color.Black), 0, 0);

                #endregion DrawingTemplate

                #region DrawingTemperatures

                DateTime newDay = DateTime.MinValue;
                for (int i = 0; i < forecastRows.Count; i++)
                {
                    var row = forecastRows[i];
                    int widthTemp = (int)Math.Ceiling(15.0 * p_Scale);
                    int heightTemp = heightBottom - (int)Math.Ceiling(((row.Temperature - minTemp) * 100.0 / (maxTemp - minTemp)) * heightBottom / 100.0) + heightTop;

                    g.FillRectangle(new SolidBrush(row.Temperature < 0 ? Color.LightBlue : Color.LightGreen), new Rectangle(widthTemp * i, heightTemp, widthTemp, height));

                    if (row.TimeTo.Date != newDay.Date)
                    {
                        newDay = row.TimeTo;
                        if (i > 0)
                        {
                            g.DrawLine(new Pen(Color.Red, 1), widthTemp * i, heightSeparator, widthTemp * i, height);
                            g.DrawString($"{row.TimeTo.ToString("dd/MM/yyyy")}", new Font(FontFamily.GenericSansSerif, (int)Math.Ceiling(10 * p_Scale)), new SolidBrush(Color.Red), widthTemp * i+2, heightSeparator+2);
                        }
                    }

                    GraphicsState state = g.Save();
                    g.RotateTransform(-90);
                    g.TranslateTransform(widthTemp * i + 2, heightTemp, MatrixOrder.Append);
                    g.DrawString($"{row.Temperature}", new Font(FontFamily.GenericSansSerif, (int)Math.Ceiling(10 * p_Scale)), new SolidBrush(Color.Red), 0,0 );
                    g.Restore(state);
                }

                #endregion DrawingTemperatures

                #region ZeroLine

                if (minTemp < 0 && maxTemp > 0)
                {
                    int heightTemp = heightBottom - (int)Math.Ceiling(((0 - minTemp) * 100.0 / (maxTemp - minTemp)) * heightBottom / 100.0) + heightTop;
                    g.DrawLine(new Pen(Color.Blue, 1), 0, heightTemp, width, heightTemp);
                }

                #endregion ZeroLine

                return bmp;
            }
            return null;
        }

        #endregion Public

    }
}
