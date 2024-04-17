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
        public bool IsNecessary { get; set; }


        public Request(Risks risk)
        {
            Name = GenerateRequestName();
            switch (risk)
            {
                case Risks.Easy:
                    RiskLevel = "Городской Миф";
                    MinRollValue = 6;
                    MaxRollValue = 10;
                    MinPrice = 200;
                    MaxPrice = 300;
                    break;

                case Risks.Medium:
                    RiskLevel = "Городская Легенда";
                    MinRollValue = 10;
                    MaxRollValue = 18;
                    MinPrice = 600;
                    MaxPrice = 1000;
                    break;

                case Risks.Hard:
                    RiskLevel = "Городская Чума";
                    MinRollValue = 26;
                    MaxRollValue = 38;
                    MinPrice = 4200;
                    MaxPrice = 6000;
                    break;
            }

        }

        private string GenerateRequestName()
        {

            string name = "";

            return name;
        }
    }

    public class DisposeRequest:Request
    {
        public byte MonsterDifficulty;
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
        }
    }

    public class SearchRequest:Request
    {
        public SearchRequest(Risks risk) : base(risk)
        {
            Type = "Поиск";
        }
    }

    public class DefenceRequest:Request
    {
        public DefenceRequest(Risks risk) : base(risk)
        {
            Type = "Защита";
        }
    }

    public class EspionageRequest:Request
    {
        public EspionageRequest(Risks risk) : base(risk)
        {
            Type = "Шпионаж";
        }
    }
}
