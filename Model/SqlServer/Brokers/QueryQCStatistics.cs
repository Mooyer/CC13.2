using MatrixPACS.Common;
using MatrixPACS.ImageServer.Enterprise.SqlServer;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Model.SqlServer.Brokers
{
	[ExtensionOf(typeof(BrokerExtensionPoint))]
	public class QueryQCStatistics : ProcedureQueryBroker<QueryQCStatisticsParameters, QueryQCStatisticsResult>, IQueryQCStatistics
	{
		public QueryQCStatistics()
			: base("QueryQCStatistics")
		{
		}
	}
}