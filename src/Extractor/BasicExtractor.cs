using System.Collections.Generic;
using System.IO;

using Extractor.BuildingPlans;
using Extractor.StatementBuilder;

namespace Extractor
{
    public class BasicExtractor
    {
        public BasicExtractor()
        : this(new List<IStatementBuilder>())
        {
        }

        public BasicExtractor(IList<IStatementBuilder> statementBuilders)
        {
            this.StatementBuilders = new List<IStatementBuilder>();
            this.StatementBuilders.AddRange(statementBuilders);
        }

        public void AddBuildingPlan(IBuildingPlan buildingPlan)
        {
            this.StatementBuilders.AddRange(buildingPlan.GetStatementBuilders());
        }

        private List<IStatementBuilder> StatementBuilders { get; set; }

        public void WriteInserts(TextWriter writer)
        {
            foreach (var statementBuilder in this.StatementBuilders)
            {
                this.WriteInsertsWithStatementBuilder(writer, statementBuilder);
            }
        }

        private void WriteInsertsWithStatementBuilder(TextWriter writer, IStatementBuilder statementBuilder)
        {
            writer.WriteLine($"-- INSERTS FOR: {statementBuilder.Identifier()}");
            this.ReadAndReplace(writer, statementBuilder);
            writer.WriteLine(string.Empty);
        }

        private void ReadAndReplace(TextWriter writer, IStatementBuilder statementBuilder)
        {
            foreach (var row in new SqlDataReader().GetRows(statementBuilder.QueryStatement()))
            {
                writer.WriteLine(new Replacer(row, statementBuilder.InsertStatement()).GetFinalOutput());
            }
        }
    }
}
