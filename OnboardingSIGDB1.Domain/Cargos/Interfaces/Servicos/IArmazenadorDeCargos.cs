using OnboardingSIGDB1.Domain.Cargos.Dtos;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos
{
    public interface IArmazenadorDeCargos
    {
        Task Adicionar(CargoDto cargo);
    }
}
