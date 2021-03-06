﻿<%@ Page Title="" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="showlistProducts.aspx.cs" Inherits="user_showlistProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><center>
<table style="width: 70%;">
        <tr>
            <td>&nbsp;
                所有"<%=Request.QueryString["name"] %>"类商品
                <asp:ListView ID="ListView2" runat="server" DataKeyNames="pID" DataSourceID="SqlDataSource2" GroupItemCount="3">
                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div class="card border-info text-center bg-transparent">
                                <div class="card-content">
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("pIMG") %>' alt="element 04" width="200" class="float-left mt-3 pl-2" title='<%# Eval("pNAME") %>' />
                                    
                                    <div class="card-body">
                                        <h4 class="card-title mt-3">                            <a href="showProducts.aspx?id=<%# Eval("pID") %>&type=<%# Eval("pCLASSID") %>" title='<%# Eval("pNAME") %>'><%# Eval("pNAME").ToString().Length>6?Eval("pNAME").ToString().Substring(0,6):Eval("pNAME") %></a>
</h4>
                                        <p class="card-text mb-25">                            ￥:<asp:Label ID="pPRICELabel" runat="server" Text='<%# Eval("pPRICE") %>' />
</p>
                                        <button class="btn btn-info mt-1" ><a href="showProducts.aspx?id=<%# Eval("pID") %>&type=<%# Eval("pCLASSID") %>">Buy Now</a></button>
                                    </div>
                                </div>
                            </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                        <tr id="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style=""></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:DataPager ID="DataPager1" runat="server" PageSize="6" PagedControlID="ListView2"></asp:DataPager>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db17135111ConnectionString %>" SelectCommand="SELECT [pID], [pNAME], [pIMG], [pPRICE], [pCLASSID] FROM [Products] WHERE ([pCLASSID] = @pCLASSID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="pCLASSID" QueryStringField="type" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </td>
        </tr>
    </table>
   </center> 
</asp:Content>

