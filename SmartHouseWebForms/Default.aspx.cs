using SmartHouseWebForms.Controls;
using SmartHouseWebForms.Models.AbstractDevices;
using SmartHouseWebForms.Models.Factory;
using System;
using System.Collections.Generic;

namespace SmartHouseWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private IDictionary<int, Device> devicesList;
        private Factory factory;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devicesList = (Dictionary<int, Device>)Session["Devices"];
            }
            else
            {
                factory = new Factory();
                devicesList = new Dictionary<int, Device>();
                devicesList.Add(1, factory.GetConditioner());
                devicesList.Add(2, factory.GetConvector());
                devicesList.Add(3, factory.GetEnergyMeter());
                devicesList.Add(4, factory.GetTemperatureSensor());

                Session["Devices"] = devicesList;
                Session["NextId"] = 5;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                addDeviceButton.Click += AddDeviceButtonClick;
            }
            InitialiseDevicesPanel();
        }

        protected void InitialiseDevicesPanel()
        {
            foreach (int key in devicesList.Keys)
            {
                devicesPanel.Controls.Add(new DeviceControl(key, devicesList));
            }
        }

        protected void AddDeviceButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            factory = new Factory();
            switch (dropDownDevicesList.SelectedIndex)
            {
                default:
                    newDevice = factory.GetConditioner();
                    break;
                case 1:
                    newDevice = factory.GetConvector();
                    break;
                case 2:
                    newDevice = factory.GetEnergyMeter();
                    break;
                case 3:
                    newDevice = factory.GetTemperatureSensor();
                    break;
            }
            int id = (int)Session["NextId"];
            devicesList.Add(id, newDevice);
            devicesPanel.Controls.Add(new DeviceControl(id, devicesList));
            id++;
            Session["NextId"] = id;
        }
    }
}