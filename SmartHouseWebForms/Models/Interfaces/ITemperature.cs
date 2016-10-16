namespace SmartHouseWebForms.Models.Interfaces
{
    public interface ITemperature
    {
        int Temperature { get; set; }
        void Increase();
        void Decrease();
    }
}