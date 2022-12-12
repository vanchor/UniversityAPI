namespace UniversityApi.Data
{
    public class UniversityMongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DepartmentCollectionName { get; set; } = null!;
    }
}
