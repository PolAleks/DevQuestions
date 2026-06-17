using System.Data;

namespace DevQuestions.Application.Database;

public interface ISqlConnection
{
    IDbConnection GetConnection();
}
