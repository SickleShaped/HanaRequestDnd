using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HanaRequestDnd
{
    public abstract class Request
    {
        public string Name { get; set; }
        public string RiskLevel { get; set; }
        public string Type { get; set; }
        public byte MinRollValue { get; set; }
        public byte MaxRollValue { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Price { get; set; }

        public byte Reputation { get; set; }
        public byte ReputationLoose { get; set; }
        public bool IsNecessary { get; set; }

        public Random random = new Random();

        public Request(Risks risk)
        {
            Name = RequestNames.firstNames[random.Next(0, RequestNames.firstNames.Length)] + " " + RequestNames.lastNames[random.Next(0, RequestNames.lastNames.Length)];

            switch (risk)
            {
                case Risks.Easy:
                    RiskLevel = "Городской Миф";
                    MinRollValue = 6;
                    MaxRollValue = 10;
                    MinPrice = 200;
                    MaxPrice = 300;
                    Reputation = 0;
                    ReputationLoose = 4;
                    break;

                case Risks.Medium:
                    RiskLevel = "Городская Легенда";
                    MinRollValue = 10;
                    MaxRollValue = 18;
                    MinPrice = 600;
                    MaxPrice = 1000;
                    Reputation = 1;
                    ReputationLoose = 2;
                    break;

                case Risks.Hard:
                    RiskLevel = "Городская Чума";
                    MinRollValue = 26;
                    MaxRollValue = 38;
                    MinPrice = 4200;
                    MaxPrice = 6000;
                    Reputation = 2;
                    ReputationLoose = 1;
                    break;
            }
            Price = random.Next(MinPrice, MaxPrice);

        }

        private string GenerateRequestName()
        {
            string name =  RequestNames.firstNames[random.Next(0, RequestNames.firstNames.Length)]+" " + RequestNames.lastNames[random.Next(0,RequestNames.lastNames.Length)];
            return name;
        }
    }

    public class DisposeRequest:Request
    {
        public byte MonsterDifficulty;

        public string FirstRoll;
        public string SecondRoll;
        public byte FirstRollDifficulty;
        public byte SecondRollDifficulty;
        public DisposeRequest(Risks risk):base(risk)
        {
            Type = "Устрание";
            switch (risk)
            {
                case Risks.Easy:
                    MonsterDifficulty = 2;
                    break;

                case Risks.Medium:
                    MonsterDifficulty = 4;
                    break;

                case Risks.Hard:
                    MonsterDifficulty = 13;
                    break;
            }

            int rnd = random.Next(0, 3);
            switch (rnd)
            {
                case 0:
                    FirstRoll = "Акробатика";
                    SecondRoll = "Скрытность";
                    break;

                case 1:
                    FirstRoll = "Анализ";
                    SecondRoll = "Восприятие";
                    break;

                case 2:
                    FirstRoll = "Убеждение";
                    SecondRoll = "Выступление";
                    
                    break;
            }
            FirstRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue+1);
            SecondRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue+1);

        }
    }

    public class SearchRequest:Request
    {
        public string FirstRoll;
        public string SecondRoll;
        public string ThirdRoll;
        public byte FirstRollDifficulty;
        public byte SecondRollDifficulty;
        public byte ThirdRollDifficulty;
        public SearchRequest(Risks risk) : base(risk)
        {
            Type = "Поиск";
            FirstRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            SecondRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            ThirdRollDifficulty = (byte)(random.Next(MinRollValue, MaxRollValue + 1));

            Random rnd = new Random();
            string[] checks = { "Анализ", "Проницательность", "Внимание", "Убеждение", "Запугивание", "Магия", "Природа" };
            string[] randArr = checks.OrderBy(_ => rnd.Next()).Take(3).ToArray();

            FirstRoll = randArr[0];
            SecondRoll = randArr[1];
            ThirdRoll = randArr[2];
        }
    }

    public class DefenceRequest:Request
    {
        public string FirstRoll;
        public string SecondRoll;
        public string ThirdRoll;
        public byte FirstRollDifficulty;
        public byte SecondRollDifficulty;
        public byte ThirdRollDifficulty;
        public DefenceRequest(Risks risk) : base(risk)
        {
            Type = "Защита";
            FirstRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            SecondRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            ThirdRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);

            Random rnd = new Random();
            string[] checks = {"Восприятие","Запугивание", "Проверка атаки", "Медицина", "Акробатика"};
            string[] randArr = checks.OrderBy(_ => rnd.Next()).Take(3).ToArray();

            FirstRoll = randArr[0];
            SecondRoll = randArr[1];
            ThirdRoll=randArr[2];
        }
    }

    public class EspionageRequest:Request
    {
        public string FirstRoll = "Скрытность";
        public string SecondRoll = "Восприятие";
        public string ThirdRoll;
        public byte FirstRollDifficulty;
        public byte SecondRollDifficulty;
        public byte ThirdRollDifficulty;
        public EspionageRequest(Risks risk) : base(risk)
        {
            Type = "Шпионаж";
            FirstRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            SecondRollDifficulty = (byte)random.Next(MinRollValue, MaxRollValue + 1);
            ThirdRollDifficulty = (byte)(random.Next(MinRollValue, MaxRollValue + 1));
            int rnd = random.Next(0, 2);
            switch (rnd)
            {
                case 0:
                    ThirdRoll = "Ловкость рук";
                    break;

                case 1:
                    ThirdRoll = "Акробатика";
                    break;

                case 2:
                    ThirdRoll = "Анализ";
                    break;
            }
        }
    }
}
