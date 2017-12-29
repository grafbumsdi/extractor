﻿using System.Collections.Generic;
using System.IO;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class BasicExtractor
    {
        public BasicExtractor(IList<IStatementBuilder> statementBuilders)
        {
            this.StatementBuilders = statementBuilders;
        }

        private IList<IStatementBuilder> StatementBuilders { get; set; }

        public IList<IStatementBuilder> GetStatementBuilders() => this.StatementBuilders;

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
