using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanaRequestDnd
{
    public partial class Form1 : Form
    {
        public byte Week { get; set; } = 1;
        public int Reputation { get; set; } = 0;
        public int MinRequestCount = 2;
        public int MaxRequestCount = 4;
        string path = @"D://DndHana.txt";

        List<Request> CurrentRequests { get; set; } = new List<Request>();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            CurrentRequests.Clear();

            Week++;
            if (Week >= 5) Week = 1;
            label2.Text = Week+"/4";

            MinRequestCount = 2 + Reputation / 10;
            MaxRequestCount = 4 + (Reputation + 5) / 10;
            if (Reputation >= 50)
            {
                MinRequestCount = 7;
                MaxRequestCount = 10;
            }

            var requests = GenerateRequests(CurrentRequests, MinRequestCount, MaxRequestCount, Reputation);
            if(Week == 1) { requests[0].IsNecessary = true; }

            foreach(var request in requests)
            {
                if (request is EspionageRequest)
                {
                    EspionageRequest typerequest = request as EspionageRequest;
                    string Necessary = (request.IsNecessary==true) ? "Да" : "Нет";

                    string requess = $"Название: {typerequest.Name}\n    Риск: {typerequest.RiskLevel}, Тип запроса: {typerequest.Type}, Обязательность: {Necessary}" +
                        "\n        Броски:" +
                        $"\n        --{typerequest.FirstRoll}"+
                        $"\n        --{typerequest.SecondRoll}"+
                        $"\n        --{typerequest.ThirdRoll}"+
                        $"\n    Оплата: {typerequest.Price}, Репутация в случае выполнения: {typerequest.Reputation}, Потеря репутации в случае поражения: {typerequest.ReputationLoose}\n ";
                    listBox1.Items.Add(requess);
                }
                if(request is DisposeRequest)
                {
                    DisposeRequest typerequest = request as DisposeRequest;
                    string Necessary = (request.IsNecessary == true) ? "Да" : "Нет";

                    string requess = $"Название: {typerequest.Name}\n    Риск: {typerequest.RiskLevel}, Тип запроса: {typerequest.Type}, Обязательность: {Necessary}" +
                        "\n        Броски:" +
                        $"\n        --{typerequest.FirstRoll}" +
                        $"\n        --{typerequest.SecondRoll}" +
                        $"\n        Уровень опасности существа: {typerequest.MonsterDifficulty}"+
                        $"\n    Оплата: {typerequest.Price}, Репутация в случае выполнения: {typerequest.Reputation}, Потеря репутации в случае поражения: {typerequest.ReputationLoose}\n ";
                    listBox1.Items.Add(requess);
                }
                if(request is DefenceRequest)
                {
                    DefenceRequest typerequest = request as DefenceRequest;

                    string Necessary = (request.IsNecessary == true) ? "Да" : "Нет";
                    string requess = $"Название: {typerequest.Name}\n    Риск: {typerequest.RiskLevel}, Тип запроса: {typerequest.Type}, Обязательность: {Necessary}" +
                        "\n        Броски:" +
                        $"\n        --{typerequest.FirstRoll}" +
                        $"\n        --{typerequest.SecondRoll}" +
                        $"\n        --{typerequest.ThirdRoll}" +
                        $"\n    Оплата: {typerequest.Price}, Репутация в случае выполнения: {typerequest.Reputation}, Потеря репутации в случае поражения: {typerequest.ReputationLoose}\n ";
                    listBox1.Items.Add(requess);
                }
                if(request is SearchRequest)
                {
                    SearchRequest typerequest = request as SearchRequest;

                    string Necessary = (request.IsNecessary == true) ? "Да" : "Нет";
                    string requess = $"Название: {typerequest.Name}\n    Риск: {typerequest.RiskLevel}, Тип запроса: {typerequest.Type}, Обязательность: {Necessary}" +
                        "\n        Броски:" +
                        $"\n        --{typerequest.FirstRoll}" +
                        $"\n        --{typerequest.SecondRoll}" +
                        $"\n        --{typerequest.ThirdRoll}" +
                        $"\n    Оплата: {typerequest.Price}, Репутация в случае выполнения: {typerequest.Reputation}, Потеря репутации в случае поражения: {typerequest.ReputationLoose}\n ";
                    listBox1.Items.Add(requess);
                }              
            }
        }

        public void ReadFile()
        {
            using (StreamReader sr = File.OpenText(path))
            {
                Reputation = Int32.Parse(sr.ReadLine());
                Week = Byte.Parse(sr.ReadLine());
                label2.Text = Week + "/4";
                label4.Text = Reputation.ToString();

            }
        }

        public void WriteFile()
        {
            
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(Reputation);
                sw.WriteLine(Week);
            }
        }

        public List<Request> GenerateRequests(List<Request> requests, int min, int max, int reputation)
        {
            Random rnd = new Random();
            int count = rnd.Next(min, max+1);
            for (int i = 0; i < count; i++)
            {
                System.Threading.Thread.Sleep(10);
                int type = rnd.Next(0,4);
                int risktype = rnd.Next(0,3);
                Risks risk = Risks.Easy;

                switch(risktype)
                {
                    case 0:
                        risk = Risks.Easy;
                        break;

                    case 1:
                        risk = Risks.Medium;
                        break;

                    case 2:
                        risk = Risks.Hard;
                        break;
                }
                switch (type)
                {
                    case 0:
                        var disposeRequest = new DisposeRequest(risk);

                        ModifyPrice(disposeRequest, reputation);
                        requests.Add(disposeRequest);
                        break;

                    case 1:
                        var defenceRequest = new DefenceRequest(risk);
                        ModifyPrice(defenceRequest, reputation);
                        requests.Add(defenceRequest);
                        break;

                    case 2:
                        var espionageRequest = new EspionageRequest(risk);
                        ModifyPrice(espionageRequest, reputation);
                        requests.Add(espionageRequest);
                        break;

                    case 3:
                        var searchRequest = new SearchRequest(risk);
                        ModifyPrice(searchRequest, reputation);
                        requests.Add(searchRequest);
                        break;
                } 
            }

            return requests;
        }

        public void ModifyPrice(Request request, int reputation)
        {
            double reputationBonus = (double)reputation / 100;
            if (reputation >= 50) { reputationBonus = 0.5; }
            if (reputation < 0) { reputationBonus = 0; }
            request.Price = (int)(request.Price + request.Price * (reputationBonus));
        }

        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listBox1.Items[e.Index].ToString(), listBox1.Font, listBox1.Width).Height;
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Reputation = Reputation + CurrentRequests[listBox1.SelectedIndex].Reputation;
                label4.Text = Reputation.ToString();
                CurrentRequests.Remove(CurrentRequests[listBox1.SelectedIndex]);
                listBox1.Items.Remove(listBox1.SelectedItem);
                WriteFile();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Reputation = Reputation - CurrentRequests[listBox1.SelectedIndex].ReputationLoose;
                label4.Text=Reputation.ToString();
                CurrentRequests.Remove(CurrentRequests[listBox1.SelectedIndex]);
                listBox1.Items.Remove(listBox1.SelectedItem);
                WriteFile();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (CurrentRequests[listBox1.SelectedIndex] is DisposeRequest)
                {
                    var request = CurrentRequests[listBox1.SelectedIndex] as DisposeRequest;
                    MessageBox.Show($"Сложности бросков:\n{request.FirstRoll}-{request.FirstRollDifficulty},\n{request.SecondRoll}-{request.SecondRollDifficulty}");
                }
                if (CurrentRequests[listBox1.SelectedIndex] is DefenceRequest)
                {
                    var request = CurrentRequests[listBox1.SelectedIndex] as DefenceRequest;
                    MessageBox.Show($"Сложности бросков:\n{request.FirstRoll}-{request.FirstRollDifficulty},\n{request.SecondRoll}-{request.SecondRollDifficulty},\n{request.ThirdRoll}-{request.ThirdRollDifficulty}");
                }
                if (CurrentRequests[listBox1.SelectedIndex] is EspionageRequest)
                {
                    var request = CurrentRequests[listBox1.SelectedIndex] as EspionageRequest;
                    MessageBox.Show($"Сложности бросков:\n{request.FirstRoll}-{request.FirstRollDifficulty},\n{request.SecondRoll}-{request.SecondRollDifficulty},\n{request.ThirdRoll}-{request.ThirdRollDifficulty}");
                }
                if (CurrentRequests[listBox1.SelectedIndex] is SearchRequest)
                {
                    var request = CurrentRequests[listBox1.SelectedIndex] as SearchRequest;
                    MessageBox.Show($"Сложности бросков:\n{request.FirstRoll}-{request.FirstRollDifficulty},\n{request.SecondRoll}-{request.SecondRollDifficulty},\n{request.ThirdRoll}-{request.ThirdRollDifficulty}");
                }


            }

        }
    }
}
