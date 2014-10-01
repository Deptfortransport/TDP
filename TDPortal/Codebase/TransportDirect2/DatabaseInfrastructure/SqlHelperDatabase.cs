// *********************************************** 
// NAME             : SqlHelperDatabase.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Enumeration defining types of databases applications interact with
// ************************************************
// 



namespace TDP.Common.DatabaseInfrastructure
{
    /// <summary>
    /// Enumeration representing different databases available 
    /// </summary>
    public enum SqlHelperDatabase
    {
        DefaultDB,
        TransientPortalDB,
        GazetteerDB,
        ContentDB,
        CommandControlDB,
        ReportStagingDB,
        AtosAdditionalDataDB,
        AirDataProviderDB,
        AdditionalDataDB,
        SqlHelperDatabaseEnd // Should ALWAYS be the last one, represents maximum.
    }
}
