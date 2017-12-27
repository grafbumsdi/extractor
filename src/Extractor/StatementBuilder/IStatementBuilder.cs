namespace Extractor.StatementBuilder
{
    public interface IStatementBuilder
    {
        string Identifier();

        string QueryStatement();

        string InsertStatement();
    }
}