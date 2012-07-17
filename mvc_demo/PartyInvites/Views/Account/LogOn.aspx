<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PartyInvites.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    로그온
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>로그온</h2>
    <p>
        사용자 이름과 암호를 입력하십시오. <%: Html.ActionLink("계정이 없는 경우", "Register") %> 등록하십시오.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "로그인하지 못했습니다. 오류를 수정한 후 다시 시도하십시오.") %>
        <div>
            <fieldset>
                <legend>계정 정보</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.CheckBoxFor(m => m.RememberMe) %>
                    <%: Html.LabelFor(m => m.RememberMe) %>
                </div>
                
                <p>
                    <input type="submit" value="로그온" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
