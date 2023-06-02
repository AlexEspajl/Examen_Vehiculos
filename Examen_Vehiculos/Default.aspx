<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Examen_Vehiculos._Default" Async="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-3">
            <div>
                <asp:Label Text="MARCA" ID="lblMarca" runat="server"></asp:Label>
             </div>
            <div>
                <asp:Label Text="SUBMARCA" ID="lblSubMarca" runat="server"></asp:Label>
            </div>

            <div>
                <asp:Label Text="MODELO" ID="lblModelo" runat="server"></asp:Label>
            </div>

            <div>
                <asp:Label Text="DESCRIPCION" ID="lblDescripcion" runat="server"></asp:Label>
            </div>
        </div>
        <div  class="col-md-3">
            <div>
                   <asp:dropdownlist runat="server" autopostback="true" ID="ddlMarca" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">  </asp:dropdownlist>
            </div>
            <div>
                <asp:dropdownlist runat="server" autopostback="true" ID="ddlSubMarca"  OnSelectedIndexChanged="ddlSubMarca_SelectedIndexChanged">  </asp:dropdownlist>
            </div>
            <div>
                <asp:dropdownlist runat="server" autopostback="true" ID="ddlModelo"  OnSelectedIndexChanged="ddlModelo_SelectedIndexChanged">  </asp:dropdownlist>
            </div>
            <div>
                <asp:dropdownlist runat="server" autopostback="true" ID="ddlDescripcion"  OnSelectedIndexChanged="ddlDescripcion_SelectedIndexChanged">  </asp:dropdownlist>
                <asp:Label Text="" ID="lblDescripcionId" runat="server" visible="false"></asp:Label>
            </div>
        </div>
        <div  class="col-md-3">
            <div>
                <asp:Label Text="CODIGO POSTAL" ID="lblCodigoPostal" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label Text="ESTADO" ID="lblEstado" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label Text="MUNICIPIO" ID="lblMunicipio" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label Text="COLONIA" ID="lblColonia" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-md-3">
            <div>
            <asp:TextBox ID="txtCodigoPostal" runat="server" AutoPostBack="True" OnTextChanged="txtCodigoPostal_TextChanged" MaxLength="5"></asp:TextBox>
            <asp:RegularExpressionValidator ID="REV1" Text="<p>*Solo se admiten números" ControlToValidate="txtCodigoPostal" Runat="server" Display="Dynamic" EnableClientScript="False" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:TextBox ID="txtEstado" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="txtMunicipio" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div>
            <asp:dropdownlist runat="server" autopostback="true" ID="ddlColonia"></asp:dropdownlist>
        </div>
        </div>
    </div>
    
    
    <div class="row">
        <asp:Button ID="Button1" runat="server" Text="Cotizar" OnClick="Button1_Click" />
    </div>

    <div class="row" runat="server" id="Aseguradoras" visible="false">
        <div class="col-md-2">
            <div class="row">
                 <img src="Content/images/AXA.png" width="50%" height="50%"/>
            </div>
           <div class="row">
               <asp:Label Text="" ID="lblAxa" runat="server"></asp:Label>
           </div>
        </div>
        <div class="col-md-2">
            <div class="row">
                <img src="Content/images/CHUBB.png" width="50%" height="50%"/>
           </div>
            <div class="row">
                <asp:Label Text="" ID="lblChubb" runat="server"></asp:Label>
           </div>
        </div>
        <div class="col-md-2">
            <div class="row">
                <img src="Content/images/HDI.png" width="50%" height="50%"/>
           </div>
            <div class="row">
                <asp:Label Text="" ID="lblHdi" runat="server"></asp:Label>
           </div>
        </div>
        <div class="col-md-2">
            <div class="row">
                <img src="Content/images/QUALITAS.png" width="50%" height="50%"/>
           </div>
            <div class="row">
                <asp:Label Text="" ID="lblQualitas" runat="server"></asp:Label>
           </div>
        </div>
        <div class="col-md-2">
            <div class="row">
                 <img src="Content/images/ZURICH.png" width="50%" height="50%"/>
           </div>
            <div class="row">
                <asp:Label Text="" ID="lblZurich" runat="server"></asp:Label>
           </div>
        </div>
        <div class="col-md-2">
        <asp:Button ID="Aceptar" runat="server" Text="Aceptar" OnClick="Aceptar_Click" />
        </div>
    </div>
</asp:Content>

