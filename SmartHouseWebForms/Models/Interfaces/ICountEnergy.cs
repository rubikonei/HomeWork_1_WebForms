using SmartHouseWebForms.Models.AbstractDevices;
using System.Collections.Generic;

namespace SmartHouseWebForms.Models.Interfaces
{
    public interface ICountEnergy
    {
        double AllPower { get; }
        void CountEnergy(IDictionary<int, Device> devices);
    }
}