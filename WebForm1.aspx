<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="diplomv3.WebForm1" %>

<!DOCTYPE html>

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <html>
        <head> 
            <script language="JavaScript"> 
                function getRandomString(obj) { // Генерация рандомного логина
                    var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var result = "";
                    length = Math.floor((Math.random() * 10) + 5);
                    for (var i = 0; i < length; i++) {
                        result += randomChars.charAt(Math.floor(Math.random() * randomChars.length));
                    }
                    obj.Результат.value = result+"@diplom.com";
                }
            </script> 
         </head> 
        <body>
                 
            
            <form id="forml" runat="server" method="post">
            <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
                <asp:Timer ID="timerTest" runat="server" Interval="1000" ontick="Timer1_Tick">
                </asp:Timer>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="timerTest" EventName="Tick">
                    </asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
                    <br>
                         <input type="button" value=Сгенерировать onClick="getRandomString(forml)">
                         <p><asp:TextBox ID="Результат" runat="server" type="text" CssClass="form-control" placeholder="Нажмите сгенерировать" required="true"></asp:TextBox></p>
                         <asp:Button ID="submit" OnClick="Submit_Click" CssClass="btn btn-primary" text="Создать ящик" runat="server" />
                         <asp:Button ID="submit2" OnClick="Submit_Click2" CssClass="btn btn-primary" text="Обновить входящие письма" runat="server" />
                         <asp:Button ID="submit3" OnClick="Submit_Click3" CssClass="btn btn-primary" Text="Взять новые 10 минут" runat="server" />
                         <asp:Button ID="submit4" OnClick="Submit_Click4" CssClass="btn btn-primary" Text="Завершить сессию" runat="server" />
                    <hr/>
                     <div>
                         <asp:GridView ID="gvMailBox" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvMailBox_SelectedIndexChanged">
                             <Columns>
                                 <asp:BoundField DataField="messageaccountid" HeaderText="ID"/>
                                 <asp:BoundField DataField="messagefilename" HeaderText="file name" />
                                 <asp:BoundField DataField="messagefrom" HeaderText="from who" />
                                 <asp:BoundField DataField="messagecreatetime" HeaderText="when" />
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                     <asp:Button ID="readbutt" Text="Select" runat="server" CommandArgument='<%# Eval("messagefilename") %>' OnClick="gvMailBox_SelectedIndexChanged"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                         </asp:GridView>
                     </div>
                 </form>
         </body> 
    </html>
</div>
