namespace LiveCoding.Domain;

public interface IProvideBar
{
    IEnumerable<Bar?> GetAllBars();
}