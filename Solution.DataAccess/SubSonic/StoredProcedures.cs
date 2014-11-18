


  
using System;
using System.Data;
using SubSonic.Schema;
using SubSonic.DataProviders;

namespace Solution.DataAccess.DataModel{
	public partial class SPs{

        public static StoredProcedure P_Branch_GetMaxBranchCode(int Depth,int ParentId){
            StoredProcedure sp=new StoredProcedure("P_Branch_GetMaxBranchCode");
			
            sp.Command.AddParameter("Depth",Depth,DbType.Int32);
            sp.Command.AddParameter("ParentId",ParentId,DbType.Int32);
            return sp;
        }
	
	}
	
}
 