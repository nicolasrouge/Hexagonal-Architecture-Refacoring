namespace LiveCoding.Services;

public interface IProvideBar
{
    IEnumerable<Bar?> GetAllBars();
}