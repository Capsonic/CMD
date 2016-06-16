using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using CMDLogic.EF;

namespace CMDLogic
{
    public interface IMetricRepository : IGenericDataRepository<Metric> { }

    public class MetricRepository : GenericDataRepository<Metric>, IMetricRepository { }
}
