﻿using System;
using System.Linq;

namespace Extractor.StatementBuilder
{
    public abstract class DefaultStatementBuilder : IStatementBuilder
    {
        public virtual string InsertStatement()
        {
            return this.InsertStatementColumnList()
                + "VALUES"
                + this.InsertStatementValuesList()
                    .Replace(" ", string.Empty)
                    .Replace(Environment.NewLine, string.Empty);
        }

        public virtual string InsertStatementColumnList()
        {
            return $"INSERT INTO [{this.TableIdentifier()}](" + string.Join(
                       ",",
                       this.Fields().Select(f => $"[{f.ValueIdentifier}]")) + ")";
        }

        public virtual string InsertStatementValuesList()
        {
            return "(" + string.Join(",", this.Fields().Select(f => $"{f.ExactPlaceHolderWithBrackets}")) + ")";
        }

        public virtual string QueryStatement()
        {
            return this.QueryStatement(string.Join(",", this.Fields().Select(f => $"[{f.ValueIdentifier}]")));
        }

        protected virtual string QueryStatement(string fields)
        {
            return $"SELECT {fields} FROM dbo.[{this.TableIdentifier()}] WHERE " + this.GetCondition();
        }

        public abstract Placeholder[] Fields();

        public abstract string TableIdentifier();

        public abstract string GetCondition();

        public virtual string Identifier() => this.TableIdentifier();
    }
}
