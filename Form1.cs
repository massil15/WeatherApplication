using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "2d1b7fbfc17441146795a0a0c78e5ac6";

        private void btn_Click(object sender, EventArgs e)
        {
            getWeather();
        }
        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", textBoxCity.Text, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                pictureBox.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                labelCondition.Text = Info.weather[0].main;
                labelDetails.Text = Info.weather[0].description;

                labelSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labelSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                labelWindSpeed.Text = Info.wind.speed.ToString();
                labelPressure.Text = Info.main.pressure.ToString();
                int tpInt = (int)((Info.main.temp) - 273.15);

                if (tpInt == 1)
                {

                    labelTemperature.Text = "Alerte Canicule (" + ((int)((Info.main.temp) - 273.15)) + "°)";
                }
                else
                {
                    labelTemperature.Text = ((int)((Info.main.temp) - 273.15)).ToString();
                }

                //labPREF1.Text = textBoxVille.Text;

            }
        }
        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();

            return day;
        }

        private void textBoxCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

        }
    }
}