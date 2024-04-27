namespace SSTMS.Model
{

    public class tableCreation
    {
        public string Description { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, ColumnDetails> Columns { get; set; }
        //public Dictionary<string, ForeignKeyDetails> ForeignKeys { get; set; }
    }

    public class ColumnDetails
    {
        public string DataType { get; set; }
        public string? MaxLength { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
    }

    //public class ForeignKeyDetails
    //{
    //    public string ColumnName { get; set; }
    //    public string ReferencedTableName { get; set; }
    //    public string ReferencedColumnName { get; set; }
    //}
}
