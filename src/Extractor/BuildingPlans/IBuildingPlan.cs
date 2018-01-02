using System.Collections.Generic;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public interface IBuildingPlan
    {
        IList<IStatementBuilder> GetStatementBuilders();
    }
}