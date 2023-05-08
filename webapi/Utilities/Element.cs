using System.Xml.Linq;

namespace webapi.Utilities
{
    public enum StandardStates { Solid, Liquid, Gas };
    public class Element
    {
        public int NumeroAtomico { get; set; }
        public string Nome { get; set; }
        public string Simbolo { get; set; }
        public double MassaAtomica { get; set; }
        public string ConfiguracaoEletronica { get; set; }
        public double Eletronegatividade { get; set; }
        public int RaioAtomico { get; set; }
        public string RaioIonico { get; set; }
        public int RaioDeVanDerWaals { get; set; }
        public int EnergiaDeIonizacao { get; set; }
        public int AfinidadeEletronica { get; set; }

        public int[]? EstadosDeOxidacao { get; set; }
        public StandardStates StandardState { get; set; } //enum
        public int PontoDeFusao { get; set; }
        public int PontoDeEbulicao { get; set; }
        public double Densidade { get; set; }
        public string Familia { get; set; } //grupo, pode ser enum
        public int AnoDescoberta { get; set; }
        public int Periodo { get; set; }
        public int Grupo { get; set; }

        public Element()
        {
        }

        public Element(int numeroAtomico, string nome, string simbolo, double massaAtomica, int periodo, int grupo, int[] estadosDeOxidacao, StandardStates estado, double densidade)
        {
            NumeroAtomico = numeroAtomico;
            Nome = nome;
            Simbolo = simbolo;
            MassaAtomica = massaAtomica;
            Periodo = periodo;
            Grupo = grupo;
            EstadosDeOxidacao = estadosDeOxidacao;
            StandardState = estado;
            Densidade = densidade;
        }
        public static object getHigherDensity(double density, List<Element> list)
        {
            return list.Where(e => e.Densidade >= density).ToList();
        }
    }
}
