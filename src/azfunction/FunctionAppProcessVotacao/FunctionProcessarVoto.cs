using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppProcessVotacao.Models;
using FunctionAppProcessVotacao.Data;

namespace FunctionAppProcessVotacao;

public class FunctionProcessarVoto
{
    private readonly ILogger<FunctionProcessarVoto> _logger;
    private readonly VotacaoRepository _repository;

    public FunctionProcessarVoto(ILogger<FunctionProcessarVoto> logger,
        VotacaoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Function("ProcessarVoto")]
    public void Run([EventHubTrigger(
        eventHubName: "testevotacao0",
        Connection = "EventHubsConnectionString",
        ConsumerGroup = "functions")] VotoEventData[] events)
    {
        foreach (VotoEventData voto in events)
        {
            _repository.Save(voto);
            _logger.LogInformation(
                $"Voto registrado com sucesso: {voto.Interesse} | Id: {voto.IdVoto}");
        }
    }
}
