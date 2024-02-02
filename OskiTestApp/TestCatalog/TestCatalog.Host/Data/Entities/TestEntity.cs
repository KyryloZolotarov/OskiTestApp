namespace TestCatalog.Host.Data.Entities
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionEntity> Questions { get; set; }
    }
}
