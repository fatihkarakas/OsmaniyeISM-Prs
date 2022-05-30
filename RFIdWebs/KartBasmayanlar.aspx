﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="KartBasmayanlar.aspx.cs" Inherits="KartBasmayanlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3>Kart Basmayanlar Listesi</h3>
                </div>
                <div class="card-body">

                    <div class="alert alert-warning alert-dismissible fade show" role="alert" runat="server" id="HataMsj" visible="false">
                        <label runat="server" id="msj"></label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <table class="table table-bordered">
                            <tr>
                                <td>Başkanlık Seçiniz</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-select"  DataTextField="baskanlik" DataValueField="id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DB1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringLocal %>" SelectCommand="SELECT [id], [baskanlik] FROM [Servis]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>Başlangıç Tarihi</td>
                                <td>
                                  <asp:TextBox runat="server" id="BasTarih" name="dDate" class="form-control" autocomplete="off" />


                                </td>
                            </tr>
                            <tr>
                                <td>Bitiş Tarihi</td>
                                <td>
                                    <asp:TextBox runat="server" id="BitTarih" name="dDate" class="form-control" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Button runat="server" ID="RaporGetirBtn" CssClass="btn btn-block btn-lg btn-info" Text="Raporları Getir" OnClick="RaporGetirBtn_Click"/></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                         <asp:Repeater runat="server" ID="Listele">
                        <HeaderTemplate>
                            <div class="alert alert-danger">
                                Seçili Tarih Arasında Kart Basmayanlar
                            </div>
                            <table class="table table-bordered" id="PersonelTablo">
                                <thead>

                                    <td>Personel İd</td>
                                    <td>Adı Soyadı</td>
                                    <td>Başkanlık</td>
                                    <td>Başlangıç Tarihi</td>
                                    <td>Bitiş Tarihi </td>
                                   
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("perid") %></td>
                                <td><%# Eval("isim") %></td>
                                <td><%# Eval("baskanlik") %></td>
                                <td><%# Eval("tarih1", "{0: dd/MM/yyyy}") %></td>
                                <td><%# Eval("tarih2", "{0: dd/MM/yyyy}") %></td>
                                
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                         <asp:SqlDataSource ID="DB2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringLocal %>">
                             
                         </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('#MainContent_BasTarih').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "yyyy/mm/dd",
            language: "Tr"
        });
         $('#MainContent_BitTarih').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "yyyy/mm/dd",
            language: "Tr"
        });
    });
</script>
</asp:Content>
