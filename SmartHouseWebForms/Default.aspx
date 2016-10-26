<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouseWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Smart House</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-2.2.3.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="~/Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server" method="post">
        <asp:DropDownList ID="dropDownDevicesList" runat="server">
            <asp:ListItem>Кондиционер</asp:ListItem>
            <asp:ListItem>Конвектор</asp:ListItem>
            <asp:ListItem>Счетчик электроэнергии</asp:ListItem>
            <asp:ListItem>Датчик температуры</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="addDeviceButton" runat="server" Text="Добавить" />
        <asp:Panel ID="devicesPanel" runat="server"></asp:Panel>
        <div id="helpPanel">
            <span>Доступные команды:</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-remove"></span>
            </a>
            <span>Удалить устройство</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-off"></span>
            </a>
            <span>Включить/Выключить устройство</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-minus"></span>
            </a>
            <span>Уменьшить температуру</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-plus"></span>
            </a>
            <span>Увеличить температуру</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-ok-circle"></span>
            </a>
            <span>Включить автоматический режим, для кондиционера при 25 - 40 С, для конвектора при -40 - 10 С от температуры окружающей среды</span>
            <br />
            <a class="btn btn-default">
                <span class="glyphicon glyphicon-flash"></span>
            </a>
            <span>Просмотр суммарной мощности всех устройств</span>
        </div>
    </form>
</body>
</html>
