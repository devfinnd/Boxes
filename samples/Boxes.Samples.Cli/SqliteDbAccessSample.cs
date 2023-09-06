using System.Data;
using System.Data.SQLite;
using Boxes.Extensions;
using Boxes.Operators;
using Boxes.Types;
using Dapper;
using static Boxes.Preludes.MaybePrelude;
using static Boxes.Preludes.ResultPrelude;

namespace Boxes.Samples.Cli;

public sealed class SqliteDbAccessSample
{
    public static void Run()
    {
        const string connectionString = "Data Source=Boxes.db;Version=3;";

        using IDbConnection dbConnection = new SQLiteConnection(connectionString);
        var result = PrepareQuery(1)
            .Bind(DbFunctions.TryExecuteQuery<SomeDataResult>(dbConnection))
            .Flatten()
            .Map(x => $"{x.Name} is {x.Age} years old")
            .UnwrapOr("No data found");

        Console.WriteLine(result);
    }

    private static Maybe<CommandDefinition> PrepareQuery(int id)
        => id <= 0
            ? None<CommandDefinition>()
            : Some(new CommandDefinition("Select * from SomeTable WHERE Id = @Id", new { Id = id }));
}

public static class DbFunctions
{
    public static Func<CommandDefinition, Result<Maybe<T>>> TryExecuteQuery<T>(IDbConnection connection) where T : notnull
        => command => Try(() => ExecuteQuery<T>(connection)(command));

    public static Func<CommandDefinition, Maybe<T>> ExecuteQuery<T>(IDbConnection connection) where T : notnull => command => connection.QuerySingle<T>(command) switch
    {
        { } result => Some(result),
        _ => None<T>()
    };
}

public class SomeDataResult
{
    public string Name { get; set; }
    public int Age { get; set; }
}
