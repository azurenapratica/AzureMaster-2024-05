using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using FunctionAppProcessVotacao.Models;

namespace FunctionAppProcessVotacao.Data;

public class VotacaoRepository
{
    public VotacaoRepository()
    {
    }

    public void Save(VotoEventData voto)
    {
        using var conexao = new SqlConnection(
            Environment.GetEnvironmentVariable("BaseVotacaoConnectionString"));
        conexao.Insert<VotoTecnologia>(new()
        {
            DataProcessamento = DateTime.UtcNow.AddHours(-3),
            EventHub = "testevotacao0",
            Producer = voto.Producer,
            Consumer = Environment.MachineName,
            ConsumerGroup = "functions",
            HorarioVoto = voto.Horario,
            IdVoto = voto.IdVoto,
            Interesse = voto.Interesse
        });
    }
}