namespace webapi.Utilities
{
    public class Substance
    {

        public string Name { get; set; } //É o elemento
        public int Coefficient { get; set; }
        public Dictionary<string, int> Elements = new Dictionary<string, int>(); //elementos e quantidade

        public Substance(string name)
        {
            Name = name;
            GetElementValue();
        }
        public void GetElementValue()
        {
            string result = "";
            int number;
            string aux = "";
            string aux3 = "";


            for (int i = 0; i < Name.Length; i++)
            {

                if (char.IsNumber(Name[i]) && !char.IsPunctuation(Name[i]))
                {
                    number = int.Parse(Name[i] + "");
                    Elements.Add(result, number);
                    result = "";
                    continue;
                }
                if (char.IsUpper(Name[i]) || char.IsPunctuation(Name[i])) //por isso adiciona string vazia
                {
                    if (char.IsLower(Name[i]))
                    {
                        if (!Elements.ContainsKey(result))
                        {
                            string aux2 = result + Name[i];
                            Elements.Add(aux2, 1);
                            result = "";
                        }

                    }
                    else
                    {
                        if (!Elements.ContainsKey(result))
                        {
                            Elements.Add(result, 1);
                            result = "";
                        }
                    }

                }
                if (char.IsPunctuation(Name[i]))
                {
                    while (true)
                    {
                        i++;
                        aux += Name[i];
                        if (char.IsPunctuation(Name[i]))
                        {
                            number = int.Parse(Name[i + 1] + "");
                            break;
                        }

                    }

                    foreach (char c in aux)
                    {
                        aux3 += c;
                        if (char.IsUpper(c))
                        {
                            if (!Elements.ContainsKey(aux3))
                                Elements.Add(aux3, number);
                            aux3 = "";
                        }
                    }
                }
                result += Name[i];
            }
            if (!Elements.ContainsKey(result))
            {
                Elements.Add(result, 1);
            }

            Elements.Remove(")");
            Elements.Remove("");
        }
    }
}
