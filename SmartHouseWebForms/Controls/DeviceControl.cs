using SmartHouseWebForms.Models.AbstractDevices;
using SmartHouseWebForms.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SmartHouseWebForms.Controls
{
    public class DeviceControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesList;

        private LinkButton onOffButton;
        private LinkButton onOffFanButton;
        private LinkButton deleteButton;
        private LinkButton countEnergyButton;
        private LinkButton increaseTempButton;
        private LinkButton decreaseTempButton;
        private LinkButton setAutoTemperature;
        private Label stateLabel;
        private Label powerLabel;
        private Label temperatureLabel;
        private Label allPowerLabel;
        private Label stateFanLabel;

        public DeviceControl(int id, IDictionary<int, Device> devicesList)
        {
            this.id = id;
            this.devicesList = devicesList;
            Initializer();
        }

        protected void Initializer()
        {
            if (devicesList[id] is ICountEnergy)
            {
                CssClass = "device energyMeter";
            }

            if (devicesList[id] is ITemperatureSensor)
            {
                CssClass = "device temperatureSensor";
            }

            if ((devicesList[id] is ClimateDevice) && (devicesList[id] is IFan))
            {
                CssClass = "device conditioner";
            }

            if ((devicesList[id] is ClimateDevice) && !(devicesList[id] is IFan))
            {
                CssClass = "device convector";
            }

            Controls.Add(Span(devicesList[id].Name + "<br />"));

            stateLabel = StateLabel(devicesList[id].State);
            Controls.Add(stateLabel);

            powerLabel = new Label();
            powerLabel.Text = devicesList[id].Power.ToString();
            Controls.Add(Span("<br />Мощность: "));
            Controls.Add(powerLabel);

            if (devicesList[id] is ClimateDevice)
            {
                Controls.Add(Span("<br />Заданная температура: "));
                temperatureLabel = new Label();
                temperatureLabel.Text = ((ClimateDevice)devicesList[id]).Temperature.ToString();
                Controls.Add(temperatureLabel);

                Controls.Add(Span("<br />Включить авто режим: "));
            }

            if (devicesList[id] is ITemperatureSensor)
            {
                Controls.Add(Span("<br />Температура: "));
                temperatureLabel = new Label();
                temperatureLabel.Text = ((ITemperatureSensor)devicesList[id]).TemperatureEnvironment.ToString();
                Controls.Add(temperatureLabel);
            }

            if (devicesList[id] is ICountEnergy)
            {
                Controls.Add(Span("<br />Суммарная мощность: "));
                allPowerLabel = new Label();
                allPowerLabel.Text = ((ICountEnergy)devicesList[id]).AllPower.ToString();
                Controls.Add(allPowerLabel);
            }

            deleteButton = new LinkButton();
            deleteButton.CssClass = "btn btn-default removeButton";
            deleteButton.Controls.Add(SpanForButton("glyphicon glyphicon-remove"));
            deleteButton.Click += DeleteButtonClick;
            Controls.Add(deleteButton);
            Controls.Add(Span("<br />"));

            onOffButton = new LinkButton();
            onOffButton.CssClass = "btn btn-default offButton";
            onOffButton.Controls.Add(SpanForButton("glyphicon glyphicon-off"));
            onOffButton.Click += OnOffButtonClick;
            Controls.Add(onOffButton);

            if (devicesList[id] is ClimateDevice)
            {
                decreaseTempButton = new LinkButton();
                decreaseTempButton.CssClass = "btn btn-default";
                decreaseTempButton.Controls.Add(SpanForButton("glyphicon glyphicon-minus"));
                decreaseTempButton.Click += DecreaseTempButtonClick;
                Controls.Add(decreaseTempButton);

                increaseTempButton = new LinkButton();
                increaseTempButton.CssClass = "btn btn-default";
                increaseTempButton.Controls.Add(SpanForButton("glyphicon glyphicon-plus"));
                increaseTempButton.Click += IncreaseTempButtonClick;
                Controls.Add(increaseTempButton);

                setAutoTemperature = new LinkButton();
                setAutoTemperature.CssClass = "btn btn-default autoButton";
                setAutoTemperature.Controls.Add(SpanForButton("glyphicon glyphicon-ok-circle"));
                setAutoTemperature.Click += SetAutoTemperatureButtonClick;
                Controls.Add(setAutoTemperature);
            }

            if (devicesList[id] is ICountEnergy)
            {
                countEnergyButton = new LinkButton();
                countEnergyButton.CssClass = "btn btn-default countEnergyButton";
                countEnergyButton.Controls.Add(SpanForButton("glyphicon glyphicon-flash"));
                countEnergyButton.Click += CountEnergyButtonClick;
                Controls.Add(countEnergyButton);
            }

            if (devicesList[id] is IFan)
            {
                Controls.Add(Span("<br />Вентилятор: "));
                stateFanLabel = StateLabel(((IFan)devicesList[id]).Fan);
                Controls.Add(stateFanLabel);

                onOffFanButton = new LinkButton();
                onOffFanButton.CssClass = "btn btn-default offFanButton";
                onOffFanButton.Controls.Add(SpanForButton("glyphicon glyphicon-off"));
                onOffFanButton.Click += OnOffFanButtonClick;
                Controls.Add(onOffFanButton);
            }
        }

        protected void OnOffButtonClick(object sender, EventArgs e)
        {
            if (devicesList[id].State == true)
            {
                devicesList[id].Off();
                stateLabel.Text = "Выключен";
            }
            else
            {
                devicesList[id].On();
                stateLabel.Text = "Включен";
            }

            powerLabel.Text = devicesList[id].Power.ToString();

            if (devicesList[id] is ClimateDevice)
            {
                temperatureLabel.Text = ((ClimateDevice)devicesList[id]).Temperature.ToString();
            }

            if (devicesList[id] is ITemperatureSensor)
            {
                temperatureLabel.Text = ((ITemperatureSensor)devicesList[id]).TemperatureEnvironment.ToString();
            }

            if (devicesList[id] is IFan)
            {
                if (((IFan)devicesList[id]).Fan == true)
                {
                    stateFanLabel.Text = "Включен";
                }
                else
                {
                    stateFanLabel.Text = "Выключен";
                }
            }
        }

        protected void CountEnergyButtonClick(object sender, EventArgs e)
        {
            ((ICountEnergy)devicesList[id]).CountEnergy(devicesList);
            allPowerLabel.Text = ((ICountEnergy)devicesList[id]).AllPower.ToString();
        }

        protected void DecreaseTempButtonClick(object sender, EventArgs e)
        {
            ((ClimateDevice)devicesList[id]).Decrease();
            temperatureLabel.Text = ((ClimateDevice)devicesList[id]).Temperature.ToString();
        }

        protected void IncreaseTempButtonClick(object sender, EventArgs e)
        {
            ((ClimateDevice)devicesList[id]).Increase();
            temperatureLabel.Text = ((ClimateDevice)devicesList[id]).Temperature.ToString();
        }

        protected void SetAutoTemperatureButtonClick(object sender, EventArgs e)
        {
            foreach (Device device in devicesList.Values)
            {
                if (device is ITemperatureSensor && device.State == true)
                {
                    ((ClimateDevice)devicesList[id]).TemperatureEnvironment = ((ITemperatureSensor)device).TemperatureEnvironment;
                }
            }
            ((ClimateDevice)devicesList[id]).SetAutoTemperature();
            temperatureLabel.Text = ((ClimateDevice)devicesList[id]).Temperature.ToString();
            powerLabel.Text = devicesList[id].Power.ToString();
        }

        protected void OnOffFanButtonClick(object sender, EventArgs e)
        {
            if (((IFan)devicesList[id]).Fan == true)
            {
                ((IFan)devicesList[id]).FanOff();
            }
            else
            {
                ((IFan)devicesList[id]).FanOn();;
            }

            if (((IFan)devicesList[id]).Fan == true)
            {
                stateFanLabel.Text = "Включен";
            }
            if (((IFan)devicesList[id]).Fan == false)
            {
                stateFanLabel.Text = "Выключен";
            }

            powerLabel.Text = devicesList[id].Power.ToString();
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            devicesList.Remove(id);
            Parent.Controls.Remove(this);
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected HtmlGenericControl SpanForButton(string cssClass)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes["class"] = cssClass;
            return span;
        }

        protected Label StateLabel(bool state)
        {
            Label label = new Label();
            if (state == true)
            {
                label.Text = "Включен";
            }
            else
            {
                label.Text = "Выключен";
            }
            return label;
        }
    }
}