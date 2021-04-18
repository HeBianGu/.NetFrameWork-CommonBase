namespace HeBianGu.Common.ValidAttribute
{
    public interface IUnitValidAttribute
    {
        double GetValue(string value);
        string GetView(double value);
    }
}