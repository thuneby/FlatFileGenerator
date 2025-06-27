namespace FlatFileGenerator.DataGenerator.Interfaces
{
    public interface IClassGenerator
    {
        List<T> GenerateData<T>() where T : class;
    }
}
