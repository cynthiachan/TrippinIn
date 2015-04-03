<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
                <asp:LinkButton runat ="server" ID="lb_eat"  OnClick="lb_eat_Click" CssClass="categoryButton eat">
                </asp:LinkButton>

                <asp:LinkButton runat ="server" ID="lb_drink" OnClick="lb_drink_Click" CssClass ="categoryButton drink">
                </asp:LinkButton>

                <asp:LinkButton runat ="server" ID="lb_party" OnClick="lb_party_Click"  CssClass ="categoryButton party">
                </asp:LinkButton>

                <asp:LinkButton runat ="server" ID="lb_outdoor" OnClick="lb_outdoor_Click" CssClass ="categoryButton outdoor">
                </asp:LinkButton>

                <asp:LinkButton runat ="server" ID="lb_shopping" OnClick="lb_shopping_Click" CssClass ="categoryButton shopping">
                </asp:LinkButton>

                <br />
                <br />

                &nbsp;

                <span class="stylecity">
                    <strong>  Select City: </strong>
                </span>

                &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp;

                <span class ="styleday">
                    <strong>  Select Day: </strong>
                </span>

                &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp;

                <span class="styletime">
                    <strong>  Select Time: </strong>
                </span>

                <br />

                <asp:DropDownList runat="server" ID ="dd1_city" width="145px" AutoPostBack ="true">
                     <asp:ListItem>---Select---</asp:ListItem>
                     <asp:ListItem Text="Tel Aviv" Value="1"></asp:ListItem>
                     <asp:ListItem Text="New York" Value="3"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList runat="server" ID ="ddl_day" width="145px" AutoPostBack ="true" OnSelectedIndexChanged="ddl_day_SelectedIndexChanged">
                     <asp:ListItem>---Select---</asp:ListItem>
                     <asp:ListItem Text="Monday"></asp:ListItem>
                     <asp:ListItem Text="Tuesday"></asp:ListItem>
                     <asp:ListItem Text="Wednesday"></asp:ListItem>
                     <asp:ListItem Text="Thursday"></asp:ListItem>
                     <asp:ListItem Text="Friday"></asp:ListItem>
                     <asp:ListItem Text="Saturday"></asp:ListItem>
                     <asp:ListItem Text="Sunday"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList runat="server" ID ="ddl_time" width="145px">
                     <asp:ListItem>---Select---</asp:ListItem>
                     <asp:ListItem Text="Morning"></asp:ListItem>
                     <asp:ListItem Text="Midday"></asp:ListItem>
                     <asp:ListItem Text="Evening"></asp:ListItem>
                     <asp:ListItem Text="Night"></asp:ListItem>
                     <asp:ListItem Text="Late Night"></asp:ListItem>
                </asp:DropDownList>
        <hr />

        <asp:Button runat="server" ID="btntest" Text ="test" OnClick="btntest_Click" style="height: 26px" />
         
        <asp:Repeater runat="server" ID="rpresults" OnItemDataBound="rpresults_ItemDataBound">
            <ItemTemplate>
                <div class ="repeaterblock">

                   <h2 id ="maincategory"><asp:Label runat="server" ID="lbl_maincategories"></asp:Label></h2>
                   <h2 id ="placetitle"><asp:Label runat="server" ID="lbl_placeTitle"></asp:Label></h2>  
                   <div id="subcategories"><asp:Label runat="server" ID="lbl_subcategories"></asp:Label></div>
                   <asp:Image runat="server" ID ="imageLabel" CssClass ="ImgDetail"/>
                   <br />
                   <br />
                   <h4 id="werehere"> <span><bold>✓</bold></span><asp:Label runat="server" ID="lbl_wereHere"> </asp:Label><span><bold> were-here </bold></span></h4>
                   <br />
                   <br />
                   <h4 id="grade"> <span> <bold> Grade: </bold> </span> <asp:Label runat="server" ID="lbl_grade"></asp:Label></h4>
                   <br /> 
                   <br />
                   <h5 id="totalranks"> <span> <bold> Total Ranks: </bold> </span> <asp:Label runat="server" ID="lbl_totalRanks"></asp:Label></h5>
                   <div id ="main"><img src ="http://fillyourplate.org/blog/wp-content/uploads/2013/03/Food-Symbol-Sign-X-RM-0502.gif" /></div>
             
                </div>
            </ItemTemplate>

        </asp:Repeater>

        <asp:Label runat="server" ID ="lblshowresults">

        </asp:Label>
    </form>
</body>
</html>