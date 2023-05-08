using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using webapi.Utilities;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodicTableController : Controller
    {
        private static readonly List<Element> _elementos = new List<Element>();
        public PeriodicTableController()
        {

            _elementos.Add(new(1, "Hidrogênio", "H", 1.008, 1, 1, new int[2] { -1, 1 }, StandardStates.Gas, 0.0000899));
            _elementos.Add(new(2, "Hélio", "He", 4.0026, 1, 18, Array.Empty<int>(), StandardStates.Gas, 0.0001785));
            _elementos.Add(new(3, "Lítio", "Li", 6.941, 2, 1, new int[1] { 1 }, StandardStates.Solid, 0.535));
            _elementos.Add(new(4, "Berílio", "Be", 9.012182, 2, 2, new int[1] { 2 }, StandardStates.Solid, 1.848));
            _elementos.Add(new(5, "Boro", "B", 10.811, 1, 13, new int[3] { 1, 2, 3 }, StandardStates.Solid, 2.46));
            _elementos.Add(new(6, "Carbono", "C", 12.0107, 2, 14, new int[8] { -4, -3, -2, -1, 1, 2, 3, 4 }, StandardStates.Solid, 2.26));
            _elementos.Add(new(7, "Nitrogênio", "N", 14.0067, 2, 15, new int[8] { -3, -2, -1, 1, 2, 3, 4, 5 }, StandardStates.Gas, 0.001251));
            _elementos.Add(new(8, "Oxigênio", "O", 15.9994, 2, 16, new int[4] { -2, -1, 1, 2 }, StandardStates.Gas, 0.001429));
            _elementos.Add(new(9, "Flúor", "F", 18.9984, 2, 17, new int[1] { -1 }, StandardStates.Gas, 0.001696));
            _elementos.Add(new(10, "Néon", "Ne", 20.1797, 2, 18, Array.Empty<int>(), StandardStates.Gas, 0.0009));
        }

        [HttpGet("all")]
        public ActionResult<object> GetAll()
        {
            return _elementos;
        }

        [HttpGet("name/{name}")]
        public ActionResult<object> GetByName(string name)
        {
            return _elementos.Where(a => a.Nome.Equals(name)).ToList();
        }

        [HttpGet("nAtomico/{number}")]
        public ActionResult<object> GetByAtomicNumber(int number)
        {
            return _elementos.Find(e => e.NumeroAtomico == number);
        }

        [HttpGet("periodo/{periodo}")]
        public ActionResult<object> GetByPeriod(int periodo)
        {
            return _elementos.Where(a => a.Periodo == periodo).ToList();
        }

        [HttpGet("estadosOxidacao/{oxState}")]
        public ActionResult<object> GetByOxidationState(int oxState)
        {
            return _elementos.FindAll(ox => ox.EstadosDeOxidacao.Contains(oxState));
        }

        [HttpGet("estadoFisico/{standardState}")]
        public ActionResult<object> GetByStandardState(string standardState)
        {
            return _elementos.Where(e => e.StandardState.ToString().Equals(standardState)).ToList();
        }

        [HttpGet("densidade/{densidade}")]
        public ActionResult<object> GetByHigherDensity(double densidade)
        {
            return _elementos.Where(e => e.Densidade >= densidade).ToList();
        }

        [HttpGet("balanceamento/{equacao}")]
        public ActionResult<object> Balancear(string equacao)
        {
            ChemicalEquation eqQuimica = new(equacao);
            return eqQuimica.Balance();
        }

    }
}
