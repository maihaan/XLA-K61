<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="SharpmapDemo.Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RadioButtonList ID="rblMapTools" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="0">Zoom in</asp:ListItem>
                <asp:ListItem Value="1">Zoom out</asp:ListItem>
                <asp:ListItem Value="2" Selected="True">Pan</asp:ListItem>
            </asp:RadioButtonList>
            <asp:ImageButton Width="400" Height="200" ID="imgMap" runat="server" OnClick="imgMap_Click" style="border: 1px solid #000;" />
        </div>
    </form>
</body>
</html>
