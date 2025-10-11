using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using System.Reflection;

namespace Eventify.Data
{
    public static class DataSeeker
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            var estadosExistentes = await unitOfWork.EstadoRepository.GetEstados();
            if (estadosExistentes.Any())
            {
                return;
            }

            var nomesDosEstados = GetNomesEstados();

            var municipiosDoCsv = await ReadMunicipiosFromCsv();

            var estadosCriados = new Dictionary<string, Estado>();
            var estadosParaSalvar = municipiosDoCsv
                .Select(m => m.Uf)
                .Distinct()
                .ToList();

            foreach (var sigla in estadosParaSalvar)
            {
                var novoEstado = new Estado
                {
                    Id = Guid.NewGuid(),
                    Sigla = sigla,
                    Nome = nomesDosEstados.ContainsKey(sigla) ? nomesDosEstados[sigla] : "Desconhecido"
                };
                estadosCriados.Add(sigla, novoEstado);
                await unitOfWork.EstadoRepository.Salvar(novoEstado);
            }
            await unitOfWork.CompleteAsync();

            foreach (var municipioInfo in municipiosDoCsv)
            {
                if (estadosCriados.TryGetValue(municipioInfo.Uf, out var estadoPai))
                {
                    var novaCidade = new Cidade
                    {
                        Id = Guid.NewGuid(),
                        Nome = municipioInfo.NomeMunicipio,
                        EstadoId = estadoPai.Id
                    };
                    await unitOfWork.CidadeRepository.Salvar(novaCidade);
                }
            }
            await unitOfWork.CompleteAsync();
        }

        private static async Task<List<(string Uf, string NomeMunicipio)>> ReadMunicipiosFromCsv()
        {
            var municipios = new List<(string Uf, string NomeMunicipio)>();
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = "Eventify.Resources.Raw.municipios.csv";

            using (var stream = await FileSystem.OpenAppPackageFileAsync("municipios.csv"))
            {
                if (stream == null) return municipios;

                using (var reader = new StreamReader(stream))
                {
                    await reader.ReadLineAsync();

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var values = line.Split(';');
                        if (values.Length >= 4)
                        {
                            var uf = values[0].Trim();
                            var nomeMunicipio = values[3].Trim();
                            if (!string.IsNullOrEmpty(uf) && !string.IsNullOrEmpty(nomeMunicipio))
                            {
                                municipios.Add((uf, nomeMunicipio));
                            }
                        }
                    }
                }
            }

            return municipios;
        }

        private static Dictionary<string, string> GetNomesEstados()
        {
            return new Dictionary<string, string>
            {
                { "AC", "Acre" }, { "AL", "Alagoas" }, { "AP", "Amapá" }, { "AM", "Amazonas" },
                { "BA", "Bahia" }, { "CE", "Ceará" }, { "DF", "Distrito Federal" }, { "ES", "Espírito Santo" },
                { "GO", "Goiás" }, { "MA", "Maranhão" }, { "MT", "Mato Grosso" }, { "MS", "Mato Grosso do Sul" },
                { "MG", "Minas Gerais" }, { "PA", "Pará" }, { "PB", "Paraíba" }, { "PR", "Paraná" },
                { "PE", "Pernambuco" }, { "PI", "Piauí" }, { "RJ", "Rio de Janeiro" }, { "RN", "Rio Grande do Norte" },
                { "RS", "Rio Grande do Sul" }, { "RO", "Rondônia" }, { "RR", "Roraima" }, { "SC", "Santa Catarina" },
                { "SP", "São Paulo" }, { "SE", "Sergipe" }, { "TO", "Tocantins" }
            };
        }
    }
}
