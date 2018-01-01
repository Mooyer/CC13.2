using System;
using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model.Parameters
{
	public class QueryQCStatisticsParameters : ProcedureParameters
	{
		public QueryQCStatisticsParameters()
			: base("QueryQCStatistics")
		{
		}

		public DateTime StartTime
		{
			set { SubCriteria["StartTime"] = new ProcedureParameter<DateTime>("StartTime", value); }
		}
		public DateTime EndTime
		{
			set { SubCriteria["EndTime"] = new ProcedureParameter<DateTime>("EndTime", value); }
		}
        public string PartitionAE
        {
            set { SubCriteria["PartitionAE"] = new ProcedureParameter<string>("PartitionAE", value); }
        }
	}
}