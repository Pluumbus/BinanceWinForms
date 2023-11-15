using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binance;
using Bybit;
using Kucoin;
using Bitget;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net;
using CryptoExchange.Net.Authentication;
using Binance.Net.SymbolOrderBooks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Binance.Spot;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using Timer = System.Threading.Timer;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;

    }

        private async void Form1_Load(object sender, EventArgs e)
        {

            Label def = new Label();
            Label lab = new Label();
            def.Location = new Point(350, 200);
            lab.Location = new Point(355, 175);
            def.Font = new Font(def.Font.FontFamily, 15);
            lab.Font = new Font(def.Font.FontFamily, 10);
            def.ForeColor = Color.Green;
            def.Text = result;
            lab.Text = "USDT - BTC";


            Controls.Add(lab);
            while (true)
            {
                await CallPrice();
               await Task.Delay(TimeSpan.FromSeconds(5));



                if(def.Text != result)
                {
                    def.Text = result;
                }
                Controls.Add(def);



            }
        }


        public string result = "0"; 
        public  async Task CallPrice()
        {
            var websocket = new MarketDataWebSocket("btcusdt@aggTrade");

            websocket.OnMessageReceived(
                async (data) =>
                {

                    JObject jsonData = JObject.Parse(data);
                    result = (string)jsonData["p"];

                    await Task.Delay(1);


                }, CancellationToken.None);

            await websocket.ConnectAsync(CancellationToken.None);
      }


    }
}
